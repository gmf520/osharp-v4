// -----------------------------------------------------------------------
//  <copyright file="BasicLoggingConfig.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-01 14:49</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using OSharp.Core.Configs.ConfigFile;


namespace OSharp.Core.Configs
{
    /// <summary>
    /// 基础日志配置信息
    /// </summary>
    public class BasicLoggingConfig
    {
        /// <summary>
        /// 初始化一个<see cref="BasicLoggingConfig"/>类型的新实例
        /// </summary>
        public BasicLoggingConfig()
        {
            AdapterConfigs = new List<LoggingAdapterConfig>();
        }

        /// <summary>
        /// 初始化一个<see cref="BasicLoggingConfig"/>类型的新实例
        /// </summary>
        internal BasicLoggingConfig(BasicLoggingElement element)
        {
            AdapterConfigs = element.Adapters.OfType<LoggingAdapterElement>()
                .Select(adapter => new LoggingAdapterConfig(adapter)).ToList();
        }

        /// <summary>
        /// 获取或设置 日志适配器配置信息集合
        /// </summary>
        public ICollection<LoggingAdapterConfig> AdapterConfigs { get; set; }
    }
}