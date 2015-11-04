// -----------------------------------------------------------------------
//  <copyright file="DatabaseInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-23 10:01</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

using OSharp.Core.Configs;
using OSharp.Data.Entity.Logging;
using OSharp.Data.Entity.Properties;
using OSharp.Core.Initialize;
using OSharp.Utility.Extensions;


namespace OSharp.Data.Entity
{
    /// <summary>
    /// 数据库初始化器，从程序集中反射实体映射类并加载到相应上下文类中，进行上下文类型的初始化
    /// </summary>
    public class DatabaseInitializer : IDatabaseInitializer
    {
        /// <summary>
        /// 获取或设置 实体映射程序集查找器
        /// </summary>
        public IEntityMapperAssemblyFinder MapperAssemblyFinder { get; set; }

        /// <summary>
        /// 开始初始化数据库
        /// </summary>
        /// <param name="config">数据库配置信息</param>
        public virtual void Initialize(DataConfig config)
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
            foreach (DbContextConfig contextConfig in config.ContextConfigs)
            {
                DbContextInit(contextConfig);
            }
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

        /// <summary>
        /// 初始化数据上下文
        /// </summary>
        /// <param name="config">数据上下文配置信息</param>
        private static void DbContextInit(DbContextConfig config)
        {
            if (!config.Enabled)
            {
                return;
            }
            DbContextInitializerBase initializer = CreateInitializer(config.InitializerConfig);
            DbContextManager.Instance.RegisterInitializer(config.ContextType, initializer);
        }

        private static DbContextInitializerBase CreateInitializer(DbContextInitializerConfig config)
        {
            Type initializerType = config.InitializerType;
            DbContextInitializerBase initializer = Activator.CreateInstance(initializerType) as DbContextInitializerBase;
            if (initializer == null)
            {
                throw new InvalidOperationException(Resources.DatabaseInitializer_TypeNotDatabaseInitializer.FormatWith(initializerType));
            }
            foreach (Assembly mapperAssembly in config.EntityMapperAssemblies)
            {
                if (initializer.MapperAssemblies.Contains(mapperAssembly))
                {
                    continue;
                }
                initializer.MapperAssemblies.Add(mapperAssembly);
            }
            dynamic dynamicInitializer = initializer;
            if (config.CreateDatabaseInitializerType != null)
            {
                dynamic createDatabaseInitializer = Activator.CreateInstance(config.CreateDatabaseInitializerType);
                dynamicInitializer.CreateDatabaseInitializer = createDatabaseInitializer;
            }
            return (DbContextInitializerBase)dynamicInitializer;
        }
    }
}