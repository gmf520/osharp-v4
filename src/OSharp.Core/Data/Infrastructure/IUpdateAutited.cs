// -----------------------------------------------------------------------
//  <copyright file="IUpdateAutited.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-21 14:55</last-date>
// -----------------------------------------------------------------------

using System;


namespace OSharp.Core.Data
{
    /// <summary>
    /// 表示实体将包含更新者，更新时间属性，将在实体更新时自动赋值
    /// </summary>
    public interface IUpdateAudited
    {
        /// <summary>
        /// 获取或设置 最后更新时间
        /// </summary>
        DateTime? LastUpdatedTime { get; set; }

        /// <summary>
        /// 获取或设置 最后更新者编号
        /// </summary>
        string LastUpdatorUserId { get; set; }
    }
}