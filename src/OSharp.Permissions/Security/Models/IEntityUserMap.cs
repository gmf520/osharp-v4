// -----------------------------------------------------------------------
//  <copyright file="IEntityUserMap.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-07 2:16</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;
using OSharp.Utility.Filter;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 定义实体用户映射信息
    /// </summary>
    public interface IEntityUserMap<out TKey, TEntityInfo, TEntityInfoKey, TUser, TUserKey> : IEntity<TKey>, ILockable, ICreatedTime
        where TEntityInfo : IEntityInfo, IEntity<TEntityInfoKey>
        where TUser : IUser<TUserKey>, IEntity<TUserKey>
    {
        /// <summary>
        /// 获取或设置 实体信息
        /// </summary>
        TEntityInfo EntityInfo { get; set; }

        /// <summary>
        /// 获取或设置 用户信息
        /// </summary>
        TUser User { get; set; }

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