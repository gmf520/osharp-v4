// -----------------------------------------------------------------------
//  <copyright file="FrameworkInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-10 15:35</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using OSharp.Core.Configs;
using OSharp.Core.Dependency;
using OSharp.Core.Initialize;
using OSharp.Core.Mapping;
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
        private static bool _mapperInitialized;
        private static bool _basicLoggingInitialized;
        private static bool _databaseInitialized;
        private static bool _entityInfoInitialized;

        /// <summary>
        /// 开始执行框架初始化
        /// </summary>
        /// <param name="iocBuilder">依赖注入构建器</param>
        public void Initialize(IIocBuilder iocBuilder)
        {
            iocBuilder.CheckNotNull("iocBuilder");

            OSharpConfig config = OSharpConfig.Instance;

            //依赖注入初始化
            IServiceProvider provider = iocBuilder.Build();

            //对象映射功能初始化
            IMappersBuilder mappersBuilder = provider.GetService<IMappersBuilder>();
            IMapper mapper = provider.GetService<IMapper>();
            if (!_mapperInitialized && mapper != null)
            {
                if (mappersBuilder != null)
                {
                    IEnumerable<IMapTuple> mapTuples = provider.GetServices<IMapTuple>();
                    mappersBuilder.Build(mapTuples);
                }
                MapperExtensions.SetMaper(mapper);
                _mapperInitialized = true;
            }

            //日志功能初始化
            IBasicLoggingInitializer loggingInitializer = provider.GetService<IBasicLoggingInitializer>();
            if (!_basicLoggingInitialized && loggingInitializer != null)
            {
                loggingInitializer.Initialize(config.LoggingConfig);
                _basicLoggingInitialized = true;
            }

            //数据库初始化
            IDatabaseInitializer databaseInitializer = provider.GetService<IDatabaseInitializer>();
            if (!_databaseInitialized && databaseInitializer != null)
            {
                databaseInitializer.Initialize(config.DataConfig);
                _databaseInitialized = true;
            }

            //实体信息初始化
            IEntityInfoHandler entityInfoHandler = provider.GetService<IEntityInfoHandler>();
            if (!_entityInfoInitialized && entityInfoHandler != null)
            {
                entityInfoHandler.Initialize();
                _entityInfoInitialized = true;
            }
            //功能信息初始化
            IFunctionHandler functionHandler = provider.GetService<IFunctionHandler>();
            if (functionHandler != null)
            {
                functionHandler.Initialize();
            }
        }
    }
}