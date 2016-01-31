// -----------------------------------------------------------------------
//  <copyright file="ILoggingContract.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-30 3:32</last-date>
// -----------------------------------------------------------------------

using System.Linq;

using OSharp.Core.Dependency;
using OSharp.Core.Logging;
using OSharp.Utility.Data;


namespace OSharp.Demo.Contracts
{
    /// <summary>
    /// 业务契约——日志模块
    /// </summary>
    public interface ILoggingContract : IScopeDependency
    {
        #region 数据日志信息业务

        /// <summary>
        /// 获取 操作日志信息查询数据集
        /// </summary>
        IQueryable<OperateLog> OperateLogs { get; }

        /// <summary>
        /// 获取 数据日志信息查询数据集
        /// </summary>
        IQueryable<DataLog> DataLogs { get; }

        /// <summary>
        /// 获取 数据日志项信息查询数据集
        /// </summary>
        IQueryable<DataLogItem> DataLogItems { get; }

        /// <summary>
        /// 删除操作日志信息信息
        /// </summary>
        /// <param name="ids">要删除的操作日志信息编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteOperateLogs(params int[] ids);

        #endregion

    }
}