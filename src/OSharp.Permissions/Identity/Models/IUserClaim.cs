// -----------------------------------------------------------------------
//  <copyright file="IUserClaim.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-25 14:04</last-date>
// -----------------------------------------------------------------------

using System;

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;


namespace OSharp.Core.Identity.Models
{
    /// <summary>
    /// 用户摘要标识信息接口
    /// </summary>
    /// <typeparam name="TKey">编号类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    public interface IUserClaim<out TKey, TUser, TUserKey> : IEntity<TKey>
        where TUser : UserBase<TUserKey>
        where TKey : IEquatable<TKey>
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// 获取或设置 摘要类型
        /// </summary>
        string ClaimType { get; set; }

        /// <summary>
        /// 获取或设置 摘要值
        /// </summary>
        string ClaimValue { get; set; }

        /// <summary>
        /// 获取或设置 相关用户
        /// </summary>
        TUser User { get; set; }
    }
}