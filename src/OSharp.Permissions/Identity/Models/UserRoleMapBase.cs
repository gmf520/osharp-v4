﻿// -----------------------------------------------------------------------
//  <copyright file="UserRoleMapBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-13 17:25</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;


namespace OSharp.Core.Identity.Models
{
    /// <summary>
    /// 用户角色映射信息基类
    /// </summary>
    /// <typeparam name="TKey">编号类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    public abstract class UserRoleMapBase<TKey, TUser, TUserKey, TRole, TRoleKey>
        : ExpirableBase<TKey>,
        IUserRoleMap<TKey, TUser, TUserKey, TRole, TRoleKey>,
        ILockable
        where TUser : IUser<TUserKey>
        where TRole : IRole<TRoleKey>
    {
        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }
        
        /// <summary>
        /// 获取或设置 用户信息
        /// </summary>
        public virtual TUser User { get; set; }

        /// <summary>
        /// 获取或设置 角色信息
        /// </summary>
        public virtual TRole Role { get; set; }
    }
}