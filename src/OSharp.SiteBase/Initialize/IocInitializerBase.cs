// -----------------------------------------------------------------------
//  <copyright file="IocInitializerBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-02 17:08</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Configs;
using OSharp.Core.Data;
using OSharp.Core.Data.Entity;
using OSharp.Core.Dependency;
using OSharp.Core.Initialize;
using OSharp.Core.Reflection;


namespace OSharp.SiteBase.Initialize
{
    /// <summary>
    /// 依赖注入初始化器基类，从程序集中反射进行依赖注入接口与实现的注册
    /// </summary>
    public abstract class IocInitializerBase : IIocInitializer
    {
        /// <summary>
        /// 初始化一个<see cref="IocInitializerBase"/>类型的新实例
        /// </summary>
        protected IocInitializerBase()
        {
            AssemblyFinder = new CurrentDomainAssemblyFinder();
            TransientTypeFinder = new TransientDependencyTypeFinder();
            LifetimeScopeTypeFinder = new LifetimeScopeDependencyTypeFinder();
            SingletonTypeFinder = new SingletonDependencyTypeFinder();
        }

        /// <summary>
        /// 获取或设置 程序集查找器
        /// </summary>
        public IAssemblyFinder AssemblyFinder { get; set; }

        /// <summary>
        /// 获取或设置 即时对象依赖类型查找器
        /// </summary>
        public ITypeFinder TransientTypeFinder { get; set; }

        /// <summary>
        /// 获取或设置 局部对象依赖类型查找器
        /// </summary>
        public ITypeFinder LifetimeScopeTypeFinder { get; set; }

        /// <summary>
        /// 获取或设置 单例对象依赖类型查找器
        /// </summary>
        public ITypeFinder SingletonTypeFinder { get; set; }

        /// <summary>
        /// 初始化依赖注入
        /// </summary>
        /// <param name="config">框架配置信息</param>
        public void Initialize(OSharpConfig config)
        {
            Type[] dbContexTypes = config.DataConfig.ContextConfigs.Where(m => m.Enabled).Select(m => m.ContextType).ToArray();
            RegisterDbContextTypes(dbContexTypes, typeof(IUnitOfWork));

            RegisterRepositoryType(typeof(Repository<,>), typeof(IRepository<,>));

            Type[] dependencyTypes = TransientTypeFinder.FindAll();
            RegisterDependencyTypes<ITransientDependency>(dependencyTypes);

            dependencyTypes = LifetimeScopeTypeFinder.FindAll();
            RegisterDependencyTypes<ILifetimeScopeDependency>(dependencyTypes);

            dependencyTypes = SingletonTypeFinder.FindAll();
            RegisterDependencyTypes<ISingletonDependency>(dependencyTypes);

            RegisterCustomTypes();

            Assembly[] assemblies = AssemblyFinder.FindAll();
            SetResolver(assemblies);
        }

        /// <summary>
        /// 重写以实现数据上下文类型的注册
        /// </summary>
        /// <param name="dbContexTypes">数据上下文类型</param>
        /// <param name="asType">IUnitOfWork类型</param>
        protected abstract void RegisterDbContextTypes(Type[] dbContexTypes, Type asType);

        /// <summary>
        /// 重写以实现数据仓储类型的注册
        /// </summary>
        /// <param name="repositoryType">数据仓储实现类型</param>
        /// <param name="iRepositoryType">数据仓储接口类型</param>
        protected abstract void RegisterRepositoryType(Type repositoryType, Type iRepositoryType);

        /// <summary>
        /// 重写以实现依赖注入接口<see cref="IDependency"/>实现类型的注册
        /// </summary>
        /// <param name="types">要注册的类型集合</param>
        protected abstract void RegisterDependencyTypes<TDependency>(Type[] types)
            where TDependency : IDependency;

        /// <summary>
        /// 注册自定义类型
        /// </summary>
        protected virtual void RegisterCustomTypes()
        { }

        /// <summary>
        /// 重写以实现设置Mvc、WebAPI、SignalR等框架的DependencyResolver
        /// </summary>
        /// <param name="assemblies"></param>
        protected abstract void SetResolver(Assembly[] assemblies);
    }
}