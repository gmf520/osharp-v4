using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace OSharp.Web.Http.Handlers
{
    /// <summary>
    /// HTTP基本身份验证基类
    /// </summary>
    public abstract class BasicAuthenticationHandlerBase : DelegatingHandler
    {
        protected abstract string Realm { get; }

        /// <summary>
        /// 重写以实现身体验证的具体业务
        /// </summary>
        /// <param name="username">登录账号</param>
        /// <param name="password">登录密码</param>
        /// <returns>是否验证通过</returns>
        protected abstract bool Authorize(string username, string password);

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization != null && request.Headers.Authorization.Scheme == "Basic")
            {
                var credentials = ParseCredentials(request.Headers.Authorization);

                if (Authorize(credentials.Username, credentials.Password))
                {
                    return base.SendAsync(request, cancellationToken);
                }
            }

            return CreateUnauthorizeResponseMessage();
        }

        private Task<HttpResponseMessage> CreateUnauthorizeResponseMessage()
        {
            return Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", Realm));
                return response;
            });
        }

        private static BasicCredentials ParseCredentials(AuthenticationHeaderValue authHeader)
        {
            try
            {
                var credentials = Encoding.ASCII.GetString(Convert.FromBase64String(authHeader.ToString().Substring(6)));
                int splitOn = credentials.IndexOf(':');

                return new BasicCredentials
                {
                    Username = credentials.Substring(0, splitOn),
                    Password = credentials.Substring(splitOn + 1)
                };
            }
            catch { }

            return new BasicCredentials();
        }

        internal struct BasicCredentials
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
