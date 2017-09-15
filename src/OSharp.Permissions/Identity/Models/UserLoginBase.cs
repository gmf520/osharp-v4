// -----------------------------------------------------------------------
//  <copyright file="UserLoginBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-25 12:06</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Data;


namespace OSharp.Core.Identity.Models
{
    /// <summary>
    /// 用户第三方登录（OAuth，如facebook,google）信息基类
    /// </summary>
    /// <typeparam name="TKey">编号类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    public abstract class UserLoginBase<TKey, TUser, TUserKey> : EntityBase<TKey>, IUserLogin<TKey, TUser, TUserKey>, ICreatedTime
        where TUser : UserBase<TUserKey>
        where TKey : IEquatable<TKey>
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// 获取或设置 第三方登录提供者
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// 获取或设置 第三方登录密钥
        /// </summary>
        public string ProviderKey { get; set; }

        /// <summary>
        /// 获取设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 相关用户信息
        /// </summary>
        public virtual TUser User { get; set; }
    }
}