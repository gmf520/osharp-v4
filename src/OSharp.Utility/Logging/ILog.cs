// -----------------------------------------------------------------------
//  <copyright file="ILog.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-08-10 13:51</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Utility.Logging
{
    /// <summary>
    /// 表示日志实例的接口
    /// </summary>
    public interface ILog : ILogger
    {
        #region 属性

        /// <summary>
        /// 获取 是否数据日志对象
        /// </summary>
        bool IsDataLogging { get; }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Trace"/>级别的日志
        /// </summary>
        bool IsTraceEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Debug"/>级别的日志
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Info"/>级别的日志
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Warn"/>级别的日志
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Error"/>级别的日志
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Fatal"/>级别的日志
        /// </summary>
        bool IsFatalEnabled { get; }

        #endregion

    }
}