// -----------------------------------------------------------------------
//  <copyright file="TokenResponse.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-06 8:07</last-date>
// -----------------------------------------------------------------------

using Newtonsoft.Json.Linq;


namespace OSharp.Web.Http.OAuth
{
    /// <summary>
    /// OAuth2Token
    /// </summary>
    public class OAuth2Token
    {
        /// <summary>
        /// 初始化一个<see cref="OAuth2Token"/>类型的新实例
        /// </summary>
        public OAuth2Token()
        { }

        /// <summary>
        /// 初始化一个<see cref="OAuth2Token"/>类型的新实例
        /// </summary>
        public OAuth2Token(JObject obj)
        {
            JToken value;
            TokenType = obj.TryGetValue("token_type", out value) ? (string)value : null;
            AccessToken = obj.TryGetValue("access_token", out value) ? (string)value : null;
            RefreshToken = obj.TryGetValue("refresh_token", out value) ? (string)value : null;
            ExpiresIn = obj.TryGetValue("expires_in", out value) ? (int)value : 0;
            Error = obj.TryGetValue("error", out value) ? (string)value : null;
            ErrorDescription = obj.TryGetValue("error_description", out value) ? (string)value : null;
            HasError = Error != null;
        }

        /// <summary>
        /// 获取或设置 Token类型
        /// </summary>
        public string TokenType { get; set; }

        /// <summary>
        /// 获取或设置 访问Token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 获取或设置 刷新Token
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 获取或设置 访问Token过期剩余秒数
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// 获取或设置 错误标题
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// 获取或设置 错误描述
        /// </summary>
        public string ErrorDescription { get; set; }

        /// <summary>
        /// 获取或设置 是否有错
        /// </summary>
        public bool HasError { get; set; }
    }
}