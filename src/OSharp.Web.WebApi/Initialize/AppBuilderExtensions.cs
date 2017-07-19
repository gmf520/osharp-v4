// -----------------------------------------------------------------------
//  <copyright file="AppBuilderExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-18 17:05</last-date>
// -----------------------------------------------------------------------

using System.Net.Http.Formatting;
using System.Web.Http;

using Microsoft.Owin.Security.OAuth;

using OSharp.Core;
using OSharp.Core.Dependency;
using OSharp.Utility;
using OSharp.Web.Http.Filters;
using OSharp.Web.Http.Handlers;

using Owin;


namespace OSharp.Web.Http.Initialize
{
    /// <summary>
    /// <see cref="IAppBuilder"/>初始化扩展
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// 初始化WebApi框架
        /// </summary>
        public static IAppBuilder UseOsharpWebApi(this IAppBuilder app, IIocBuilder iocBuilder)
        {
            iocBuilder.CheckNotNull("iocBuilder");
            IFrameworkInitializer initializer = new FrameworkInitializer();
            initializer.Initialize(iocBuilder);
            return app;
        }

        /// <summary>
        /// 初始化WebApi
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IAppBuilder ConfigureWebApi(this IAppBuilder app)
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;

            //注册请求生命周期Scope的处理器
            config.MessageHandlers.Add(new RequestLifetimeScopeHandler());

            //全局异常处理
            config.Filters.Add(new ExceptionHandlingAttribute());
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.EnsureInitialized();
            return app;
        }
    }
}