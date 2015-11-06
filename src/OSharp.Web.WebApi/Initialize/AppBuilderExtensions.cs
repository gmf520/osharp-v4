// -----------------------------------------------------------------------
//  <copyright file="AppBuilderExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-29 23:04</last-date>
// -----------------------------------------------------------------------

using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Dispatcher;

using Microsoft.Owin.Security.OAuth;

using OSharp.Core;
using OSharp.Core.Dependency;
using OSharp.Utility;
using OSharp.Web.Http.Filters;
using OSharp.Web.Http.Selectors;

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

            config.Services.Replace(typeof(IHttpControllerSelector), new AreaHttpControllerSelector(config));
            config.Routes.MapHttpRoute("ActionApi", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });

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