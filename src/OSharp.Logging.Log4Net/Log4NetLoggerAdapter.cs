// -----------------------------------------------------------------------
//  <copyright file="Log4NetLoggerAdapter.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-23 8:58</last-date>
// -----------------------------------------------------------------------

using System;
using System.IO;

using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;

using OSharp.Utility.Logging;

using LogManager = log4net.LogManager;


namespace OSharp.Logging.Log4Net
{
    /// <summary>
    /// log4net 日志输出适配器
    /// </summary>
    public class Log4NetLoggerAdapter : LoggerAdapterBase
    {
        /// <summary>
        /// 初始化一个<see cref="Log4NetLoggerAdapter"/>类型的新实例
        /// </summary>
        public Log4NetLoggerAdapter()
        {
            const string fileName = "log4net.config";
            string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (File.Exists(configFile))
            {
                XmlConfigurator.ConfigureAndWatch(new FileInfo(configFile));
                return;
            }
            RollingFileAppender appender = new RollingFileAppender
            {
                Name = "root",
                File = "logs\\log_",
                AppendToFile = true,
                LockingModel = new FileAppender.MinimalLock(),
                RollingStyle = RollingFileAppender.RollingMode.Date,
                DatePattern = "yyyyMMdd-HH\".log\"",
                StaticLogFileName = false,
                MaxSizeRollBackups = 10,
                Layout = new PatternLayout("[%d{yyyy-MM-dd HH:mm:ss.fff}] %-5p %c.%M %t %w %n%m%n")
                //Layout = new PatternLayout("[%d [%t] %-5p %c [%x] - %m%n]")
            };
            appender.ClearFilters();
            appender.AddFilter(new LevelMatchFilter { LevelToMatch = Level.Info });
            //PatternLayout layout = new PatternLayout("[%d{yyyy-MM-dd HH:mm:ss.fff}] %c.%M %t %n%m%n");
            //appender.Layout = layout;
            BasicConfigurator.Configure(appender);
            appender.ActivateOptions();
        }

        #region Overrides of LoggerAdapterBase

        /// <summary>
        /// 创建指定名称的缓存实例
        /// </summary>
        /// <param name="name">指定名称</param>
        /// <returns></returns>
        protected override ILog CreateLogger(string name)
        {
            log4net.ILog log = LogManager.GetLogger(name);
            return new Log4NetLog(log);
        }

        #endregion
    }
}