// -----------------------------------------------------------------------
//  <copyright file="FunctionRoleMapDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-04 11:45</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data;


namespace OSharp.Core.Security.Dtos
{
    /// <summary>
    /// 功能角色映射基类DTO
    /// </summary>
    public abstract class FunctionRoleMapBaseDto<TKey, TFunctionKey, TRoleKey> : IAddDto, IEditDto<TKey>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// 获取或设置 功能编号
        /// </summary>
        public TFunctionKey FunctionId { get; set; }

        /// <summary>
        /// 获取或设置 角色编号
        /// </summary>
        public TRoleKey RoleId { get; set; }

        /// <summary>
        /// 获取或设置 限制类型
        /// </summary>
        public FilterType FilterType { get; set; }

        /// <summary>
        /// 获取或设置 生效时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}