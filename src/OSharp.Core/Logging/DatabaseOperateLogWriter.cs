// -----------------------------------------------------------------------
//  <copyright file="DatabaseOperateLogWriter.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-23 9:08</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Data;
using OSharp.Utility;


namespace OSharp.Core.Logging
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
            operateLog.CheckNotNull("operateLog");
            _operateLogRepository.Insert(operateLog);
        }
    }
}