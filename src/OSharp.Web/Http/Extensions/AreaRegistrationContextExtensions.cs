using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;


namespace OSharp.Web.Http.Extensions
{
    /// <summary>
    /// <see cref="AreaRegistrationContext" 的扩展类/>
    /// </summary>
    public static class AreaRegistrationContextExtensions
    {
        /// <summary>
        /// 映射指定的URL路由
        /// </summary>
        /// <param name="context">当前区域和路由集合信息。</param>
        /// <param name="name">要映射的路由的名称。</param>
        /// <param name="routeTemplate">路由的路由模板。</param>
        /// <returns>路由相关信息</returns>
        public static Route MapHttpRoute(this AreaRegistrationContext context, string name, string routeTemplate)
        {
            return MapHttpRoute(context, name, routeTemplate, null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">当前区域和路由集合信息。</param>
        /// <param name="name">要映射的路由的名称。</param>
        /// <param name="routeTemplate">路由的路由模板。</param>
        /// <param name="defaults">一个包含默认路由值的对象。</param>
        /// <returns>路由相关信息</returns>
        public static Route MapHttpRoute(this AreaRegistrationContext context, string name, string routeTemplate, object defaults)
        {
            return MapHttpRoute(context, name, routeTemplate, defaults, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">当前区域和路由集合信息。</param>
        /// <param name="name">要映射的路由的名称。</param>
        /// <param name="routeTemplate">路由的路由模板。</param>
        /// <param name="defaults">一个包含默认路由值的对象。</param>
        /// <param name="constraints">一组表达式，用于指定 routeTemplate 的值。</param>
        /// <returns>路由相关信息</returns>
        public static Route MapHttpRoute(this AreaRegistrationContext context, string name, string routeTemplate, object defaults, object constraints)
        {
            Route route = context.Routes.MapHttpRoute(name, routeTemplate, defaults, constraints);
            if (route.DataTokens == null)
            {
                route.DataTokens = new RouteValueDictionary();
            }
            route.DataTokens.Add("area", context.AreaName);
            return route;
        }
    }
}
