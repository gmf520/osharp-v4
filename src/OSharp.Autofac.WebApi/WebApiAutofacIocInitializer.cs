// -----------------------------------------------------------------------
//  <copyright file="WebApiAutofacIocInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-08 19:28</last-date>
// -----------------------------------------------------------------------

using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;

using Autofac;
using Autofac.Integration.WebApi;

using OSharp.Core.Dependency;
using OSharp.Core.Initialize;
using OSharp.Core.Security;
using OSharp.Web.Http.Initialize;


namespace OSharp.Autofac.Http
{
    /// <summary>
    /// WebApi-Autofac依赖注入初始化类
    /// </summary>
    public class WebApiAutofacIocInitializer : IocInitializerBase
    {
        /// <summary>
        /// 添加自定义服务映射
        /// </summary>
        /// <param name="services">服务信息集合</param>
        protected override void AddCustomTypes(IServiceCollection services)
        {
            services.AddInstance(this);
            services.AddSingleton<IIocResolver, WebApiIocResolver>();
            services.AddSingleton<IFunctionHandler, WebApiFunctionHandler>();
        }

        /// <summary>
        /// 构建服务并设置WebApi平台的Resolver
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        /// <param name="assemblies">要检索的程序集集合</param>
        protected override void BuildAndSetResolver(IServiceCollection services, Assembly[] assemblies)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterApiControllers(assemblies).AsSelf().PropertiesAutowired();
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            builder.RegisterWebApiModelBinderProvider();
            builder.Populate(services);
            IContainer container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}