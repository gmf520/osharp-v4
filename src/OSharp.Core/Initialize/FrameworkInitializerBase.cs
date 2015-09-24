// -----------------------------------------------------------------------
//  <copyright file="FrameworkInitializerBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-23 14:59</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Configs;
using OSharp.Core.Context;
using OSharp.Core.Properties;
using OSharp.Core.Security;


namespace OSharp.Core.Initialize
{
    /// <summary>
    /// 框架初始化基类
    /// </summary>
    public abstract class FrameworkInitializerBase : IFrameworkInitializer
    {
        //基础模块，只初始化一次
        private static bool _basicLoggingInitialized;
        private static bool _databaseInitialized;
        private static bool _entityInfoInitialized;
        private IEntityInfoHandler _entityInfoHandler;
        private IFunctionHandler _functionHandler;


        /// <summary>
        /// 获取或设置 基础日志初始化器
        /// </summary>
        public IBasicLoggingInitializer BasicLoggingInitializer { get; set; }

        /// <summary>
        /// 获取或设置 数据库初始化器
        /// </summary>
        public IDatabaseInitializer DatabaseInitializer { get; set; }

        /// <summary>
        /// 获取或设置 依赖注入初始化器
        /// </summary>
        public IIocInitializer IocInitializer { get; set; }

        /// <summary>
        /// 获取或设置 实体信息数据处理器
        /// </summary>
        public IEntityInfoHandler EntityInfoHandler
        {
            get { return _entityInfoHandler; }
            set
            {
                _entityInfoHandler = value;
                OSharpContext.Current.EntityInfoHandler = value;
            }
        }

        /// <summary>
        /// 获取或设置 功能信息数据处理器
        /// </summary>
        public IFunctionHandler FunctionHandler
        {
            get { return _functionHandler; }
            set
            {
                _functionHandler = value;
                OSharpContext.Current.FunctionHandler = value;
            }
        }

        /// <summary>
        /// 开始初始化
        /// </summary>
        public void Initialize()
        {
            OSharpConfig config = ResetConfig(OSharpConfig.Instance);

            if (!_basicLoggingInitialized && BasicLoggingInitializer != null)
            {
                BasicLoggingInitializer.Initialize(config.LoggingConfig);
                _basicLoggingInitialized = true;
            }

            if (IocInitializer == null)
            {
                throw new InvalidOperationException(Resources.FrameworkInitializerBase_IocInitializeIsNull);
            }
            IocInitializer.Initialize(config, this);

            if (!_databaseInitialized)
            {
                if (DatabaseInitializer == null)
                {
                    throw new InvalidOperationException(Resources.FrameworkInitializerBase_DatabaseInitializeIsNull);
                }
                DatabaseInitializer.Initialize(config.DataConfig);
                _databaseInitialized = true;
            }

            if (!_entityInfoInitialized)
            {
                if (EntityInfoHandler == null)
                {
                    throw new InvalidOperationException(Resources.FrameworkInitializerBase_EntityInfoHandlerIsNull);
                }
                EntityInfoHandler.Initialize();
                _entityInfoInitialized = true;
            }

            if (FunctionHandler == null)
            {
                throw new InvalidOperationException(Resources.FrameworkInitializerBase_FunctionHandlerIsNull);
            }
            FunctionHandler.Initialize();
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