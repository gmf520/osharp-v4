// -----------------------------------------------------------------------
//  <copyright file="AutofacMvcIocInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-01 22:45</last-date>
// -----------------------------------------------------------------------

using System.Reflection;
using System.Web.Mvc;

using Autofac;
using Autofac.Integration.Mvc;

using OSharp.Core.Dependency;
using OSharp.Core.Initialize;
using OSharp.SiteBase.Dependency;


namespace OSharp.Autofac
{
    /// <summary>
    /// MVC的依赖注入初始化器Autofac实现
    /// </summary>
    public class AutofacMvcIocInitializer : AutofacIocInitializerBase, IMvcIocInitializer
    {
        /// <summary>
        /// 注册自定义类型
        /// </summary>
        protected override void RegisterCustomTypes()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterInstance(this).As<IIocInitializer>().SingleInstance();
            builder.RegisterType<MvcIocResolver>().As<IIocResolver>().SingleInstance();
            builder.Update(Container);
        }

        /// <summary>
        /// 重写以实现设置Mvc框架的DependencyResolver
        /// </summary>
        /// <param name="assemblies"></param>
        protected override void SetResolver(Assembly[] assemblies)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(assemblies).AsSelf().PropertiesAutowired();
            builder.RegisterFilterProvider();
            builder.Update(Container);
            IDependencyResolver resolver = new AutofacDependencyResolver(Container);
            DependencyResolver.SetResolver(resolver);
        }
    }
}