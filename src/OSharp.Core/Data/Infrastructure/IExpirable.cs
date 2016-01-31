// -----------------------------------------------------------------------
//  <copyright file="IExpirable.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-21 21:54</last-date>
// -----------------------------------------------------------------------

using System;


namespace OSharp.Core.Data
{
    /// <summary>
    /// 定义可过期性，包含生效时间与过期时间
    /// </summary>
    public interface IExpirable
    {
        /// <summary>
        /// 获取或设置 生效时间
        /// </summary>
        DateTime? BeginTime { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        DateTime? EndTime { get; set; }
    }
}