// -----------------------------------------------------------------------
//  <copyright file="UserBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-24 0:05</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;


namespace OSharp.Core.Identity.Models
{
    /// <summary>
    /// 用户信息基类
    /// </summary>
    /// <typeparam name="TKey">用户编号类型</typeparam>
    public abstract class UserBase<TKey> : EntityBase<TKey>, IUser<TKey>, ICreatedTime, ILockable
    {
        /// <summary>
        /// Unique username
        /// </summary>
        [Required, StringLength(100)]
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置 用户昵称
        /// </summary>
        [StringLength(100)]
        public string NickName { get; set; }

        /// <summary>
        /// 获取或设置 电子邮箱
        /// </summary>
        [StringLength(200)]
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置 电子邮箱是否验证
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// 获取或设置 密码哈希
        /// </summary>
        [StringLength(200)]
        public string PasswordHash { get; set; }

        /// <summary>
        /// 获取或设置 安全标识，当用户认证过期（修改密码、退出等）时将变更的随机值
        /// </summary>
        [StringLength(200)]
        public string SecurityStmp { get; set; }

        /// <summary>
        /// 获取或设置 手机号码
        /// </summary>
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 获取或设置 手机号码是否验证
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 获取或设置 是否开启二元认证
        /// </summary>
        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// 获取或设置 登录锁定UTC时间，在此时间前登录将被锁定
        /// </summary>
        public DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// 获取或设置 是否允许登录锁定
        /// </summary>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// 获取或设置 当前登录失败次数，达到设定值将被锁定
        /// </summary>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定，用于冻结
        /// </summary>
        public bool IsLocked { get; set; }
    }
}