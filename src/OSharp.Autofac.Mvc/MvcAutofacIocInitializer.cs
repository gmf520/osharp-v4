// -----------------------------------------------------------------------
//  <copyright file="MvcAutofacIocInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-08 18:00</last-date>
// -----------------------------------------------------------------------

using System;
using System.Reflection;
using System.Web.Mvc;

using Autofac;
using Autofac.Integration.Mvc;

using OSharp.Core.Dependency;
using OSharp.Core.Initialize;
using OSharp.Web.Mvc.Initialize;


namespace OSharp.Autofac.Mvc
{
    /// <summary>
    /// Mvc-Autofac依赖注入初始化类
    /// </summary>
    public class MvcAutofacIocInitializer : IocInitializerBase
    {
        /// <summary>
        /// 添加自定义服务映射
        /// </summary>
        /// <param name="services">服务信息集合</param>
        protected override void AddCustomTypes(IServiceCollection services)
        {
            services.AddInstance(this);
            services.AddSingleton<IIocResolver, MvcIocResolver>();
        }

        /// <summary>
        /// 将服务构建成服务提供者<see cref="IServiceProvider"/>的实例
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        /// <param name="assemblies">程序集集合</param>
        /// <returns>服务提供者</returns>
        protected override IServiceProvider BuildServiceProvider(IServiceCollection services, Assembly[] assemblies)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(assemblies).AsSelf().PropertiesAutowired();
            builder.RegisterFilterProvider();
            builder.Populate(services);
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            return container.Resolve<IServiceProvider>();
        }
        
    }
}