// -----------------------------------------------------------------------
//  <copyright file="ICreatedTime.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-21 14:34</last-date>
// -----------------------------------------------------------------------

using System;


namespace OSharp.Core.Data
{
    /// <summary>
    /// 表示实体将包含创建时间，在创建实体时，将自动提取当前时间为创建时间
    /// </summary>
    public interface ICreatedTime
    {
        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        DateTime CreatedTime { get; set; }
    }
}