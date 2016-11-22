// -----------------------------------------------------------------------
//  <copyright file="IocBuilderBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-09 3:05</last-date>
// -----------------------------------------------------------------------

using System;
using System.Reflection;

using OSharp.Core.Reflection;


namespace OSharp.Core.Dependency
{
    /// <summary>
    /// 依赖注入构建器基类，从程序集中反射进行依赖注入接口与实现的注册
    /// </summary>
    public abstract class IocBuilderBase : IIocBuilder
    {
        private readonly IServiceCollection _services;
        private bool _isBuilded;

        /// <summary>
        /// 初始化一个<see cref="IocBuilderBase"/>类型的新实例
        /// </summary>
        /// <param name="services">服务信息集合</param>
        protected IocBuilderBase(IServiceCollection services)
        {
            AssemblyFinder = new DirectoryAssemblyFinder();
            _services = services.Clone();
            _isBuilded = false;
        }

        /// <summary>
        /// 获取或设置 程序集查找器
        /// </summary>
        public IAllAssemblyFinder AssemblyFinder { get; set; }

        /// <summary>
        /// 获取 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// 开始构建依赖注入映射
        /// </summary>
        /// <returns>服务提供者</returns>
        public IServiceProvider Build()
        {
            if (_isBuilded)
            {
                return ServiceProvider;
            }

            //设置各个框架的DependencyResolver
            Assembly[] assemblies = AssemblyFinder.FindAll();

            AddCustomTypes(_services);

            ServiceProvider = BuildAndSetResolver(_services, assemblies);
            _isBuilded = true;
            return ServiceProvider;
        }

        /// <summary>
        /// 添加自定义服务映射
        /// </summary>
        /// <param name="services">服务信息集合</param>
        protected abstract void AddCustomTypes(IServiceCollection services);

        /// <summary>
        /// 重写以实现构建服务并设置各个平台的Resolver
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        /// <param name="assemblies">要检索的程序集集合</param>
        /// <returns>服务提供者</returns>
        protected abstract IServiceProvider BuildAndSetResolver(IServiceCollection services, Assembly[] assemblies);
    }
}