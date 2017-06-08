// -----------------------------------------------------------------------
//  <copyright file="HttpConfigurationExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-18 15:00</last-date>
// -----------------------------------------------------------------------

using System.Web.Http;


namespace OSharp.Web.Http.Extensions
{
    /// <summary>
    /// HttpConfiguration扩展
    /// </summary>
    public static class HttpConfigurationExtensions
    {
        /// <summary>
        /// 注册WebApi默认路由
        /// </summary>
        public static void MapDefaultRoutes(this HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            //config.Routes.MapHttpRoute("ActionApi", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }
    }
}