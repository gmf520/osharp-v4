using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using OSharp.Utility.Extensions;
using OSharp.Utility.Secutiry;


namespace OSharp.Web.Http.Handlers
{
    /// <summary>
    /// HTTP摘要身份验证基类
    /// </summary>
    public class DigestAuthenticationHandler : DelegatingHandler
    {
        protected string Realm { get { return "66soft.net"; } }

        #region Overrides of DelegatingHandler

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpRequestHeaders headers = request.Headers;
            if (headers.Authorization != null)
            {
                Header header = new Header(request.Headers.Authorization.Parameter, request.Method.Method);
                if (Nonce.IsValid(header.Nonce, header.NounceCounter))
                {
                    string password = header.UserName;
                    string ha1 = "{0}:{1}:{2}".FormatWith(header.UserName, header.Realm, password).ToMd5Hash();

                    string ha2 = "{0}:{1}".FormatWith(header.Method, header.Uri).ToMd5Hash();

                    string computeResponse = "{0}:{1}:{2}:{3}:{4}:{5}".FormatWith(ha1, header.Nonce, header.NounceCounter, header.Cnonce).ToMd5Hash();

                    if (string.CompareOrdinal(header.Response, computeResponse) == 0)
                    {
                        GenericIdentity identity = new GenericIdentity(header.UserName, "Digest");
                        GenericPrincipal principal = new GenericPrincipal(identity, null);
                        Thread.CurrentPrincipal = principal;
                        if (System.Web.HttpContext.Current != null)
                        {
                            System.Web.HttpContext.Current.User = principal;
                        }
                    }
                }
            }

            return base.SendAsync(request, cancellationToken).ContinueWith(task =>
            {
                if (task.Result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    task.Result.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Digest", Header.UnauthorizeResponseHeader.ToString()));
                }
                return task.Result;
            }, cancellationToken);
        }

        #endregion
    }


    public class Header
    {
        public Header()
        { }

        public Header(string header, string method)
        {
            string pairs = header.Replace("\"", string.Empty);
            foreach (string pair in pairs.Split(','))
            {
                int index = pair.IndexOf("=", StringComparison.Ordinal);
                string key = pair.Substring(0, index);
                string value = pair.Substring(index + 1);

                switch (key)
                {
                    case "username": 
                        UserName = value;
                        break;
                    case "realm":
                        Realm = value;
                        break;
                    case "nonce":
                        Nonce = value;
                        break;
                    case "uri":
                        Uri = value;
                        break;
                    case "nc":
                        NounceCounter = value;
                        break;
                    case "cnonce":
                        Cnonce = value;
                        break;
                    case "response":
                        Response = value;
                        break;
                    case "method":
                        Method = value;
                        break;
                }
            }
            if (string.IsNullOrEmpty(Method))
            {
                Method = method;
            }
        }

        public string Cnonce { get; private set; }
        public string Nonce { get; private set; }
        public string Realm { get; private set; }
        public string UserName { get; private set; }
        public string Uri { get; private set; }
        public string Response { get; private set; }
        public string Method { get; private set; }
        public string NounceCounter { get; private set; }

        public static Header UnauthorizeResponseHeader
        {
            get { return new Header { Realm = "Realm Of Badri", Nonce = Handlers.Nonce.Generate() }; }
        }

        public override string ToString()
        {
            StringBuilder header = new StringBuilder();
            header.AppendFormat("realm=\"{0}\"", Realm);
            header.AppendFormat(", nonce=\"{0}\"", Nonce);
            header.AppendFormat(", qop=\"{0}\"", "auth");
            return header.ToString();
        }
    }


    public static class Nonce
    {
        private static readonly ConcurrentDictionary<string, Tuple<int, DateTime>> Nonces =
            new ConcurrentDictionary<String, Tuple<Int32, DateTime>>();

        public static string Generate()
        {
            byte[] bytes = new byte[16];
            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(bytes);
            }
            string nonce = HashHelper.GetMd5(bytes);
            Nonces.TryAdd(nonce, new Tuple<int, DateTime>(0, DateTime.Now.AddMinutes(10)));
            return nonce;
        }

        public static bool IsValid(string nonce, string nonceCount)
        {
            Tuple<int, DateTime> cacheNonce;
            Nonces.TryGetValue(nonce, out cacheNonce);
            if (cacheNonce == null)
            {
                return false;
            }
            if (int.Parse(nonceCount) <= cacheNonce.Item1)
            {
                return false;
            }
            if (cacheNonce.Item2 <= DateTime.Now)
            {
                return false;
            }
            Nonces[nonce] = new Tuple<int, DateTime>(int.Parse(nonceCount), cacheNonce.Item2);
            return true;
        }
    }
}
