// -----------------------------------------------------------------------
//  <copyright file="BasicLoggingInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-29 13:51</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Configs;
using OSharp.Core.Initialize;
using OSharp.Utility.Logging;


namespace OSharp.SiteBase.Initialize
{
    /// <summary>
    /// log4net日志初始化器，用于初始化基础日志功能
    /// </summary>
    public class Log4NetLoggingInitializer : LoggingInitializerBase, IBasicLoggingInitializer
    {
        /// <summary>
        /// 开始初始化基础日志
        /// </summary>
        /// <param name="config">日志配置信息</param>
        public void Initialize(LoggingConfig config)
        {
            LogManager.SetEntryInfo(config.EntryConfig.Enabled, config.EntryConfig.EntryLogLevel);
            foreach (LoggingAdapterConfig adapterConfig in config.BasicLoggingConfig.AdapterConfigs)
            {
                SetLoggingFromAdapterConfig(adapterConfig);
            }
        }
    }
}