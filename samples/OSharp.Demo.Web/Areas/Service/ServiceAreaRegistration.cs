// -----------------------------------------------------------------------
//  <copyright file="ServiceAreaRegistration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-06 1:11</last-date>
// -----------------------------------------------------------------------

using System.Web.Http;
using System.Web.Mvc;

using OSharp.Web.Mvc.Routing;


namespace OSharp.Demo.Web.Areas.Service
{
    public class ServiceAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Service"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Routes.MapHttpRoute(
                "ServiceActionApi",
                "api/service/{controller}/{action}/{id}",
                new { area = "Service", id = RouteParameter.Optional }
                );
            config.Routes.MapHttpRoute(
                "ServiceApi",
                "api/service/{controller}/{id}",
                new { area = "Service", id = RouteParameter.Optional }
                );

            context.MapLowerCaseUrlRoute(
                "Service_default",
                "api/service/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}