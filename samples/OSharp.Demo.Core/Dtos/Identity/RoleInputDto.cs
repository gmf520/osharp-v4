// -----------------------------------------------------------------------
//  <copyright file="RoleInputDto.cs" company="OSharp开源团队">
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
    public class RoleInputDto : IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 角色名称
        /// </summary>
        [Required, StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 角色描述
        /// </summary>
        [StringLength(500)]
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 是否是管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 获取或设置 是否系统角色
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 组织机构编号
        /// </summary>
        public int? OrganizationId { get; set; }

        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }
    }
}