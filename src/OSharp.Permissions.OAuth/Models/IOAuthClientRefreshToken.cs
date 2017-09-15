// -----------------------------------------------------------------------
//  <copyright file="IClientRefreshToken.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-09 15:55</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Data;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 定义客户端刷新Token信息
    /// </summary>
    public interface IOAuthClientRefreshToken<out TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 获取或设置 Token值
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// 获取或设置 保护的Ticket
        /// </summary>
        string ProtectedTicket { get; set; }

        /// <summary>
        /// 获取或设置 生成时间
        /// </summary>
        DateTime IssuedUtc { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        DateTime? ExpiresUtc { get; set; }
    }
}