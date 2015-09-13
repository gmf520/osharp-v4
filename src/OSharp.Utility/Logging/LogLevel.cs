// -----------------------------------------------------------------------
//  <copyright file="LogLevel.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-08-10 13:48</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Utility.Logging
{
    /// <summary>
    /// 表示日志输出级别的枚举
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 输出所有级别的日志
        /// </summary>
        All = 0,

        /// <summary>
        /// 表示跟踪的日志级别
        /// </summary>
        Trace = 1,

        /// <summary>
        /// 表示调试的日志级别
        /// </summary>
        Debug = 2,

        /// <summary>
        /// 表示消息的日志级别
        /// </summary>
        Info = 3,

        /// <summary>
        /// 表示警告的日志级别
        /// </summary>
        Warn = 4,

        /// <summary>
        /// 表示错误的日志级别
        /// </summary>
        Error = 5,

        /// <summary>
        /// 表示严重错误的日志级别
        /// </summary>
        Fatal = 6,

        /// <summary>
        /// 关闭所有日志，不输出日志
        /// </summary>
        Off = 7
    }
}