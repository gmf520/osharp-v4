// -----------------------------------------------------------------------
//  <copyright file="OperateLog.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-04 2:35</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using OSharp.Core.Context;
using OSharp.Core.Data;


namespace OSharp.Core.Logging
{
    /// <summary>
    /// 操作日志信息类
    /// </summary>
    [Description("系统-操作日志信息")]
    public class OperateLog : EntityBase<int>, ICreatedTime
    {
        /// <summary>
        /// 初始化一个<see cref="OperateLog"/>类型的新实例
        /// </summary>
        public OperateLog()
        {
            DataLogs = new List<DataLog>();
        }

        /// <summary>
        /// 获取或设置 执行的功能名称
        /// </summary>
        [StringLength(100)]
        public string FunctionName { get; set; }

        /// <summary>
        /// 获取或设置 操作人信息
        /// </summary>
        public Operator Operator { get; set; }

        /// <summary>
        /// 获取设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 数据日志集合
        /// </summary>
        public virtual ICollection<DataLog> DataLogs { get; set; }
    }
}