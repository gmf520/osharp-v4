// -----------------------------------------------------------------------
//  <copyright file="Logger.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-02-07 15:39</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;


namespace OSharp.Utility.Logging
{
    /// <summary>
    /// 日志记录者，日志记录输入端
    /// </summary>
    internal sealed class InternalLogger : ILogger
    {
        private readonly ICollection<ILog> _logs;

        /// <summary>
        /// 初始化一个<see cref="InternalLogger"/>新实例
        /// </summary>
        /// <param name="type"></param>
        public InternalLogger(Type type)
            : this(type.FullName)
        { }

        /// <summary>
        /// 初始化一个<see cref="InternalLogger"/>新实例
        /// </summary>
        /// <param name="name">指定名称</param>
        public InternalLogger(string name)
        {
            _logs = LogManager.Adapters.Select(adapter => adapter.GetLogger(name)).ToList();
        }

        static InternalLogger()
        {
            EntryEnabled = true;
            EntryLogLevel = LogLevel.All;
        }

        /// <summary>
        /// 获取或设置 是否允许记录日志，如为 false，将完全禁止日志记录
        /// </summary>
        public static bool EntryEnabled { get; set; }

        /// <summary>
        /// 获取或设置 日志级别的入口控制，级别决定是否执行相应级别的日志记录功能
        /// </summary>
        public static LogLevel EntryLogLevel { get; set; }

        #region Implementation of ILogger

        /// <summary>
        /// 写入<see cref="LogLevel.Trace"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Trace<T>(T message)
        {
            if (!IsEnabledFor(LogLevel.Trace))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Trace(message);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Trace"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Trace(string format, params object[] args)
        {
            if (!IsEnabledFor(LogLevel.Trace))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Trace(format, args);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Debug"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Debug<T>(T message)
        {
            if (!IsEnabledFor(LogLevel.Debug))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Debug(message);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Debug"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Debug(string format, params object[] args)
        {
            if (!IsEnabledFor(LogLevel.Debug))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Debug(format, args);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Info"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="isData">是否数据日志</param>
        public void Info<T>(T message, bool isData)
        {
            if (!IsEnabledFor(LogLevel.Info))
            {
                return;
            }

            var logs = _logs.Where(m => isData ? m.IsDataLogging : !m.IsDataLogging);
            foreach (ILog log in logs)
            {
                log.Info(message, isData);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Info"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Info(string format, params object[] args)
        {
            if (!IsEnabledFor(LogLevel.Info))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Info(format, args);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Warn"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Warn<T>(T message)
        {
            if (!IsEnabledFor(LogLevel.Warn))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Warn(message);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Warn"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Warn(string format, params object[] args)
        {
            if (!IsEnabledFor(LogLevel.Warn))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Warn(format, args);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Error<T>(T message)
        {
            if (!IsEnabledFor(LogLevel.Error))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Error(message);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Error(string format, params object[] args)
        {
            if (!IsEnabledFor(LogLevel.Error))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Error(format, args);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>日志消息，并记录异常
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">异常</param>
        public void Error<T>(T message, Exception exception)
        {
            if (!IsEnabledFor(LogLevel.Error))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Error(message, exception);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>格式化日志消息，并记录异常
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="exception">异常</param>
        /// <param name="args">格式化参数</param>
        public void Error(string format, Exception exception, params object[] args)
        {
            if (!IsEnabledFor(LogLevel.Error))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Error(format, exception, args);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Fatal<T>(T message)
        {
            if (!IsEnabledFor(LogLevel.Fatal))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Fatal(message);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Fatal(string format, params object[] args)
        {
            if (!IsEnabledFor(LogLevel.Fatal))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Fatal(format, args);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>日志消息，并记录异常
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">异常</param>
        public void Fatal<T>(T message, Exception exception)
        {
            if (!IsEnabledFor(LogLevel.Fatal))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Fatal(message, exception);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>格式化日志消息，并记录异常
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="exception">异常</param>
        /// <param name="args">格式化参数</param>
        public void Fatal(string format, Exception exception, params object[] args)
        {
            if (!IsEnabledFor(LogLevel.Fatal))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Fatal(format, exception, args);
            }
        }

        #endregion

        #region 私有方法

        private static bool IsEnabledFor(LogLevel level)
        {
            return EntryEnabled && level >= EntryLogLevel;
        }

        #endregion
    }
}