// -----------------------------------------------------------------------
//  <copyright file="LocalAutofacIocInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-08 19:24</last-date>
// -----------------------------------------------------------------------

using System;
using System.Reflection;

using Autofac;

using OSharp.Autofac;
using OSharp.Core.Dependency;
using OSharp.Core.Initialize;


namespace OSharp.App.Local.Initialize
{
    /// <summary>
    /// 本地程序-Autofac依赖注入初始化
    /// </summary>
    public class LocalAutofacIocInitializer : IocInitializerBase
    {
        /// <summary>
        /// 获取 依赖注入解析器
        /// </summary>
        public IIocResolver Resolver { get; private set; }

        /// <summary>
        /// 添加自定义服务映射
        /// </summary>
        /// <param name="services">服务信息集合</param>
        protected override void AddCustomTypes(IServiceCollection services)
        {
            services.AddInstance(this);
            services.AddSingleton<IIocResolver, LocalIocResolver>();
        }

        /// <summary>
        /// 将服务构建成服务提供者<see cref="IServiceProvider"/>的实例
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        /// <param name="assemblies">要检索的程序集集合</param>
        /// <returns>服务提供者</returns>
        protected override IServiceProvider BuildServiceProvider(IServiceCollection services, Assembly[] assemblies)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Populate(services);
            IContainer container = builder.Build();
            LocalIocResolver.Container = container;
            Resolver = container.Resolve<IIocResolver>();
            return container.Resolve<IServiceProvider>();
        }
    }
}