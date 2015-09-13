// -----------------------------------------------------------------------
//  <copyright file="AutofacIocInitializerBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-01 22:41</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

using Autofac;

using OSharp.Core.Dependency;
using OSharp.SiteBase.Initialize;


namespace OSharp.Autofac
{
    /// <summary>
    /// Autofac依赖注入初始化器实现
    /// </summary>
    public abstract class AutofacIocInitializerBase : IocInitializerBase
    {
        /// <summary>
        /// 初始化一个<see cref="AutofacIocInitializerBase"/>类型的新实例
        /// </summary>
        protected AutofacIocInitializerBase()
        {
            ContainerBuilder builder = new ContainerBuilder();
            Container = builder.Build();
        }

        /// <summary>
        /// 获取或设置 Autofac组合IContainer
        /// </summary>
        protected IContainer Container { get; set; }

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
    }
}