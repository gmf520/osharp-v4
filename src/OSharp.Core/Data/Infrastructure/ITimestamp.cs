// -----------------------------------------------------------------------
//  <copyright file="ITimestamp.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-25 15:01</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OSharp.Core.Data
{
    /// <summary>
    /// 用于获取或设置 版本控制标识,用于处理并发
    /// </summary>
    public interface ITimestamp
    {
        /// <summary>
        /// 获取或设置 版本控制标识，用于处理并发
        /// </summary>
        byte[] Timestamp { get; set; }
    }
}