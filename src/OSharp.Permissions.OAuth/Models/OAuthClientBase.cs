// -----------------------------------------------------------------------
//  <copyright file="ClientBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-31 17:29</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using OSharp.Core.Data;

namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 客户端信息基类
    /// </summary>
    public abstract class OAuthClientBase<TKey, TClientSecret, TClientSecretKey> 
        : EntityBase<TKey>, IOAuthClient<TKey>, ILockable, ICreatedTime
        where TClientSecret : IOAuthClientSecret<TClientSecretKey>
        where TKey : IEquatable<TKey>
        where TClientSecretKey : IEquatable<TClientSecretKey>
    {
        /// <summary>
        /// 初始化一个<see cref="OAuthClientBase{TKey,TClientSecret,TClientSecretKey}"/>类型的新实例
        /// </summary>
        protected OAuthClientBase()
        {
            ClientSecrets = new List<TClientSecret>();
        }

        /// <summary>
        /// 获取或设置 应用名称
        /// </summary>
        [Required, StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 客户端类型
        /// </summary>
        public OAuthClientType OAuthClientType { get; set; }

        /// <summary>
        /// 获取或设置 客户端编号
        /// </summary>
        [Required, StringLength(100)]
        public string ClientId { get; set; }

        /// <summary>
        /// 获取或设置 应用地址
        /// </summary>
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// 获取或设置 应用Logo地址
        /// </summary>
        [Required]
        public string LogoUrl { get; set; }

        /// <summary>
        /// 获取或设置 重定向地址
        /// </summary>
        [Required]
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

        /// <summary>
        /// 获取设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 客户端密钥集合
        /// </summary>
        public virtual ICollection<TClientSecret> ClientSecrets { get; set; }
        
    }
}