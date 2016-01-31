// -----------------------------------------------------------------------
//  <copyright file="DataConfigReseter.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-26 0:59</last-date>
// -----------------------------------------------------------------------

using System.Linq;

using OSharp.Core.Configs;
using OSharp.Data.Entity.Logging;
using OSharp.Core.Reflection;


namespace OSharp.Data.Entity
{
    /// <summary>
    /// 数据配置信息重置类
    /// </summary>
    public class DataConfigReseter : IDataConfigReseter
    {
        /// <summary>
        /// 初始化一个<see cref="DataConfigReseter"/>类型的新实例
        /// </summary>
        public DataConfigReseter()
        {
            MapperAssemblyFinder = new EntityMapperAssemblyFinder()
            {
                AllAssemblyFinder = new DirectoryAssemblyFinder()
            };
        }

        /// <summary>
        /// 获取或设置 实体映射程序集查找器
        /// </summary>
        public IEntityMapperAssemblyFinder MapperAssemblyFinder { get; set; }

        /// <summary>
        /// 重置数据配置信息
        /// </summary>
        /// <param name="config">原始数据配置信息</param>
        /// <returns>重置后的数据配置信息</returns>
        public DataConfig Reset(DataConfig config)
        {
            //没有上下文，添加默认上下文
            if (!config.ContextConfigs.Any())
            {
                DbContextConfig contextConfig = GetDefaultDbContextConfig();
                config.ContextConfigs.Add(contextConfig);
            }
            //如果业务上下文存在开启数据日志功能，并且日志上下文没有设置，则添加日志上下文
            if (config.ContextConfigs.All(m => m.ContextType != typeof(LoggingDbContext)))
            {
                DbContextConfig contextConfig = GetLoggingDbContextConfig();
                config.ContextConfigs.Add(contextConfig);
            }
            return config;
        }

        /// <summary>
        /// 获取默认业务上下文配置信息
        /// </summary>
        /// <returns></returns>
        protected virtual DbContextConfig GetDefaultDbContextConfig()
        {
            return new DbContextConfig()
            {
                ConnectionStringName = "default",
                ContextType = typeof(DefaultDbContext),
                InitializerConfig = new DbContextInitializerConfig()
                {
                    InitializerType = typeof(DefaultDbContextInitializer),
                    EntityMapperAssemblies = MapperAssemblyFinder.FindAll()
                }
            };
        }

        /// <summary>
        /// 获取默认日志上下文配置信息
        /// </summary>
        /// <returns></returns>
        protected virtual DbContextConfig GetLoggingDbContextConfig()
        {
            return new DbContextConfig()
            {
                ConnectionStringName = "default",
                ContextType = typeof(LoggingDbContext),
                InitializerConfig = new DbContextInitializerConfig()
                {
                    InitializerType = typeof(LoggingDbContextInitializer),
                    EntityMapperAssemblies = { typeof(LoggingDbContext).Assembly }
                }
            };
        }

    }
}