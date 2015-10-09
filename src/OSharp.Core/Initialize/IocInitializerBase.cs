// -----------------------------------------------------------------------
//  <copyright file="IocInitializerBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-08 19:16</last-date>
// -----------------------------------------------------------------------

using System;
using System.Reflection;

using OSharp.Core.Context;
using OSharp.Core.Dependency;
using OSharp.Core.Reflection;


namespace OSharp.Core.Initialize
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
            AssemblyFinder = new DirectoryAssemblyFinder();
        }

        /// <summary>
        /// 获取或设置 程序集查找器
        /// </summary>
        public IAssemblyFinder AssemblyFinder { get; set; }

        /// <summary>
        /// 初始化依赖注入
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        public void Initialize(IServiceCollection services)
        {
            //设置各个框架的DependencyResolver
            Assembly[] assemblies = AssemblyFinder.FindAll();

            AddCustomTypes(services);

            IServiceProvider provider = BuildServiceProvider(services, assemblies);
            OSharpContext.IocServiceProvider = provider;
        }

        /// <summary>
        /// 添加自定义服务映射
        /// </summary>
        /// <param name="services">服务信息集合</param>
        protected virtual void AddCustomTypes(IServiceCollection services)
        { }

        /// <summary>
        /// 将服务构建成服务提供者<see cref="IServiceProvider"/>的实例
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        /// <param name="assemblies">要检索的程序集集合</param>
        /// <returns>服务提供者</returns>
        protected abstract IServiceProvider BuildServiceProvider(IServiceCollection services, Assembly[] assemblies);
    }
}