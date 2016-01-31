// -----------------------------------------------------------------------
//  <copyright file="EntityRoleMapBaseInputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-14 3:37</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Data;
using OSharp.Utility.Filter;


namespace OSharp.Core.Security.Dtos
{
    /// <summary>
    /// 实体角色映射输入DTO基类
    /// </summary>
    public abstract class EntityRoleMapBaseInputDto<TKey, TEntityInfoKey, TRoleKey> : IInputDto<TKey>
    {
        /// <summary>
        /// 获取或设置 实体编号
        /// </summary>
        public TEntityInfoKey EntityInfoId { get; set; }

        /// <summary>
        /// 获取或设置 角色编号
        /// </summary>
        public TRoleKey RoleId { get; set; }

        /// <summary>
        /// 获取或设置 过滤条件组
        /// </summary>
        public FilterGroup FilterGroup { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public TKey Id { get; set; }
    }
}