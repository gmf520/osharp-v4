// -----------------------------------------------------------------------
//  <copyright file="Global.asax.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-29 20:35</last-date>
// -----------------------------------------------------------------------

using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using OSharp.Autofac.Http;
using OSharp.Autofac.Mvc;
using OSharp.Autofac.SignalR;
using OSharp.Core;
using OSharp.Core.Caching;
using OSharp.Core.Dependency;
using OSharp.Demo.Dtos;
using OSharp.Logging.Log4Net;
using OSharp.Web.Mvc.Routing;


namespace OSharp.Demo.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RoutesRegister();

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
            
            IServicesBuilder builder = new ServicesBuilder();
            IServiceCollection services = builder.Build();
            services.AddLog4NetServices();
            services.AddDataServices();

            IFrameworkInitializer initializer = new FrameworkInitializer();
            initializer.Initialize(new MvcAutofacIocBuilder(services));
            initializer.Initialize(new WebApiAutofacIocBuilder(services));
            //initializer.Initialize(new SignalRAutofacIocBuilder(services));
        }
    }
}