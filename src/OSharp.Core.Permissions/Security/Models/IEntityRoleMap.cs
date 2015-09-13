// -----------------------------------------------------------------------
//  <copyright file="IEntityRoleMap.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-07 2:08</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data;
using OSharp.Core.Identity.Models;
using OSharp.Utility.Filter;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 定义实体角色映射信息
    /// </summary>
    public interface IEntityRoleMap<TKey, TEntityInfo, TEntityInfoKey, TRole, TRoleKey>:IEntity<TKey>
        where TEntityInfo : EntityInfoBase<TEntityInfoKey>
        where TRole : RoleBase<TRoleKey>
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