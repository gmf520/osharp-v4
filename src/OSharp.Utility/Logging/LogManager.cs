// -----------------------------------------------------------------------
//  <copyright file="LogManager.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-22 20:33</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;


namespace OSharp.Utility.Logging
{
    /// <summary>
    /// 日志管理器
    /// </summary>
    public static class LogManager
    {
        private static readonly ConcurrentDictionary<string, ILogger> Loggers;
        private static readonly object LockObj = new object();

        /// <summary>
        /// 初始化一个<see cref="LogManager"/>实例
        /// </summary>
        static LogManager()
        {
            Loggers = new ConcurrentDictionary<string, ILogger>();
            Adapters = new List<ILoggerAdapter>();
        }

        /// <summary>
        /// 获取 日志适配器集合
        /// </summary>
        internal static ICollection<ILoggerAdapter> Adapters { get; private set; }

        /// <summary>
        /// 添加日志适配器
        /// </summary>
        public static void AddLoggerAdapter(ILoggerAdapter adapter)
        {
            lock (LockObj)
            {
                if (Adapters.Any(m => m == adapter))
                {
                    return;
                }
                Adapters.Add(adapter);
                Loggers.Clear();
            }
        }

        /// <summary>
        /// 移除日志适配器
        /// </summary>
        public static void RemoveLoggerAdapter(ILoggerAdapter adapter)
        {
            lock (LockObj)
            {
                if (Adapters.All(m => m != adapter))
                {
                    return;
                }
                Adapters.Remove(adapter);
                Loggers.Clear();
            }
        }

        /// <summary>
        /// 设置日志记录入口参数
        /// </summary>
        /// <param name="enabled">是否允许记录日志，如为 false，将完全禁止日志记录</param>
        /// <param name="entryLevel">日志级别的入口控制，级别决定是否执行相应级别的日志记录功能</param>
        public static void SetEntryInfo(bool enabled, LogLevel entryLevel)
        {
            InternalLogger.EntryEnabled = enabled;
            InternalLogger.EntryLogLevel = entryLevel;
        }

        /// <summary>
        /// 获取日志记录者实例
        /// </summary>
        public static ILogger GetLogger(string name)
        {
            name.CheckNotNullOrEmpty("name");
            lock (LockObj)
            {
                ILogger logger;
                if (Loggers.TryGetValue(name, out logger))
                {
                    return logger;
                }
                logger = new InternalLogger(name);
                Loggers[name] = logger;
                return logger; 
            }
        }

        /// <summary>
        /// 获取指定类型的日志记录实例
        /// </summary>
        public static ILogger GetLogger(Type type)
        {
            type.CheckNotNull("type");
            return GetLogger(type.FullName);
        }

        /// <summary>
        /// 获取指定类型的日志记录实例
        /// </summary>
        public static ILogger GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }
    }
}