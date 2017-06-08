using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

using OSharp.Utility;
using OSharp.Utility.Exceptions;
using OSharp.Utility.Logging;
using OSharp.Utility.Secutiry;
using OSharp.Web.Http.Internal;
using OSharp.Web.Http.Properties;


namespace OSharp.Web.Http.Security
{
    /// <summary>
    /// 服务端通信加密解密消息处理器
    /// </summary>
    public class HostCryptoDelegatingHandler : DelegatingHandler
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(HostCryptoDelegatingHandler));
        private readonly string _privateKey;
        private readonly string _hashType;
        private CommunicationCryptor _cryptor;

        /// <summary>
        /// 使用服务端密钥初始化<see cref="HostCryptoDelegatingHandler"/>类的新实例
        /// </summary>
        /// <param name="privateKey">服务端私钥</param>
        /// <param name="hashType">签名哈希类型，必须为MD5或SHA1</param>
        public HostCryptoDelegatingHandler(string privateKey, string hashType = "MD5")
        {
            privateKey.CheckNotNullOrEmpty("privateKey");
            hashType.CheckNotNullOrEmpty("hashType");
            hashType = hashType.ToUpper();
            hashType.Required(str => hashType == "MD5" || hashType == "SHA1", Resources.Http_Security_RSA_Sign_HashType);

            _privateKey = privateKey;
            _hashType = hashType;
        }

        /// <summary>
        /// 是否开启通信加密功能
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual bool CanCrypto(HttpRequestMessage request)
        {
            return true;
        }

        /// <summary>
        /// 以异步操作发送 HTTP 请求到内部管理器以发送到服务器。
        /// </summary>
        /// <returns>
        /// 返回 <see cref="T:System.Threading.Tasks.Task`1"/>。 表示异步操作的任务对象。
        /// </returns>
        /// <param name="request">要发送到服务器的 HTTP 请求消息。</param><param name="cancellationToken">取消操作的取消标记。</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="request"/> 为 null。</exception>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!CanCrypto(request))
            {
                return base.SendAsync(request, cancellationToken);
            }

            var result = DecryptRequest(request);
            if (result != null)
            {
                return result;
            }
            return base.SendAsync(request, cancellationToken)
                .ContinueWith(task => EncryptResponse(task.Result), cancellationToken);
        }

        private Task<HttpResponseMessage> DecryptRequest(HttpRequestMessage request)
        {
            if (!request.Headers.Contains(HttpHeaderNames.OSharpClientPublicKey))
            {
                return CreateResponseTask(request, HttpStatusCode.BadRequest, "在请求头中客户端公钥信息无法找到。");
            }
            string publicKey = request.Headers.GetValues(HttpHeaderNames.OSharpClientPublicKey).First();
            _cryptor = new CommunicationCryptor(_privateKey, publicKey, _hashType);

            if (request.Content == null)
            {
                return null;
            }
            string data = request.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            try
            {
                if (_cryptor != null)
                {
                    data = _cryptor.DecryptAndVerifyData(data);
                }
                if (data == null)
                {
                    throw new OSharpException("服务器解析请求数据时发生异常。");
                }
                HttpContent content = new StringContent(data);
                content.Headers.ContentType = request.Content.Headers.ContentType;
                request.Content = content;
                return null;
            }
            catch (CryptographicException ex)
            {
                const string message = "服务器解析传输数据时发生异常。";
                Logger.Error(message, ex);
                return CreateResponseTask(request, HttpStatusCode.BadRequest, message, ex);
            }
            catch (Exception ex)
            {
                Logger.Error(Resources.Http_Security_Host_DecryptRequest_Failt, ex);
                return CreateResponseTask(request, HttpStatusCode.BadRequest, Resources.Http_Security_Host_DecryptRequest_Failt, ex);
            }
        }

        private HttpResponseMessage EncryptResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return response;
            }
            if (response.Content == null)
            {
                return response;
            }
            string data = response.Content.ReadAsStringAsync().Result;
            try
            {
                if (_cryptor != null)
                {
                    data = _cryptor.EncryptData(data);
                }
                HttpContent content = new StringContent(data);
                content.Headers.ContentType = response.Content.Headers.ContentType;
                response.Content = content;
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(Resources.Http_Security_Host_EncryptResponse_Failt, ex);
                HttpError error = new HttpError(Resources.Http_Security_Host_EncryptResponse_Failt);
                return response.RequestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, error);
            }
        }

        private Task<HttpResponseMessage> CreateResponseTask(HttpRequestMessage request,
            HttpStatusCode statusCode,
            string message,
            Exception ex = null)
        {
            return Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                if (statusCode >= HttpStatusCode.OK && statusCode <= (HttpStatusCode)299)
                {
                    if (_cryptor != null)
                    {
                        message = _cryptor.EncryptData(message);
                        return request.CreateResponse(statusCode, message);
                    }
                }
                HttpResponseMessage response = request.CreateErrorResponse(statusCode, ex == null ? new HttpError(message) : new HttpError(ex, false));
                return response;
            });
        }
    }
}
