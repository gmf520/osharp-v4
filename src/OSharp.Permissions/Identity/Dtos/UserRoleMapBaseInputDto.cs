// -----------------------------------------------------------------------
//  <copyright file="UserRoleMapBaseInputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-14 3:37</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data;


namespace OSharp.Core.Identity.Dtos
{
    /// <summary>
    /// 用户角色映射基类DTO
    /// </summary>
    public abstract class UserRoleMapBaseInputDto<TKey, TUserKey, TRoleKey> : IInputDto<TKey>
    {
        private DateTime? _beginTime;
        private DateTime? _endTime;

        /// <summary>
        /// 
        /// </summary>
        protected UserRoleMapBaseInputDto()
        {
            _beginTime = DateTime.Now;
        }

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
        public DateTime? BeginTime
        {
            get { return _beginTime; }
            set
            {
                if (EndTime != null && value > EndTime.Value)
                {
                    throw new InvalidOperationException("生效时间不能大于过期时间");
                }
                _beginTime = value;
            }
        }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        public DateTime? EndTime
        {
            get { return _endTime; }
            set
            {
                if (value != null && BeginTime != null && value < BeginTime)
                {
                    throw new InvalidOperationException("过期时间不能小于生效时间");
                }
                _endTime = value;
            }
        }

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