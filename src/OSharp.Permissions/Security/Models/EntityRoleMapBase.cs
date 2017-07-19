// -----------------------------------------------------------------------
//  <copyright file="EntityRoleMapBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-07 2:16</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations.Schema;

using OSharp.Core.Data;
using OSharp.Core.Identity.Models;
using OSharp.Utility.Extensions;
using OSharp.Utility.Filter;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 实体角色映射信息基类
    /// </summary>
    public abstract class EntityRoleMapBase<TKey, TEntityInfo, TEntityInfoKey, TRole, TRoleKey>
        : EntityBase<TKey>, IEntityRoleMap<TKey, TEntityInfo, TEntityInfoKey, TRole, TRoleKey>
        where TEntityInfo : EntityInfoBase<TEntityInfoKey>
        where TRole : RoleBase<TRoleKey>
        where TKey : IEquatable<TKey>
        where TEntityInfoKey : IEquatable<TEntityInfoKey>
        where TRoleKey : IEquatable<TRoleKey>
    {
        /// <summary>
        /// 获取或设置 实体信息
        /// </summary>
        public virtual TEntityInfo EntityInfo { get; set; }

        /// <summary>
        /// 获取或设置 角色信息
        /// </summary>
        public virtual TRole Role { get; set; }

        /// <summary>
        /// 获取或设置 过滤条件组Json字符串
        /// </summary>
        public string FilterGroupJson { get; set; }

        /// <summary>
        /// 获取 过滤条件组信息
        /// </summary>
        [NotMapped]
        public FilterGroup FilterGroup
        {
            get
            {
                if (FilterGroupJson.IsNullOrEmpty())
                {
                    return null;
                }
                return FilterGroupJson.FromJsonString<FilterGroup>();
            }
        }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}