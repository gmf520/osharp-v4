// -----------------------------------------------------------------------
//  <copyright file="DataLogConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-29 22:21</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Logging;


namespace OSharp.Core.Data.Entity.Logging
{
    /// <summary>
    /// 数据日志映射配置
    /// </summary>
    public class DataLogConfiguration : EntityConfigurationBase<DataLog, int>
    {
        /// <summary>
        /// 初始化一个<see cref="DataLogConfiguration"/>类型的新实例
        /// </summary>
        public DataLogConfiguration()
        {
            HasOptional(m => m.OperateLog).WithMany(n => n.DataLogs).WillCascadeOnDelete(true);
        }

        /// <summary>
        /// 获取 相关上下文类型，如为null，将使用默认上下文，否则使用指定的上下文类型
        /// </summary>
        public override Type DbContextType
        {
            get { return typeof(LoggingDbContext); }
        }

    }
}