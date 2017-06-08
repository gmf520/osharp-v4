// -----------------------------------------------------------------------
//  <copyright file="OSharpConfig.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-10 11:05</last-date>
// -----------------------------------------------------------------------

using System;
using System.Configuration;

using OSharp.Core.Configs.ConfigFile;


namespace OSharp.Core.Configs
{
    /// <summary>
    /// OSharp配置类
    /// </summary>
    public sealed class OSharpConfig
    {
        private const string OSharpSectionName = "osharp";
        private static readonly Lazy<OSharpConfig> InstanceLazy
            = new Lazy<OSharpConfig>(() => new OSharpConfig());

        /// <summary>
        /// 初始化一个新的<see cref="OSharpConfig"/>实例
        /// </summary>
        private OSharpConfig()
        {
            OSharpFrameworkSection section = (OSharpFrameworkSection)ConfigurationManager.GetSection(OSharpSectionName);
            if (section == null)
            {
                DataConfig = new DataConfig();
                LoggingConfig = new LoggingConfig();
                return;
            }
            DataConfig = new DataConfig(section.Data);
            LoggingConfig = new LoggingConfig(section.Logging);
        }

        /// <summary>
        /// 获取 配置类的单一实例
        /// </summary>
        public static OSharpConfig Instance
        {
            get
            {
                OSharpConfig config = InstanceLazy.Value;
                if (DataConfigReseter != null)
                {
                    config.DataConfig = DataConfigReseter.Reset(config.DataConfig);
                }
                if (LoggingConfigReseter != null)
                {
                    config.LoggingConfig = LoggingConfigReseter.Reset(config.LoggingConfig);
                }
                return config;
            }
        }

        /// <summary>
        /// 获取或设置 数据配置重置信息
        /// </summary>
        public static IDataConfigReseter DataConfigReseter { get; set; }

        /// <summary>
        /// 获取或设置 日志配置重置信息
        /// </summary>
        public static ILoggingConfigReseter LoggingConfigReseter { get; set; }

        /// <summary>
        /// 获取或设置 数据配置信息
        /// </summary>
        public DataConfig DataConfig { get; set; }

        /// <summary>
        /// 获取或设置 日志配置信息
        /// </summary>
        public LoggingConfig LoggingConfig { get; set; }
    }
}