// -----------------------------------------------------------------------
//  <copyright file="LoggingEntryElement.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-01 11:50</last-date>
// -----------------------------------------------------------------------

using System.Configuration;

using OSharp.Utility.Logging;


namespace OSharp.Core.Configs.ConfigFile
{
    /// <summary>
    /// 日志输入配置节点
    /// </summary>
    internal class LoggingEntryElement : ConfigurationElement
    {
        private const string EnabledKey = "enabled";
        private const string EntryLogLevelKey = "level";

        /// <summary>
        /// 获取或设置 是否允许日志输入
        /// </summary>
        [ConfigurationProperty(EnabledKey, DefaultValue = true)]
        public virtual bool Enabled
        {
            get { return (bool)this[EnabledKey]; }
            set { this[EnabledKey] = value; }
        }

        /// <summary>
        /// 获取或设置 日志输入级别
        /// </summary>
        [ConfigurationProperty(EntryLogLevelKey, DefaultValue = LogLevel.All)]
        public virtual LogLevel EntryLogLevel
        {
            get { return (LogLevel)this[EntryLogLevelKey]; }
            set { this[EnabledKey] = value; }
        }
    }
}