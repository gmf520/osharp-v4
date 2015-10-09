// -----------------------------------------------------------------------
//  <copyright file="FrameworkInitializerBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-23 14:59</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Configs;
using OSharp.Core.Dependency;
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

        private readonly InitializeOptionsBase _options;

        /// <summary>
        /// 初始化一个<see cref="FrameworkInitializerBase"/>类型的新实例
        /// </summary>
        protected FrameworkInitializerBase(InitializeOptionsBase options)
        {
            _options = options;
            Services = options.ServicesBuilder.Build(options.OSharpConfig);
        }

        /// <summary>
        /// 获取 依赖注入解析器
        /// </summary>
        protected abstract IIocResolver IocResolver { get; }

        /// <summary>
        /// 获取 依赖注入服务映射信息集合
        /// </summary>
        public IServiceCollection Services { get; private set; }

        /// <summary>
        /// 开始初始化
        /// </summary>
        public void Initialize()
        {
            OSharpConfig config = _options.OSharpConfig;

            if (!_basicLoggingInitialized && _options.BasicLoggingInitializer != null)
            {
                _options.BasicLoggingInitializer.Initialize(config.LoggingConfig);
                _basicLoggingInitialized = true;
            }

            if (_options.IocInitializer == null)
            {
                throw new InvalidOperationException(Resources.FrameworkInitializerBase_IocInitializeIsNull);
            }
            _options.IocInitializer.Initialize(Services);

            if (!_databaseInitialized)
            {
                if (_options.DatabaseInitializer == null)
                {
                    throw new InvalidOperationException(Resources.FrameworkInitializerBase_DatabaseInitializeIsNull);
                }
                _options.DatabaseInitializer.Initialize(config.DataConfig);
                _databaseInitialized = true;
            }

            if (!_entityInfoInitialized)
            {
                IEntityInfoHandler entityInfoHandler = IocResolver.Resolve<IEntityInfoHandler>();
                if (entityInfoHandler == null)
                {
                    throw new InvalidOperationException(Resources.FrameworkInitializerBase_EntityInfoHandlerIsNull);
                }
                entityInfoHandler.Initialize();
                _entityInfoInitialized = true;
            }

            IFunctionHandler functionHandler = IocResolver.Resolve<IFunctionHandler>();
            if (functionHandler == null)
            {
                throw new InvalidOperationException(Resources.FrameworkInitializerBase_FunctionHandlerIsNull);
            }
            functionHandler.Initialize();
        }

        /// <summary>
        /// 重写以实现重置OSharp配置信息
        /// </summary>
        /// <param name="config">原始配置信息</param>
        /// <returns>重置后的配置信息</returns>
        protected virtual OSharpConfig ResetConfig(OSharpConfig config)
        {
            if (_options.DataConfigReseter != null)
            {
                config.DataConfig = _options.DataConfigReseter.Reset(config.DataConfig);
            }
            return config;
        }
    }
}