// -----------------------------------------------------------------------
//  <copyright file="OrganizationDto.cs" company="OSharp开源团队">
//      Copyright (c) 2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-01-08 0:31</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

using OSharp.Core.Data;


namespace OSharp.Demo.Dtos.Identity
{
    public class OrganizationDto : IAddDto, IEditDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Remark { get; set; }

        [Range(0, 999)]
        public int SortCode { get; set; }

        public int? ParentId { get; set; }
    }
}