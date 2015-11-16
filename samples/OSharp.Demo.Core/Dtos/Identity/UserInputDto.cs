// -----------------------------------------------------------------------
//  <copyright file="UserInputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-14 3:38</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

using OSharp.Core.Data;


namespace OSharp.Demo.Dtos.Identity
{
    /// <summary>
    /// 用户信息DTO
    /// </summary>
    public class UserInputDto : IInputDto<int>
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
        /// 获取或设置 登录密码
        /// </summary>
        public string Password { get; set; }

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
        /// 获取或设置 手机号码
        /// </summary>
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 获取或设置 手机号码是否验证
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 获取或设置 注册IP
        /// </summary>
        public string RegistedIp { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }
    }
}