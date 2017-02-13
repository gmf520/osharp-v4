// -----------------------------------------------------------------------
//  <copyright file="IUserLogin.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-25 13:59</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;


namespace OSharp.Core.Identity.Models
{
    /// <summary>
    /// 用户第三方登录（OAuth，如facebook,google）信息接口
    /// </summary>
    /// <typeparam name="TKey">编号类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    public interface IUserLogin<out TKey, TUser, TUserKey> : IEntity<TKey>
        where TUser : UserBase<TUserKey>
    {
        /// <summary>
        /// 获取或设置 第三方登录提供者
        /// </summary>
        string LoginProvider { get; set; }

        /// <summary>
        /// 获取或设置 第三方登录密钥
        /// </summary>
        string ProviderKey { get; set; }

        /// <summary>
        /// 获取或设置 相关用户信息
        /// </summary>
        TUser User { get; set; }
    }
}