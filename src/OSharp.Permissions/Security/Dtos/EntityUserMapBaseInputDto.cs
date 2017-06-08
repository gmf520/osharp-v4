// -----------------------------------------------------------------------
//  <copyright file="EntityUserMapBaseInputDto.cs" company="OSharp开源团队">
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
    /// 实体用户映射基类输入DTO
    /// </summary>
    public abstract class EntityUserMapBaseInputDto<TKey, TEntityInfoKey, TUserKey> : IInputDto<TKey>
    {
        /// <summary>
        /// 获取或设置 实体编号
        /// </summary>
        public TEntityInfoKey EntityInfoId { get; set; }

        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        public TUserKey UserId { get; set; }

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