// -----------------------------------------------------------------------
//  <copyright file="ClientRefreshTokenBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-09 16:13</last-date>
// -----------------------------------------------------------------------

using System;

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 实体类——客户端刷新Token信息，用于刷新AccessToken
    /// </summary>
    public abstract class OAuthClientRefreshTokenBase<TKey, TClient, TClientKey, TUser, TUserKey> : EntityBase<TKey>, IOAuthClientRefreshToken<TKey>
        where TClient : IOAuthClient<TClientKey>
        where TUser : IUser<TUserKey>
    {
        /// <summary>
        /// 获取或设置 Token值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 获取或设置 保护的Ticket
        /// </summary>
        public string ProtectedTicket { get; set; }

        /// <summary>
        /// 获取或设置 生成时间
        /// </summary>
        public DateTime IssuedUtc { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        public DateTime? ExpiresUtc { get; set; }
        
        /// <summary>
        /// 获取或设置 相关客户端信息
        /// </summary>
        public virtual TClient Client { get; set; }

        /// <summary>
        /// 获取或设置 相关用户信息
        /// </summary>
        public virtual TUser User { get; set; }
    }
}