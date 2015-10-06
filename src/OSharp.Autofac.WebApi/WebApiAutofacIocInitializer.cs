// -----------------------------------------------------------------------
//  <copyright file="WebApiAutofacIocInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-23 22:42</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dependencies;

using Autofac;
using Autofac.Integration.WebApi;

using OSharp.Core.Data;
using OSharp.Core.Data.Entity;
using OSharp.Core.Dependency;
using OSharp.Core.Initialize;
using OSharp.Web.Http.Initialize;


namespace OSharp.Autofac.Http
{
    /// <summary>
    /// WebApi-Autofac依赖注入初始化类
    /// </summary>
    public class WebApiAutofacIocInitializer : IocInitializerBase
    {
        /// <summary>
        /// 初始化一个<see cref="WebApiAutofacIocInitializer"/>类型的新实例
        /// </summary>
        public WebApiAutofacIocInitializer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            Container = builder.Build();
        }

        /// <summary>
        /// 获取或设置 Autofac组合IContainer
        /// </summary>
        protected IContainer Container { get; private set; }

        /// <summary>
        /// 重写以返回数据仓储实现类型
        /// </summary>
        /// <returns></returns>
        protected override Type GetRepositoryType()
        {
            return typeof(Repository<,>);
        }

        /// <summary>
        /// 重写以实现数据上下文类型的注册
        /// </summary>
        /// <param name="dbContexTypes">数据上下文类型</param>
        /// <param name="asType">IUnitOfWork类型</param>
        protected override void RegisterDbContextTypes(Type[] dbContexTypes, Type asType)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterTypes(dbContexTypes).As(asType).AsSelf().AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();
            builder.Update(Container);
        }

        /// <summary>
        /// 重写以实现数据仓储类型的注册
        /// </summary>
        /// <param name="repositoryType">数据仓储实现类型</param>
        /// <param name="iRepositoryType">数据仓储接口类型</param>
        protected override void RegisterRepositoryType(Type repositoryType, Type iRepositoryType)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterGeneric(repositoryType).As(iRepositoryType).PropertiesAutowired().InstancePerLifetimeScope();
            builder.Update(Container);
        }

        /// <summary>
        /// 重写以实现依赖注入接口<see cref="IDependency"/>实现类型的注册
        /// </summary>
        /// <param name="types">要注册的类型集合</param>
        protected override void RegisterDependencyTypes<TDependency>(Type[] types)
        {
            ContainerBuilder builder = new ContainerBuilder();
            var builderSource = builder.RegisterTypes(types).AsSelf().AsImplementedInterfaces().PropertiesAutowired();
            Type baseType = typeof(TDependency);
            if (baseType == typeof(ITransientDependency))
            {
                builderSource.InstancePerDependency();
            }
            else if (baseType == typeof(ILifetimeScopeDependency))
            {
                builderSource.InstancePerLifetimeScope();
            }
            else if (baseType == typeof(ISingletonDependency))
            {
                builderSource.SingleInstance();
            }
            builder.Update(Container);
        }

        /// <summary>
        /// 注册自定义类型
        /// </summary>
        protected override void RegisterCustomTypes()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterInstance(this).As<IIocInitializer>().SingleInstance();
            builder.RegisterType<WebApiIocResolver>().As<IIocResolver>().SingleInstance();
            builder.Update(Container);
        }

        /// <summary>
        /// 重写以实现设置Mvc、WebAPI、SignalR等框架的DependencyResolver
        /// </summary>
        /// <param name="assemblies"></param>
        protected override void SetResolver(Assembly[] assemblies)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterApiControllers(assemblies).AsSelf().PropertiesAutowired();
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            builder.RegisterWebApiModelBinderProvider();
            builder.Update(Container);
            IDependencyResolver resolver = new AutofacWebApiDependencyResolver(Container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}