// -----------------------------------------------------------------------
//  <copyright file="FunctionRoleMapBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-03 19:06</last-date>
// -----------------------------------------------------------------------

using System;

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;
using OSharp.Core.Identity.Models;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 功能角色映射信息基类
    /// </summary>
    /// <typeparam name="TKey">编号类型</typeparam>
    /// <typeparam name="TFunction">功能类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    public abstract class FunctionRoleMapBase<TKey, TFunction, TFunctionKey, TRole, TRoleKey>
        : EntityBase<TKey>, IFunctionRoleMap<TKey, TFunction, TFunctionKey, TRole, TRoleKey>
        where TFunction : FunctionBase<TFunctionKey>
        where TRole : RoleBase<TRoleKey>
    {
        /// <summary>
        /// 获取或设置 功能信息
        /// </summary>
        public virtual TFunction Function { get; set; }

        /// <summary>
        /// 获取或设置 角色信息
        /// </summary>
        public virtual TRole Role { get; set; }

        /// <summary>
        /// 获取或设置 限制类型
        /// </summary>
        public FilterType FilterType { get; set; }

        /// <summary>
        /// 获取或设置 生效时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}