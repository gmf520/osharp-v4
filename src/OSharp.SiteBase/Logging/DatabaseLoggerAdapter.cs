// -----------------------------------------------------------------------
//  <copyright file="DatabaseLoggerAdapter.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-07 17:36</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core;
using OSharp.Core.Dependency;
using OSharp.Utility.Logging;


namespace OSharp.SiteBase.Logging
{
    /// <summary>
    /// 数据库日志适配器
    /// </summary>
    public class DatabaseLoggerAdapter : LoggerAdapterBase, ILifetimeScopeDependency
    {
        /// <summary>
        /// 获取或设置 依赖注入解析器
        /// </summary>
        public IIocResolver IocResolver { get; set; }

        /// <summary>
        /// 获取指定名称的Logger实例
        /// </summary>
        /// <param name="name">指定名称</param>
        /// <returns>日志实例</returns>
        /// <exception cref="System.NotSupportedException">指定名称的日志缓存实例不存在则返回异常<see cref="System.NotSupportedException"/></exception>
        protected override ILog GetLoggerInternal(string name)
        {
            ILog log =  CreateLogger(name);
            System.Diagnostics.Debug.WriteLine(log.GetHashCode());
            return log;
        }

        /// <summary>
        /// 创建指定名称的缓存实例
        /// </summary>
        /// <param name="name">指定名称</param>
        /// <returns></returns>
        protected override ILog CreateLogger(string name)
        {
            return IocResolver.Resolve<DatabaseLog>();
        }
    }
}