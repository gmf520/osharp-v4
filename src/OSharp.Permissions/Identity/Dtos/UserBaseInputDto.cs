// -----------------------------------------------------------------------
//  <copyright file="UserBaseInputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-20 2:47</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

using OSharp.Core.Data;


namespace OSharp.Core.Identity.Dtos
{
    /// <summary>
    /// 用户输入DTO基类
    /// </summary>
    public class UserBaseInputDto<TKey> : IInputDto<TKey>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// 获取或设置 用户名
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
        [StringLength(50)]
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置 手机号码
        /// </summary>
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 获取或设置 手机号码是否验证
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

    }
}