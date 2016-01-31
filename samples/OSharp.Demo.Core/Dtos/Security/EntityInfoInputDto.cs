// -----------------------------------------------------------------------
//  <copyright file="EntityInfoInputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-14 3:38</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Data;


namespace OSharp.Demo.Dtos.Security
{
    /// <summary>
    /// DTO——实体数据信息
    /// </summary>
    public class EntityInfoInputDto : IInputDto<Guid>
    {
        /// <summary>
        /// 获取 实体数据显示名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 获取 实体数据类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取 是否启用数据日志
        /// </summary>
        public bool DataLogEnabled { get; set; }

        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public Guid Id { get; set; }
    }
}