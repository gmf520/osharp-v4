// -----------------------------------------------------------------------
//  <copyright file="LoggingInitializerBase.cs" company="OSharp开源团队">
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
using System.Web.Mvc;

using OSharp.Core.Configs;
using OSharp.Utility;
using OSharp.Utility.Logging;


namespace OSharp.Core.Initialize
{
    /// <summary>
    /// 日志初始化器基类
    /// </summary>
    public abstract class LoggingInitializerBase
    {
        /// <summary>
        /// 从日志适配器配置节点初始化日志适配器
        /// </summary>
        /// <param name="config">日志适配器配置节点</param>
        protected virtual void SetLoggingFromAdapterConfig(LoggingAdapterConfig config)
        {
            config.CheckNotNull("config");
            if (!config.Enabled)
            {
                return;
            }
            ILoggerAdapter adapter = DependencyResolver.Current.GetService(config.AdapterType) as ILoggerAdapter;
            if (adapter == null)
            {
                adapter = Activator.CreateInstance(config.AdapterType) as ILoggerAdapter;
                if (adapter == null)
                {
                    return;
                }
            }
            LogManager.AddLoggerAdapter(adapter);
        }
    }
}