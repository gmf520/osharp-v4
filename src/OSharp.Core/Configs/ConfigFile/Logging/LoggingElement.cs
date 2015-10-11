// -----------------------------------------------------------------------
//  <copyright file="LoggingElement.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-30 15:39</last-date>
// -----------------------------------------------------------------------

using System.Configuration;


namespace OSharp.Core.Configs.ConfigFile
{
    /// <summary>
    /// 日志配置节点
    /// </summary>
    internal class LoggingElement : ConfigurationElement
    {
        private const string LoggingEntryKey = "entry";
        private const string DataLoggingKey = "data";
        private const string BasicLoggingKey = "basic";

        /// <summary>
        /// 获取或设置 日志输入配置节点
        /// </summary>
        [ConfigurationProperty(LoggingEntryKey)]
        public virtual LoggingEntryElement LoggingEntry
        {
            get { return (LoggingEntryElement)this[LoggingEntryKey]; }
            set { this[LoggingEntryKey] = value; }
        }

        ///// <summary>
        ///// 获取或设置 数据日志配置节点
        ///// </summary>
        //[ConfigurationProperty(DataLoggingKey)]
        //public virtual DataLoggingElement DataLogging
        //{
        //    get { return (DataLoggingElement)this[DataLoggingKey]; }
        //    set { this[DataLoggingKey] = value; }
        //}

        /// <summary>
        /// 获取或设置 基础日志配置节点
        /// </summary>
        [ConfigurationProperty(BasicLoggingKey)]
        public virtual BasicLoggingElement BasicLogging
        {
            get { return (BasicLoggingElement)this[BasicLoggingKey]; }
            set { this[BasicLoggingKey] = value; }
        }

    }
}