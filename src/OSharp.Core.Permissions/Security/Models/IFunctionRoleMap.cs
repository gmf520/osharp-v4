// -----------------------------------------------------------------------
//  <copyright file="IFunctionRoleMap.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-04 13:38</last-date>
// -----------------------------------------------------------------------

using System;

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 定义功能角色映射信息
    /// </summary>
    /// <typeparam name="TKey">映射编号类型</typeparam>
    /// <typeparam name="TFunction">功能类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    public interface IFunctionRoleMap<TKey, TFunction, TFunctionKey, TRole, TRoleKey> : IEntity<TKey>
        where TFunction : IFunction, IEntity<TFunctionKey>
        where TRole : IRole<TRoleKey>, IEntity<TRoleKey>
    {
        /// <summary>
        /// 获取或设置 功能信息
        /// </summary>
        TFunction Function { get; set; }

        /// <summary>
        /// 获取或设置 角色信息
        /// </summary>
        TRole Role { get; set; }

        /// <summary>
        /// 获取或设置 验证类型
        /// </summary>
        FilterType FilterType { get; set; }

        /// <summary>
        /// 获取或设置 生效时间
        /// </summary>
        DateTime BeginTime { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        DateTime? EndTime { get; set; }
    }
}