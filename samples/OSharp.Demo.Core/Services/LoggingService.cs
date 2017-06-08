// -----------------------------------------------------------------------
//  <copyright file="LoggingService.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-30 3:34</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Data;
using OSharp.Core.Logging;
using OSharp.Demo.Contracts;


namespace OSharp.Demo.Services
{
    /// <summary>
    /// 业务实现——日志模块
    /// </summary>
    public partial class LoggingService : ILoggingContract
    {
        /// <summary>
        /// 获取或设置 操作日志仓储对象
        /// </summary>
        public IRepository<OperateLog, int> OperateLogRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 数据日志仓储对象
        /// </summary>
        public IRepository<DataLog, int> DataLogRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 数据日志项仓储对象
        /// </summary>
        public IRepository<DataLogItem, Guid> DataLogItemRepository { protected get; set; }
    }
}