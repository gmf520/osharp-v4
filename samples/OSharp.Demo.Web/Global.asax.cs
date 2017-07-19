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
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

using OSharp.Utility.Logging;
using OSharp.Web.Http.Extensions;
using OSharp.Web.Mvc.Routing;


namespace OSharp.Demo.Web
{
    public class Global : HttpApplication
    {
        private ILogger _logger;

        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RoutesRegister();
        }

        private static void RoutesRegister()
        {
            GlobalConfiguration.Configuration.MapDefaultRoutes();

            RouteCollection routes = RouteTable.Routes;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapLowerCaseUrlRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "OSharp.Demo.Web.Controllers" });
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            GetLogger();
            Exception ex = Server.GetLastError();
            _logger.Fatal("全局异常Application_Error", ex);
        }
        
        private void GetLogger()
        {
            if (_logger == null)
            {
                _logger = LogManager.GetLogger<Global>();
            }
        }
    }
}