using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

using OSharp.Utility;
using OSharp.Utility.Exceptions;
using OSharp.Utility.Logging;
using OSharp.Utility.Secutiry;
using OSharp.Web.Http.Internal;
using OSharp.Web.Properties;


namespace OSharp.Web.Http.Security
{
    /// <summary>
    /// 客户端通信加密解密消息处理器
    /// </summary>
    public class ClientCryptoDelegatingHandler : DelegatingHandler
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(ClientCryptoDelegatingHandler));
        private readonly CommunicationCryptor _cryptor;
        private readonly string _clientPublicKey;

        /// <summary>
        /// 使用服务端公钥初始化<see cref="ClientCryptoDelegatingHandler"/>类的新实例
        /// </summary>
        /// <param name="publicKey">服务端公钥</param>
        /// <param name="hashType">签名哈希类型，必须为MD5或SHA1</param>
        public ClientCryptoDelegatingHandler(string publicKey, string hashType = "MD5")
        {
            publicKey.CheckNotNullOrEmpty("publicKey");
            hashType.CheckNotNullOrEmpty("hashType");
            hashType = hashType.ToUpper();
            hashType.Required(str => hashType == "MD5" || hashType == "SHA1", Resources.Http_Security_RSA_Sign_HashType);
            RsaHelper rsa = new RsaHelper();
            _cryptor = new CommunicationCryptor(rsa.PrivateKey, publicKey, hashType);
            _clientPublicKey = rsa.PublicKey;
        }

        /// <summary>
        /// 以异步操作发送 HTTP 请求到内部管理器以发送到服务器。
        /// </summary>
        /// <returns>
        /// 返回 <see cref="T:System.Threading.Tasks.Task`1"/>。 表示异步操作的任务对象。
        /// </returns>
        /// <param name="request">要发送到服务器的 HTTP 请求消息。</param>
        /// <param name="cancellationToken">取消操作的取消标记。</param>
        /// <exception cref="T:System.ArgumentNullException"> <paramref name="request"/> 为 null。</exception>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = EncryptRequest(request);
            if (result != null)
            {
                return result;
            }
            return base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        var aggregateException = task.Exception;
                        if (aggregateException != null)
                        {
                            var requestException = aggregateException.InnerExceptions.FirstOrDefault(m => m is HttpRequestException);
                            if (requestException != null && requestException.InnerException is WebException)
                            {
                                return request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, requestException.InnerException);
                            }
                        }

                        return request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, task.Exception);
                    }
                    return DecryptResponse(task.Result);
                }, cancellationToken);
        }

        private Task<HttpResponseMessage> EncryptRequest(HttpRequestMessage request)
        {
            request.Headers.Add(HttpHeaderNames.OSharpClientPublicKey, _clientPublicKey);

            if (request.Method == HttpMethod.Get || request.Content == null)
            {
                return null;
            }
            string data = request.Content.ReadAsStringAsync().Result;
            try
            {
                data = _cryptor.EncryptData(data);
                HttpContent content = new StringContent(data);
                content.Headers.ContentType = request.Content.Headers.ContentType;
                request.Content = content;
                return null;
            }
            catch (Exception ex)
            {
                Logger.Error(Resources.Http_Security_Client_EncryptRequest_Failt, ex);
                return CreateResponseTask(request, HttpStatusCode.BadRequest, Resources.Http_Security_Client_EncryptRequest_Failt, ex);
            }
        }

        private HttpResponseMessage DecryptResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return response;
            }
            string data = response.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrEmpty(data))
            {
                return response;
            }
            try
            {
                data = _cryptor.DecryptAndVerifyData(data);
                if (data == null)
                {
                    throw new OSharpException(Resources.Http_Security_Client_VerifyResponse_Failt);
                }
                HttpContent content = new StringContent(data);
                content.Headers.ContentType = response.Content.Headers.ContentType;
                response.Content = content;
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(Resources.Http_Seciruty_Client_DecryptResponse_Failt, ex);
                response = response.RequestMessage.CreateErrorResponse(HttpStatusCode.InternalServerError, Resources.Http_Seciruty_Client_DecryptResponse_Failt);
                return response;
            }
        }

        private Task<HttpResponseMessage> CreateResponseTask(HttpRequestMessage request, HttpStatusCode statusCode, string message, Exception ex = null)
        {
            return Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                if (ex == null)
                {
                    return request.CreateResponse(statusCode, message, MediaTypeConstants.ApplicationJson);
                }
                Exception exception = new Exception(message, ex);
                var response = request.CreateErrorResponse(statusCode, new HttpError(exception, true));
                return response;
            });
        }
    }
}
