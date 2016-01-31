// -----------------------------------------------------------------------
//  <copyright file="OperateLogConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-04 3:07</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Logging;


namespace OSharp.Data.Entity.Logging
{
    /// <summary>
    /// 操作日志映射配置
    /// </summary>
    public class OperateLogConfiguration : EntityConfigurationBase<OperateLog, int>
    {
        /// <summary>
        /// 获取 相关上下文类型，如为null，将使用默认上下文，否则使用指定的上下文类型
        /// </summary>
        public override Type DbContextType
        {
            get { return typeof(LoggingDbContext); }
        }
    }
}