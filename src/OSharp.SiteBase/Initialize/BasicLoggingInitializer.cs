// -----------------------------------------------------------------------
//  <copyright file="BasicLoggingInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-29 13:51</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Configs;
using OSharp.Core.Initialize;
using OSharp.SiteBase.Logging;
using OSharp.Utility.Logging;


namespace OSharp.SiteBase.Initialize
{
    /// <summary>
    /// 基础日志初始化器，用于初始化基础日志功能
    /// </summary>
    public class BasicLoggingInitializer : LoggingInitializerBase, IBasicLoggingInitializer
    {
        /// <summary>
        /// 开始初始化基础日志
        /// </summary>
        /// <param name="config">基础日志配置信息</param>
        public virtual void  Initialize(LoggingConfig config)
        {
            LogManager.SetEntryInfo(config.EntryConfig.Enabled, config.EntryConfig.EntryLogLevel);
            if (config.BasicLoggingConfig.AdapterConfigs.Count == 0)
            {
                config.BasicLoggingConfig.AdapterConfigs.Add(new LoggingAdapterConfig() { AdapterType = typeof(Log4NetLoggerAdapter) });
            }
            foreach (LoggingAdapterConfig adapterConfig in config.BasicLoggingConfig.AdapterConfigs)
            {
                SetLoggingFromAdapterConfig(adapterConfig);
            }
        }
    }
}