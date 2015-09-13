// -----------------------------------------------------------------------
//  <copyright file="DatabaseOperateLogWriter.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-06 1:38</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data;
using OSharp.Core.Logging;
using OSharp.Utility;


namespace OSharp.SiteBase.Logging
{
    /// <summary>
    /// 操作日志数据库输出实现
    /// </summary>
    public class DatabaseOperateLogWriter : IOperateLogWriter
    {
        private readonly IRepository<OperateLog, int> _operateLogRepository;

        /// <summary>
        /// 初始化一个<see cref="DatabaseOperateLogWriter"/>类型的新实例
        /// </summary>
        public DatabaseOperateLogWriter(IRepository<OperateLog, int> operateLogRepository)
        {
            _operateLogRepository = operateLogRepository;
        }

        /// <summary>
        /// 输出操作日志
        /// </summary>
        /// <param name="operateLog">操作日志信息</param>
        public void Write(OperateLog operateLog)
        {
            operateLog.CheckNotNull("operateLog" );
            _operateLogRepository.Insert(operateLog);
        }
    }
}