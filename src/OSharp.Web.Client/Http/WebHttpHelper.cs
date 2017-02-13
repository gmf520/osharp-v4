using System;
using System.Net;
using System.Net.Http;

using OSharp.Utility;
using OSharp.Web.Http.Security;


namespace OSharp.Web.Http
{
    /// <summary>
    /// WebAPI辅助操作类
    /// </summary>
    public static class WebHttpHelper
    {
        /// <summary>
        /// 获取HttpClient对象
        /// </summary>
        public static HttpClient GetHttpClient(string baseUrl, string publicKey = null, string hashType = "MD5")
        {
            baseUrl.CheckNotNullOrEmpty("baseUrl");
            HttpClient httpClient = publicKey == null
                ? new HttpClient()
                : HttpClientFactory.Create(new ClientCryptoDelegatingHandler(publicKey, hashType));
            httpClient.BaseAddress = new Uri(baseUrl);
            return httpClient;
        }

        /// <summary>
        /// 将Http状态码翻译为对应的中文
        /// </summary>
        /// <param name="code">Http状态码</param>
        /// <returns>中文解析</returns>
        public static string ToChsText(this HttpStatusCode code)
        {
            switch (code)
            {
                case HttpStatusCode.Continue:
                    return "请求者应继续进行请求";
                case HttpStatusCode.SwitchingProtocols:
                    return "请求者已要求服务器切换协议，服务器已确认并准备进行切换";
                case HttpStatusCode.OK:
                    return "服务器成功处理了相应请求";
                case HttpStatusCode.Created:
                    return "请求成功且服务器已创建了新的资源";
                case HttpStatusCode.Accepted:
                    return "服务器已接受相应请求，但尚未对其进行处理";
                case HttpStatusCode.NonAuthoritativeInformation:
                    return "服务器已成功处理相应请求，但返回了可能来自另一来源的信息";
                case HttpStatusCode.NoContent:
                    return "服务器已成功处理相应请求，但未返回任何内容";
                case HttpStatusCode.ResetContent:
                    return "服务器已成功处理相应请求，但未返回任何内容，但要求请求者重置文档视图";
                case HttpStatusCode.PartialContent:
                    return "服务器成功处理了部分 GET 请求";
                case HttpStatusCode.MultipleChoices:
                    return "服务器可以根据请求来执行多项操作";
                case HttpStatusCode.Moved:
                    return "请求的网页已永久移动到新位置";
                case HttpStatusCode.Redirect:
                    return "服务器目前正从不同位置的网页响应请求，但请求者应继续使用原有位置来进行以后的请求";
                case HttpStatusCode.RedirectMethod:
                    return "当请求者应对不同的位置进行单独的 GET 请求以检索响应时，服务器会返回此代码";
                case HttpStatusCode.NotModified:
                    return "请求的网页自上次请求后再也没有修改过";
                case HttpStatusCode.UseProxy:
                    return "请求者只能使用代理访问请求的网页";
                case HttpStatusCode.Unused:
                    return "Unused 是未完全指定的 HTTP/1.1 规范的建议扩展";
                case HttpStatusCode.RedirectKeepVerb:
                    return "服务器目前正从不同位置的网页响应请求，但请求者应继续使用原有位置来进行以后的请求";
                case HttpStatusCode.BadRequest:
                    return "服务器未能识别请求";
                case HttpStatusCode.Unauthorized:
                    return "请求要求进行身份验证";
                case HttpStatusCode.PaymentRequired:
                    return "保留 PaymentRequired 以供将来使用";
                case HttpStatusCode.Forbidden:
                    return "服务器拒绝相应请求";
                case HttpStatusCode.NotFound:
                    return "服务器找不到请求的资源";
                case HttpStatusCode.MethodNotAllowed:
                    return "禁用相应请求中所指定的方法";
                case HttpStatusCode.NotAcceptable:
                    return "无法使用相应请求的内容特性来响应请求的网页";
                case HttpStatusCode.ProxyAuthenticationRequired:
                    return "请求者应当使用代理进行授权";
                case HttpStatusCode.RequestTimeout:
                    return "服务器在等待请求时超时";
                case HttpStatusCode.Conflict:
                    return "服务器在完成请求时遇到冲突";
                case HttpStatusCode.Gone:
                    return "请求的资源已被永久删除";
                case HttpStatusCode.LengthRequired:
                    return "服务器不会接受包含无效内容长度标头字段的请求";
                case HttpStatusCode.PreconditionFailed:
                    return "服务器未满足请求者在请求中设置的其中一个前提条件";
                case HttpStatusCode.RequestEntityTooLarge:
                    return "服务器无法处理相应请求，因为请求实体过大，已超出服务器的处理能力";
                case HttpStatusCode.RequestUriTooLong:
                    return "请求的 URI 过长，服务器无法进行处理";
                case HttpStatusCode.UnsupportedMediaType:
                    return "相应请求的格式不受请求页面的支持";
                case HttpStatusCode.RequestedRangeNotSatisfiable:
                    return "如果相应请求是针对网页的无效范围进行的，那么服务器会返回此状态代码";
                case HttpStatusCode.ExpectationFailed:
                    return "服务器未满足“期望”请求标头字段的要求";
                case HttpStatusCode.InternalServerError:
                    return "服务器内部遇到错误，无法完成相应请求";
                case HttpStatusCode.NotImplemented:
                    return "请求的功能在服务器中尚未实现";
                case HttpStatusCode.BadGateway:
                    return "服务器作为网关或代理，从上游服务器收到了无效的响应";
                case HttpStatusCode.ServiceUnavailable:
                    return "目前服务器不可用（由于超载或进行停机维护）";
                case HttpStatusCode.GatewayTimeout:
                    return "服务器作为网关或代理，未及时从上游服务器接收请求";
                case HttpStatusCode.HttpVersionNotSupported:
                    return "服务器不支持相应请求中所用的 HTTP 协议版本";
                default:
                    return "未知Http状态";
            }
        }
    }
}
