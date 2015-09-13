// -----------------------------------------------------------------------
//  <copyright file="DataLogCache.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-04 2:24</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using OSharp.Core;
using OSharp.Core.Logging;


namespace OSharp.SiteBase.Logging
{
    /// <summary>
    /// 数据日志缓存类
    /// </summary>
    public class DataLogCache : IDataLogCache
    {
        private readonly IList<DataLog> _dataLogs;

        /// <summary>
        /// 初始化一个<see cref="DataLogCache"/>类型的新实例
        /// </summary>
        public DataLogCache()
        {
            _dataLogs = new List<DataLog>();
        }

        /// <summary>
        /// 获取 数据日志集合
        /// </summary>
        public IEnumerable<DataLog> DataLogs
        {
            get { return _dataLogs; }
        }

        /// <summary>
        /// 向缓存中添加数据日志信息
        /// </summary>
        /// <param name="dataLog">数据日志信息</param>
        public void AddDataLog(DataLog dataLog)
        {
            _dataLogs.Add(dataLog);
        }

    }
}