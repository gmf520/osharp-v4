// -----------------------------------------------------------------------
//  <copyright file="RegisterExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-04-02 14:17</last-date>
// -----------------------------------------------------------------------

using System;

using Autofac;

using Microsoft.AspNet.SignalR.Hubs;


namespace OSharp.Autofac.SignalR
{
    /// <summary>
    /// Autofac注册扩展
    /// </summary>
    public static class RegisterExtensions
    {
        /// <summary>
        /// 注册生命周期作用域相关的类型
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterLifetimeHubManager(this ContainerBuilder builder)
        {
            bool flag = builder == null;
            if (flag)
            {
                throw new ArgumentNullException("builder");
            }
            builder.RegisterType<LifetimeHubManager>().SingleInstance();
            builder.RegisterType<AutofacHubActivator>().As<IHubActivator>().SingleInstance();
        }
    }
}