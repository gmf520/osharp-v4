// -----------------------------------------------------------------------
//  <copyright file="AppBuilderExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-29 23:06</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNet.SignalR;

using OSharp.Core;
using OSharp.Core.Dependency;
using OSharp.Utility;

using Owin;


namespace OSharp.Web.SignalR.Initialize
{
    /// <summary>
    /// <see cref="IAppBuilder"/>初始化扩展
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// 初始化SignalR框架
        /// </summary>
        public static IAppBuilder UseOsharpSignalR(this IAppBuilder app, IIocBuilder iocBuilder)
        {
            iocBuilder.CheckNotNull("iocBuilder");
            IFrameworkInitializer initializer = new FrameworkInitializer();
            initializer.Initialize(iocBuilder);
            return app;
        }

        /// <summary>
        /// 初始化SignalR
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IAppBuilder ConfigureSignalR(this IAppBuilder app)
        {
            app.MapSignalR(new HubConfiguration()
            {
                EnableDetailedErrors = true
            });
            return app;
        }
    }
}