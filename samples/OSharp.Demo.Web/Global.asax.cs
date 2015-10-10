// -----------------------------------------------------------------------
//  <copyright file="Global.asax.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-29 20:35</last-date>
// -----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using OSharp.Autofac.Http;
using OSharp.Autofac.Mvc;
using OSharp.Autofac.SignalR;
using OSharp.Core;
using OSharp.Core.Caching;
using OSharp.Core.Initialize;
using OSharp.Demo.Dtos;
using OSharp.SiteBase.Initialize;
using OSharp.Web.Http.Initialize;
using OSharp.Web.Mvc.Initialize;
using OSharp.Web.Mvc.Routing;
using OSharp.Web.SignalR.Initialize;


namespace OSharp.Demo.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RoutesRegister();
            DtoMappers.MapperRegister();

            //Initialize();
        }

        private static void RoutesRegister()
        {
            RouteCollection routes = RouteTable.Routes;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapLowerCaseUrlRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "OSharp.Demo.Web.Controllers" });
        }

        private static void Initialize()
        {
            ICacheProvider provider = new RuntimeMemoryCacheProvider();
            CacheManager.SetProvider(provider, CacheLevel.First);

            IBasicLoggingInitializer loggingInitializer = new Log4NetLoggingInitializer();
            //Mvc初始化
            MvcInitializeOptions mvcOptions = new MvcInitializeOptions(loggingInitializer, new MvcAutofacIocInitializer());
            IFrameworkInitializer initializer = new MvcFrameworkInitializer(mvcOptions);
            initializer.Initialize();

            //WebApi初始化
            WebApiInitializeOptions apiOptions = new WebApiInitializeOptions(loggingInitializer, new WebApiAutofacIocInitializer());
            initializer = new WebApiFrameworkInitializer(apiOptions);
            initializer.Initialize();

            ////SignalR初始化
            //SignalRInitializeOptions signalrOptions = new SignalRInitializeOptions(loggingInitializer, new SignalRAutofacIocInitializer());
            //initializer = new SignalRFrameworkInitializer(signalrOptions);
            //initializer.Initialize();
        }
    }
}