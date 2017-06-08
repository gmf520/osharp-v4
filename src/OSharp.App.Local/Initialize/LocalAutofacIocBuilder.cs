// -----------------------------------------------------------------------
//  <copyright file="LocalAutofacIocBuilder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-12 15:23</last-date>
// -----------------------------------------------------------------------

using System;
using System.Reflection;

using Autofac;

using OSharp.Autofac;
using OSharp.Core.Dependency;
using OSharp.Core.Security;


namespace OSharp.App.Local.Initialize
{
    /// <summary>
    /// 本地程序-Autofac依赖注入初始化
    /// </summary>
    public class LocalAutofacIocBuilder : IocBuilderBase
    {
        /// <summary>
        /// 初始化一个<see cref="LocalAutofacIocBuilder"/>类型的新实例
        /// </summary>
        /// <param name="services">服务信息集合</param>
        public LocalAutofacIocBuilder(IServiceCollection services)
            : base(services)
        { }

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
            services.AddSingleton<IFunctionHandler, NullFunctionHandler>();
        }

        /// <summary>
        /// 构建服务并设置本地程序平台的Resolver
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        /// <param name="assemblies">要检索的程序集集合</param>
        /// <returns>服务提供者</returns>
        protected override IServiceProvider BuildAndSetResolver(IServiceCollection services, Assembly[] assemblies)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Populate(services);
            IContainer container = builder.Build();
            LocalIocResolver.Container = container;
            Resolver = container.Resolve<IIocResolver>();
            return Resolver.Resolve<IServiceProvider>();
        }
    }
}