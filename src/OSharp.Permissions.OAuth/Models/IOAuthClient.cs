// -----------------------------------------------------------------------
//  <copyright file="IClient.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-31 16:12</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Data;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 定义OAuth客户端应用信息
    /// </summary>
    public interface IOAuthClient<out TKey> : IEntity<TKey>
    {
        /// <summary>
        /// 获取或设置 应用名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 获取或设置 客户端类型
        /// </summary>
        OAuthClientType OAuthClientType { get; set; }

        /// <summary>
        /// 获取或设置 客户端编号
        /// </summary>
        string ClientId { get; set; }

        /// <summary>
        /// 获取或设置 应用地址
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// 获取或设置 应用Logo地址
        /// </summary>
        string LogoUrl { get; set; }

        /// <summary>
        /// 获取或设置 重定向地址
        /// </summary>
        string RedirectUrl { get; set; }
        
        /// <summary>
        /// 获取或设置 需要授权
        /// </summary>
        bool RequireConsent { get; set; }

        /// <summary>
        /// 获取或设置 允许记住授权
        /// </summary>
        bool AllowRememberConsent { get; set; }

        /// <summary>
        /// 获取或设置 只允许ClientCrdentials
        /// </summary>
        bool AllowClientCredentialsOnly { get; set; }
    }
}