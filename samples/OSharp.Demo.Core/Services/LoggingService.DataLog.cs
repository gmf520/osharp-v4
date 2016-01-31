// -----------------------------------------------------------------------
//  <copyright file="LoggingService.DataLog.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-30 3:36</last-date>
// -----------------------------------------------------------------------

using System.Linq;

using OSharp.Core.Logging;
using OSharp.Utility.Data;


namespace OSharp.Demo.Services
{
    public partial class LoggingService
    {
        #region Implementation of ILoggingContract

        /// <summary>
        /// 获取 操作日志信息查询数据集
        /// </summary>
        public IQueryable<OperateLog> OperateLogs
        {
            get { return OperateLogRepository.Entities; }
        }

        /// <summary>
        /// 获取 数据日志信息查询数据集
        /// </summary>
        public IQueryable<DataLog> DataLogs
        {
            get { return DataLogRepository.Entities; }
        }

        /// <summary>
        /// 获取 数据日志项信息查询数据集
        /// </summary>
        public IQueryable<DataLogItem> DataLogItems
        {
            get { return DataLogItemRepository.Entities; }
        }

        /// <summary>
        /// 删除操作日志信息信息
        /// </summary>
        /// <param name="ids">要删除的操作日志信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteOperateLogs(params int[] ids)
        {
            return OperateLogRepository.Delete(ids);
        }

        #endregion
    }
}