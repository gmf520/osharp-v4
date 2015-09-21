// -----------------------------------------------------------------------
//  <copyright file="AutofacMvcIocInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-01 22:45</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

using Autofac;
using Autofac.Integration.Mvc;

using OSharp.Core.Configs;
using OSharp.Core.Data.Entity.Logging;
using OSharp.Core.Dependency;
using OSharp.Core.Initialize;
using OSharp.SiteBase.Dependency;


namespace OSharp.Autofac
{
    /// <summary>
    /// Console的依赖注入初始化器Autofac实现
    /// </summary>
    public class AutofacConsoleIocInitializer : AutofacIocInitializerBase, IConsoleIocInitializer
    {
        /// <summary>
        /// 返回基类Container
        /// </summary>
        /// <returns></returns>
        public IContainer GetComtainer()
        {
            return Container;
        }
        /// <summary>
        /// 注册自定义类型
        /// </summary>
        protected override void RegisterCustomTypes()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterInstance(this).As<IIocInitializer>().SingleInstance();
            builder.RegisterType<ConsoleIocResolver>().
                WithParameter(new TypedParameter(typeof(IContainer), Container)).
                As<IIocResolver>().SingleInstance();
            builder.Update(Container);
        }

        /// <summary>
        /// 重写以实现设置Console框架的DependencyResolver
        /// </summary>
        /// <param name="assemblies"></param>
        protected override void SetResolver(Assembly[] assemblies)
        {
            
        }
    }
}