// -----------------------------------------------------------------------
//  <copyright file="EntityUserMapBaseDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-07 2:22</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data;
using OSharp.Utility.Filter;


namespace OSharp.Core.Security.Dtos
{
    /// <summary>
    /// 实体用户映射基类DTO
    /// </summary>
    public abstract class EntityUserMapBaseDto<TKey, TEntityInfoKey, TUserKey> : IAddDto, IEditDto<TKey>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// 获取或设置 实体编号
        /// </summary>
        public TEntityInfoKey EntityInfoId { get; set; }

        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        public TUserKey UserId { get; set; }

        /// <summary>
        /// 获取或设置 过滤条件组
        /// </summary>
        public FilterGroup FilterGroup { get; set; }
    }
}