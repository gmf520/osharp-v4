// -----------------------------------------------------------------------
//  <copyright file="RefreshTokenInfo.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-10 2:42</last-date>
// -----------------------------------------------------------------------

using System;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 刷新Token信息
    /// </summary>
    public class RefreshTokenInfo
    {
        /// <summary>
        /// 获取或设置 Token值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 获取或设置 相关用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置 相关客户端编号
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 获取或设置 发布时间
        /// </summary>
        public DateTime IssuedUtc { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        public DateTime? ExpiresUtc { get; set; }

        /// <summary>
        /// 获取或设置 加密票据
        /// </summary>
        public string ProtectedTicket { get; set; }
    }
}