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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 客户端信息基类
    /// </summary>
    public abstract class ClientBase<TKey, TClientSecret, TClientSecretKey, TClientRedirectUri, TClientRedirectUriKey> 
        : EntityBase<TKey>, IClient<TKey>, ILockable, ICreatedTime
        where TClientSecret : IClientSecret<TClientSecretKey>
        where TClientRedirectUri : IClientRedirectUri<TClientRedirectUriKey>
    {
        /// <summary>
        /// 获取或设置 应用名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 客户端编号
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 获取或设置 应用地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 获取或设置 应用Logo地址
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// 获取或设置 需要授权
        /// </summary>
        public bool RequireConsent { get; set; }

        /// <summary>
        /// 获取或设置 允许记住授权
        /// </summary>
        public bool AllowRememberConsent { get; set; }

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

        /// <summary>
        /// 获取或设置 客户端重定向地址集合
        /// </summary>
        public virtual ICollection<TClientRedirectUri> ClientRedirectUris { get; set; }
    }
}