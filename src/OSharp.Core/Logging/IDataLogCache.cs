// -----------------------------------------------------------------------
//  <copyright file="IDataLogCache.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-06 1:13</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;

using OSharp.Core.Dependency;


namespace OSharp.Core.Logging
{
    /// <summary>
    /// 数据日志缓存接口
    /// </summary>
    public interface IDataLogCache : IScopeDependency
    {
        /// <summary>
        /// 获取 数据日志集合
        /// </summary>
        IEnumerable<DataLog> DataLogs { get; }

        /// <summary>
        /// 向缓存中添加数据日志信息
        /// </summary>
        /// <param name="dataLog">数据日志信息</param>
        void AddDataLog(DataLog dataLog);
    }
}