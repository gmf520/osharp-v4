// -----------------------------------------------------------------------
//  <copyright file="ModuleBaseInputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-13 0:55</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using OSharp.Core.Data;


namespace OSharp.Core.Security.Dtos
{
    /// <summary>
    /// 模块信息基类输入DTO
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ModuleBaseInputDto<TKey> : IInputDto<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 获取或设置 模块名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 模块描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 排序码
        /// </summary>
        public double OrderCode { get; set; }

        /// <summary>
        /// 获取或设置 父模块编号
        /// </summary>
        public TKey ParentId { get; set; }

        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public TKey Id { get; set; }
    }
}