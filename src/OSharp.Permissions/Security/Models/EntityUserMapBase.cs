// -----------------------------------------------------------------------
//  <copyright file="EntityUserMapBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-07 2:17</last-date>
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
    /// 实体用户映射信息基类
    /// </summary>
    public abstract class EntityUserMapBase<TKey, TEntityInfo, TEntityInfoKey, TUser, TUserKey>
        : EntityBase<TKey>, IEntityUserMap<TKey, TEntityInfo, TEntityInfoKey, TUser, TUserKey>
        where TEntityInfo : EntityInfoBase<TEntityInfoKey>
        where TUser : UserBase<TUserKey>
        where TKey : IEquatable<TKey>
        where TEntityInfoKey : IEquatable<TEntityInfoKey>
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// 获取或设置 实体信息
        /// </summary>
        public virtual TEntityInfo EntityInfo { get; set; }

        /// <summary>
        /// 获取或设置 用户信息
        /// </summary>
        public virtual TUser User { get; set; }

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