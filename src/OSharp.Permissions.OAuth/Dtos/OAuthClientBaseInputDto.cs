// -----------------------------------------------------------------------
//  <copyright file="ClientBaseInputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-04 15:36</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Data;
using OSharp.Core.Security.Models;


namespace OSharp.Core.Security.Dtos
{
    /// <summary>
    /// 客户端输入DTO基类
    /// </summary>
    public abstract class OAuthClientBaseInputDto<TKey> : IInputDto<TKey>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// 获取或设置 应用名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 客户端类型
        /// </summary>
        public OAuthClientType OAuthClientType { get; set; }
        
        /// <summary>
        /// 获取或设置 应用地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 获取或设置 应用Logo地址
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// 获取或设置 重定向地址
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// 获取或设置 需要授权
        /// </summary>
        public bool RequireConsent { get; set; }

        /// <summary>
        /// 获取或设置 允许记住授权
        /// </summary>
        public bool AllowRememberConsent { get; set; }

        /// <summary>
        /// 获取或设置 只允许ClientCrdentials
        /// </summary>
        public bool AllowClientCredentialsOnly { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

    }
}