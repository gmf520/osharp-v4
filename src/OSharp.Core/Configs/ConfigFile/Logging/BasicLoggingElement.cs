// -----------------------------------------------------------------------
//  <copyright file="BasicLoggingElement.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-01 14:50</last-date>
// -----------------------------------------------------------------------

using System.Configuration;


namespace OSharp.Core.Configs.ConfigFile
{
    /// <summary>
    /// 基础日志配置节点
    /// </summary>
    internal class BasicLoggingElement : ConfigurationElement
    {
        private const string AdapterKey = "adapters";

        /// <summary>
        /// 获取或设置 日志适配器配置节点集合
        /// </summary>
        [ConfigurationProperty(AdapterKey)]
        public virtual LoggingAdapterCollection Adapters
        {
            get { return (LoggingAdapterCollection)base[AdapterKey]; }
        }
    }
}