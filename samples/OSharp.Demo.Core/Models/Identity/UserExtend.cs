// -----------------------------------------------------------------------
//  <copyright file="UserExtend.cs" company="OSharp开源团队">
//      Copyright (c) 2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-01-08 0:20</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using OSharp.Core.Data;


namespace OSharp.Demo.Models.Identity
{
    /// <summary>
    /// 实体类——用户扩展信息
    /// </summary>
    [Description("认证-用户扩展信息")]
    public class UserExtend : IEntity<int>
    {
        /// <summary>
        /// 获取或设置 注册IP地址
        /// </summary>
        [StringLength(18)]
        public string RegistedIp { get; set; }

        /// <summary>
        /// 获取或设置 用户信息
        /// </summary>
        [Required]
        public virtual User User { get; set; }

        /// <summary>
        /// 获取或设置 实体唯一标识，主键
        /// </summary>
        [Key]
        public int Id { get; set; }

    }
}