// -----------------------------------------------------------------------
//  <copyright file="FunctionUserMapBaseInputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-14 3:37</last-date>
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
    /// 功能用户映射基类Dto
    /// </summary>
    public abstract class FunctionUserMapBaseInputDto<TKey, TFunctionKey, TUserKey> : IInputDto<TKey>
    {
        /// <summary>
        /// 获取或设置 功能编号
        /// </summary>
        public TFunctionKey FunctionId { get; set; }

        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        public TUserKey UserId { get; set; }

        /// <summary>
        /// 获取或设置 限制类型
        /// </summary>
        public FilterType FilterType { get; set; }

        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public TKey Id { get; set; }
    }
}