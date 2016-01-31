// -----------------------------------------------------------------------
//  <copyright file="LoggingConfig.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-30 18:05</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Configs.ConfigFile;


namespace OSharp.Core.Configs
{
    /// <summary>
    /// 日志配置信息
    /// </summary>
    public class LoggingConfig
    {
        /// <summary>
        /// 初始化一个<see cref="LoggingConfig"/>类型的新实例
        /// </summary>
        public LoggingConfig()
        {
            EntryConfig = new LoggingEntryConfig();
            DataLoggingConfig = new DataLoggingConfig();
            BasicLoggingConfig = new BasicLoggingConfig();
        }

        /// <summary>
        /// 初始化一个<see cref="LoggingConfig"/>类型的新实例
        /// </summary>
        internal LoggingConfig(LoggingElement element)
        {
            EntryConfig = new LoggingEntryConfig(element.LoggingEntry);
            //DataLoggingConfig = new DataLoggingConfig(element.DataLogging);
            BasicLoggingConfig = new BasicLoggingConfig(element.BasicLogging);
        }

        /// <summary>
        /// 获取或设置 日志入口配置信息
        /// </summary>
        public LoggingEntryConfig EntryConfig { get; set; }

        /// <summary>
        /// 获取或设置 数据日志配置信息
        /// </summary>
        public DataLoggingConfig DataLoggingConfig { get; set; }

        /// <summary>
        /// 获取或设置 基本日志配置信息
        /// </summary>
        public BasicLoggingConfig BasicLoggingConfig { get; set; }
    }
}