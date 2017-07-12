// -----------------------------------------------------------------------
//  <copyright file="IEntityRoleMap.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-07 2:08</last-date>
// -----------------------------------------------------------------------

using System;

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;
using OSharp.Core.Identity.Models;
using OSharp.Utility.Filter;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 定义实体角色映射信息
    /// </summary>
    public interface IEntityRoleMap<out TKey, TEntityInfo, TEntityInfoKey, TRole, TRoleKey> : IEntity<TKey>, ILockable, ICreatedTime
        where TEntityInfo : IEntityInfo, IEntity<TEntityInfoKey>
        where TRole : IRole<TRoleKey>, IEntity<TRoleKey>
        where TKey : IEquatable<TKey>
        where TEntityInfoKey : IEquatable<TEntityInfoKey>
        where TRoleKey : IEquatable<TRoleKey>
    {
        /// <summary>
        /// 获取或设置 实体信息
        /// </summary>
        TEntityInfo EntityInfo { get; set; }

        /// <summary>
        /// 获取或设置 角色信息
        /// </summary>
        TRole Role { get; set; }

        /// <summary>
        /// 获取或设置 过滤条件组Json字符串
        /// </summary>
        string FilterGroupJson { get; set; }

        /// <summary>
        /// 获取 过滤条件组信息
        /// </summary>
        FilterGroup FilterGroup { get; }
    }
}