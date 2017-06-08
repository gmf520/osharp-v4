// -----------------------------------------------------------------------
//  <copyright file="DataLogItemConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-29 22:21</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Logging;


namespace OSharp.Data.Entity.Logging
{
    /// <summary>
    /// 实体操作明细映射配置
    /// </summary>
    public class DataLogItemConfiguration : EntityConfigurationBase<DataLogItem, Guid>
    {
        /// <summary>
        /// 初始化一个<see cref="DataLogItemConfiguration"/>类型的新实例
        /// </summary>
        public DataLogItemConfiguration()
        {
            HasRequired(m => m.DataLog).WithMany(n => n.LogItems).WillCascadeOnDelete();
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