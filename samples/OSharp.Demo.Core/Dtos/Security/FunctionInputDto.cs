// -----------------------------------------------------------------------
//  <copyright file="FunctionInputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-05-06 14:07</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Security;


namespace OSharp.Demo.Dtos.Security
{
    /// <summary>
    /// DTO——功能信息
    /// </summary>
    public class FunctionInputDto : FunctionBaseInputDto<Guid>
    {
        /// <summary>
        /// 获取或设置 是否逻辑删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}