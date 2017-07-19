// -----------------------------------------------------------------------
//  <copyright file="ClientSecretBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-31 15:45</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

using OSharp.Core.Data;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 客户端密钥信息基类
    /// </summary>
    public abstract class OAuthClientSecretBase<TKey, TClient, TClientKey> : ExpirableBase<TKey>, IOAuthClientSecret<TKey>
        where TClient : IOAuthClient<TClientKey>
        where TKey : IEquatable<TKey>
        where TClientKey : IEquatable<TClientKey>
    {
        /// <summary>
        /// 获取或设置 密钥值
        /// </summary>
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// 获取或设置 密钥类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 获取或设置 描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 归属客户端
        /// </summary>
        public virtual TClient Client { get; set; }
    }
}