using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

using OSharp.Utility.Extensions;


namespace OSharp.Web.Http.Extensions
{
    public static class HttpMessageExtensions
    {
        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";

        public static bool IsLocal(this HttpRequestMessage request)
        {
            var localFlag = request.Properties["MS_IsLocal"] as Lazy<bool>;
            return localFlag != null && localFlag.Value;
        }

        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey(HttpContext))
            {
                dynamic ctx = request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            return null;
        }

        public static Task<HttpResponseMessage> ToTask(this HttpResponseMessage responseMessage)
        {
            TaskCompletionSource<HttpResponseMessage> taskCompletionSource = new TaskCompletionSource<HttpResponseMessage>();
            taskCompletionSource.SetResult(responseMessage);
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// 获取<see cref="HttpResponseMessage"/>中包装的错误信息
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string GetErrorMessage(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                string msg = "请求处理失败";
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        msg = "请求的资源不存在";
                        break;
                    case HttpStatusCode.BadRequest:
                        msg = "请求中止";
                        break;
                    case HttpStatusCode.Forbidden:
                        msg = "请求被拒绝";
                        break;
                    case HttpStatusCode.ServiceUnavailable:
                        msg = "服务器忙或停机维护";
                        break;
                }
                MediaTypeHeaderValue contentType = response.Content.Headers.ContentType;
                if (contentType == null || contentType.MediaType != "text/html")
                {
                    HttpError error = response.Content.ReadAsAsync<HttpError>().Result;
                    if (error != null)
                    {
                        string errorMsg = error.Message;
                        if (errorMsg.Contains("An error has occurred"))
                        {
#if NET45
                            errorMsg = error.ExceptionMessage;
#else
                            errorMsg = "请求处理发生错误";
#endif
                        }
                        msg = "{0}，详情：{1}".FormatWith(msg, errorMsg);
                    }
                }
                return msg;
            }
            return null;
        }
    }
}
