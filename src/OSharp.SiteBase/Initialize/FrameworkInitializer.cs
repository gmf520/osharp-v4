// -----------------------------------------------------------------------
//  <copyright file="FrameworkInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-02 22:34</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core;
using OSharp.Core.Configs;
using OSharp.Core.Initialize;
using OSharp.SiteBase.Dependency;


namespace OSharp.SiteBase.Initialize
{
    /// <summary>
    /// 框架初始化器基类
    /// </summary>
    public class FrameworkInitializer : IFrameworkInitializer
    {
        /// <summary>
        /// 初始化一个<see cref="FrameworkInitializer"/>类型的新实例
        /// </summary>
        public FrameworkInitializer()
        {
            BasicLoggingInitializer = new BasicLoggingInitializer();
            DatabaseInitializer = new DatabaseInitializer();
            DataLoggingInitializer = new DataLoggingInitializer();
            DataHandlerInitializer = new DataHandlerInitializer();
        }

        /// <summary>
        /// 获取或设置 基础日志初始化器
        /// </summary>
        public IBasicLoggingInitializer BasicLoggingInitializer { get; set; }

        /// <summary>
        /// 获取或设置 数据库初始化器
        /// </summary>
        public IDatabaseInitializer DatabaseInitializer { get; set; }

        /// <summary>
        /// 获取或设置 MVC依赖注入初始化器
        /// </summary>
        public IMvcIocInitializer MvcIocInitializer { get; set; }

        /// <summary>
        /// 获取或设置 WebApi依赖注入初始化器
        /// </summary>
        public IWebApiIocInitializer WebApiIocInitializer { get; set; }

        /// <summary>
        /// 获取或设置 SignalR依赖注入初始化器
        /// </summary>
        public ISignalRIocInitializer SignalRIocInitializer { get; set; }

        /// <summary>
        /// 获取或设置 数据日志初始化器
        /// </summary>
        public IDataLoggingInitializer DataLoggingInitializer { get; set; }

        /// <summary>
        /// 获取或设置 数据处理初始化器
        /// </summary>
        public IDataHandlerInitializer DataHandlerInitializer { get; set; }

        /// <summary>
        /// 执行初始化
        /// </summary>
        public void Initialize()
        {
            if (MvcIocInitializer == null)
            {
                throw new InvalidCastException("MVC初始化器不能为空，请为FrameworkInitializer.MvcIocInitializer属性赋值");
            }
            OSharpConfig config = OSharpConfig.Instance;

            BasicLoggingInitializer.Initialize(config.LoggingConfig);

            DatabaseInitializer.Initialize(config.DataConfig);
            
            if (MvcIocInitializer != null)
            {
                MvcIocInitializer.Initialize(config);
            }
            if (WebApiIocInitializer != null)
            {
                WebApiIocInitializer.Initialize(config);
            }
            if (SignalRIocInitializer != null)
            {
                SignalRIocInitializer.Initialize(config);
            }
            //DataLoggingInitializer.Initialize(config.LoggingConfig.DataLoggingConfig);
            DataHandlerInitializer.Initialize();
        }

        /// <summary>
        /// 重写以实现重置OSharp配置信息
        /// </summary>
        /// <param name="config">原始配置信息</param>
        /// <returns>重置后的配置信息</returns>
        protected virtual OSharpConfig ResetConfig(OSharpConfig config)
        {
            return config;
        }
    }
}