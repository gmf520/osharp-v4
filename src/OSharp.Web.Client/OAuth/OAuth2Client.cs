// -----------------------------------------------------------------------
//  <copyright file="OAuth2Client.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-06 2:39</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;


namespace OSharp.Web.OAuth
{
    /// <summary>
    /// OAuth2客户端
    /// </summary>
    public class OAuth2Client : HttpClient
    {
        /// <summary>
        /// 初始化一个<see cref="OAuth2Client"/>类型的新实例
        /// </summary>
        public OAuth2Client(string url)
            :
                this(new Uri(url))
        { }

        /// <summary>
        /// 初始化一个<see cref="OAuth2Client"/>类型的新实例
        /// </summary>
        public OAuth2Client(Uri uri)
            : this(uri, null, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="OAuth2Client"/>类型的新实例
        /// </summary>
        public OAuth2Client(Uri uri, string clientId, string clientSecret)
        {
            BaseAddress = uri;
            ClientId = clientId;
            ClientSecret = clientSecret;
            TokenPath = "token";
            AuthorizePath = "authorize";
        }

        /// <summary>
        /// 获取或设置 客户端编号
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 获取或设置 客户端密钥
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// 获取或设置 Token路径
        /// </summary>
        public string TokenPath { get; set; }

        /// <summary>
        /// 获取或设置 Authorize路径
        /// </summary>
        public string AuthorizePath { get; set; }

        /// <summary>
        /// 请求Token
        /// </summary>
        /// <returns></returns>
        public async Task<OAuth2Token> RequestToken()
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes(ClientId + ":" + ClientSecret)));
            IDictionary<string, string> paramters = new Dictionary<string, string>();
            paramters.Add(GrantTypes.ClientCredentials);
            HttpResponseMessage response = await this.PostAsync(TokenPath, new FormUrlEncodedContent(paramters));
            JObject obj = await response.Content.ReadAsAsync<JObject>();
            OAuth2Token token = new OAuth2Token(obj);
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            return token;
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        public async Task<OAuth2Token> RefreshToken(string refreshToken)
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes(ClientId + ":" + ClientSecret)));
            IDictionary<string, string> paramters = new Dictionary<string, string>();
            paramters.Add(GrantTypes.RefreshToken);
            paramters.Add("refresh_token", refreshToken);
            HttpResponseMessage response = await this.PostAsync(TokenPath, new FormUrlEncodedContent(paramters));
            JObject obj = await response.Content.ReadAsAsync<JObject>();
            OAuth2Token token = new OAuth2Token(obj);
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            return token;
        }
    }
}