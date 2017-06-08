// -----------------------------------------------------------------------
//  <copyright file="UserRoleMapBaseInputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-14 3:37</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Data;


namespace OSharp.Core.Identity.Dtos
{
    /// <summary>
    /// 用户角色映射基类DTO
    /// </summary>
    public abstract class UserRoleMapBaseInputDto<TKey, TUserKey, TRoleKey> : IInputDto<TKey>
    {
        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        public TUserKey UserId { get; set; }

        /// <summary>
        /// 获取或设置 角色编号
        /// </summary>
        public TRoleKey RoleId { get; set; }

        /// <summary>
        /// 获取或设置 生效时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// 验证时间有效性
        /// </summary>
        public void ThrowIfTimeInvalid()
        {
            if (!BeginTime.HasValue || !EndTime.HasValue || BeginTime.Value <= EndTime.Value)
            {
                return;
            }
            throw new IndexOutOfRangeException("生效时间不能大于过期时间");
        }
    }
}