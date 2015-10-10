// -----------------------------------------------------------------------
//  <copyright file="FrameworkInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-10 15:35</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Configs;
using OSharp.Core.Dependency;
using OSharp.Core.Initialize;
using OSharp.Core.Properties;
using OSharp.Core.Security;
using OSharp.Utility;


namespace OSharp.Core
{
    /// <summary>
    /// 框架初始化
    /// </summary>
    public class FrameworkInitializer : IFrameworkInitializer
    {
        //基础模块，只初始化一次
        private static bool _basicLoggingInitialized;
        private static bool _databaseInitialized;
        private static bool _entityInfoInitialized;

        /// <summary>
        /// 开始执行框架初始化
        /// </summary>
        /// <param name="services">服务映射集合</param>
        /// <param name="iocBuilder">依赖注入构建器</param>
        public void Initialize(IServiceCollection services, IIocBuilder iocBuilder)
        {
            services.CheckNotNull("services");
            iocBuilder.CheckNotNull("iocBuilder");

            OSharpConfig config = OSharpConfig.Instance;

            //使用副本进行初始化，防止不同平台间的相互污染
            IServiceCollection newServices = services.Clone();
            IServiceProvider provider = iocBuilder.Build(newServices);

            IBasicLoggingInitializer loggingInitializer = provider.GetService<IBasicLoggingInitializer>();
            if (!_basicLoggingInitialized && loggingInitializer != null)
            {
                loggingInitializer.Initialize(config.LoggingConfig);
                _basicLoggingInitialized = true;
            }

            IDatabaseInitializer databaseInitializer = provider.GetService<IDatabaseInitializer>();
            if (!_databaseInitialized)
            {
                if (databaseInitializer == null)
                {
                    throw new InvalidOperationException(Resources.FrameworkInitializerBase_DatabaseInitializeIsNull);
                }
                databaseInitializer.Initialize(config.DataConfig);
                _databaseInitialized = true;
            }

            if (!_entityInfoInitialized)
            {
                IEntityInfoHandler entityInfoHandler = provider.GetService<IEntityInfoHandler>();
                if (entityInfoHandler == null)
                {
                    throw new InvalidOperationException(Resources.FrameworkInitializerBase_EntityInfoHandlerIsNull);
                }
                entityInfoHandler.Initialize();
                _entityInfoInitialized = true;
            }

            IFunctionHandler functionHandler = provider.GetService<IFunctionHandler>();
            if (functionHandler == null)
            {
                throw new InvalidOperationException(Resources.FrameworkInitializerBase_FunctionHandlerIsNull);
            }
            functionHandler.Initialize();
        }
    }
}