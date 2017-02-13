using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;

using Newtonsoft.Json.Linq;

using OSharp.Utility.Secutiry;
using OSharp.Web.Http.Internal;


namespace OSharp.Web.SignalR
{
    /// <summary>
    /// 支持加密传输的<see cref="HubConnection"/>
    /// </summary>
    public class ClientCryptoHubConnection : HubConnection
    {
        private CommunicationCryptor _cryptor;


        /// <summary>
        /// Initializes a new instance of the <see cref="T:Microsoft.AspNet.SignalR.Client.HubConnection"/> class.
        /// </summary>
        /// <param name="url">The url to connect to.</param>
        public ClientCryptoHubConnection(string url)
            : base(url)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Microsoft.AspNet.SignalR.Client.HubConnection"/> class.
        /// </summary>
        /// <param name="url">The url to connect to.</param><param name="useDefaultUrl">Determines if the default "/signalr" path should be appended to the specified url.</param>
        public ClientCryptoHubConnection(string url, bool useDefaultUrl)
            : base(url, useDefaultUrl)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Microsoft.AspNet.SignalR.Client.HubConnection"/> class.
        /// </summary>
        /// <param name="url">The url to connect to.</param><param name="queryString">The query string data to pass to the server.</param>
        public ClientCryptoHubConnection(string url, string queryString)
            : base(url, queryString)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Microsoft.AspNet.SignalR.Client.HubConnection"/> class.
        /// </summary>
        /// <param name="url">The url to connect to.</param><param name="queryString">The query string data to pass to the server.</param><param name="useDefaultUrl">Determines if the default "/signalr" path should be appended to the specified url.</param>
        public ClientCryptoHubConnection(string url, string queryString, bool useDefaultUrl)
            : base(url, queryString, useDefaultUrl)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Microsoft.AspNet.SignalR.Client.HubConnection"/> class.
        /// </summary>
        /// <param name="url">The url to connect to.</param><param name="queryString">The query string data to pass to the server.</param>
        public ClientCryptoHubConnection(string url, IDictionary<string, string> queryString)
            : base(url, queryString)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Microsoft.AspNet.SignalR.Client.HubConnection"/> class.
        /// </summary>
        /// <param name="url">The url to connect to.</param><param name="queryString">The query string data to pass to the server.</param><param name="useDefaultUrl">Determines if the default "/signalr" path should be appended to the specified url.</param>
        public ClientCryptoHubConnection(string url, IDictionary<string, string> queryString, bool useDefaultUrl)
            : base(url, queryString, useDefaultUrl)
        { }

        /// <summary>
        /// 加密初始化
        /// </summary>
        /// <param name="facePublicKey">对方公钥</param>
        /// <param name="hashType">摘要哈希方式，值必须为MD5或SHA1</param>
        public void CryptoInitialize(string facePublicKey, string hashType)
        {
            if (_cryptor == null)
            {
                RsaHelper ownRsa = new RsaHelper();
                Headers.Add(HttpHeaderNames.OSharpClientPublicKey, ownRsa.PublicKey);
                _cryptor = new CommunicationCryptor(ownRsa.PrivateKey, facePublicKey, hashType);
            }
        }

        /// <summary>
        /// 客户端往服务端发送数据时对数据进行加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Task Send(string data)
        {
            if (_cryptor == null)
            {
                throw new InvalidOperationException("通信加密尚未初始化。");
            }
            //加密传输数据
            HubInvocation hubData = this.JsonDeserializeObject<HubInvocation>(data);
            JToken[] args = hubData.Args;
            if (args != null && args.Length > 0)
            {
                string encrypt = _cryptor.EncryptData(this.JsonSerializeObject(args));
                List<JToken> @params = new List<JToken>() { JToken.FromObject(encrypt, JsonSerializer) };
                if (args.Length > 1)
                {
                    @params = @params.Concat(new JToken[args.Length - 1]).ToList();
                }
                hubData.Args = @params.ToArray();
                data = this.JsonSerializeObject(hubData);
            }

            return base.Send(data);
        }

        /// <summary>
        /// 接收到服务端数据之后对数据进行解密
        /// </summary>
        /// <param name="message"></param>
        protected override void OnMessageReceived(JToken message)
        {
            if (_cryptor == null)
            {
                throw new InvalidOperationException("通信加密尚未初始化。");
            }
            if (message["P"] == null && message["I"] == null)
            {
                HubInvocation invocation = message.ToObject<HubInvocation>(JsonSerializer);
                if (invocation.Args.Length == 1)
                {
                    string encrypt = invocation.Args[0].ToString();
                    if (!encrypt.StartsWith("{") && !encrypt.StartsWith("["))
                    {
                        string json = _cryptor.DecryptAndVerifyData(encrypt);
                        JToken[] args = this.JsonDeserializeObject<JToken[]>(json);
                        message["A"] = JToken.FromObject(args, JsonSerializer);
                    }
                }
            }

            base.OnMessageReceived(message);
        }

    }
}
