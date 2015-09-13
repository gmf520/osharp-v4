// -----------------------------------------------------------------------
//  <copyright file="Global.asax.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-29 21:58</last-date>
// -----------------------------------------------------------------------

using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using OSharp.Autofac;
using OSharp.Core;
using OSharp.Core.Caching;
using OSharp.Demo.Dtos;
using OSharp.SiteBase.Initialize;
using OSharp.Web.Mvc.Routing;


namespace OSharp.Demo.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RoutesRegister();
            DtoMappers.MapperRegister();

            Initialize();
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

            IFrameworkInitializer initializer = new FrameworkInitializer()
            {
                MvcIocInitializer = new AutofacMvcIocInitializer()
            };
            initializer.Initialize();
        }
    }
}