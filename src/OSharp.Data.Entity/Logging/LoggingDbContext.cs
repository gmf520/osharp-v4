// -----------------------------------------------------------------------
//  <copyright file="LoggingDbContext.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-29 22:14</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Data.Entity.Logging
{
    /// <summary>
    /// 日志数据上下文
    /// </summary>
    public class LoggingDbContext : DbContextBase<LoggingDbContext>
    {
        /// <summary>
        /// 获取 是否允许数据库日志记录
        /// </summary>
        protected override bool DataLoggingEnabled { get { return false; } }

    }
}