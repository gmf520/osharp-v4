// -----------------------------------------------------------------------
//  <copyright file="RewriteUrlRouteExtensions.cs" company="OSharp��Դ�Ŷ�">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>������</last-editor>
//  <last-date>2014-08-29 15:12</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;


namespace OSharp.Web.Mvc.Routing
{
    /// <summary>
    /// UrlRoute��д������չ��
    /// </summary>
    public static class RewriteUrlRouteExtensions
    {
        /// <summary>
        /// ��·�ɼ��ϵ�UrlRoute��д
        /// </summary>
        public static RewriteLowerCaseUrlRoute MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url)
        {
            return routes.MapLowerCaseUrlRoute(name, url, null, null);
        }

        /// <summary>
        /// ��·�ɼ��ϵ�UrlRoute��д
        /// </summary>
        public static RewriteLowerCaseUrlRoute MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            return routes.MapLowerCaseUrlRoute(name, url, defaults, null);
        }

        /// <summary>
        /// ��·�ɼ��ϵ�UrlRoute��д
        /// </summary>
        public static RewriteLowerCaseUrlRoute MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url, string[] namespaces)
        {
            return routes.MapLowerCaseUrlRoute(name, url, null, null, namespaces);
        }

        /// <summary>
        /// ��·�ɼ��ϵ�UrlRoute��д
        /// </summary>
        public static RewriteLowerCaseUrlRoute MapLowerCaseUrlRoute(this RouteCollection routes,
            string name,
            string url,
            object defaults,
            object constraints)
        {
            return routes.MapLowerCaseUrlRoute(name, url, defaults, constraints, null);
        }

        /// <summary>
        /// ��·�ɼ��ϵ�UrlRoute��д
        /// </summary>
        public static RewriteLowerCaseUrlRoute MapLowerCaseUrlRoute(this RouteCollection routes,
            string name,
            string url,
            object defaults,
            string[] namespaces)
        {
            return routes.MapLowerCaseUrlRoute(name, url, defaults, null, namespaces);
        }

        /// <summary>
        /// ��·�ɼ��ϵ�UrlRoute��д
        /// </summary>
        public static RewriteLowerCaseUrlRoute MapLowerCaseUrlRoute(this RouteCollection routes,
            string name,
            string url,
            object defaults,
            object constraints,
            string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            RewriteLowerCaseUrlRoute route2 = new RewriteLowerCaseUrlRoute(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints),
                DataTokens = new RouteValueDictionary()
            };
            RewriteLowerCaseUrlRoute item = route2;
            if ((namespaces != null) && (namespaces.Length > 0))
            {
                item.DataTokens["Namespaces"] = namespaces;
            }
            routes.Add(name, item);
            return item;
        }

        /// <summary>
        /// ������·�ɵ�UrlRoute��д
        /// </summary>
        public static RewriteLowerCaseUrlRoute MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name, string url)
        {
            return MapLowerCaseUrlRoute(context, name, url, null);
        }

        /// <summary>
        /// ������·�ɵ�UrlRoute��д
        /// </summary>
        public static RewriteLowerCaseUrlRoute MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name, string url, object defaults)
        {
            return MapLowerCaseUrlRoute(context, name, url, defaults, null);
        }

        /// <summary>
        /// ������·�ɵ�UrlRoute��д
        /// </summary>
        public static RewriteLowerCaseUrlRoute MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name, string url, string[] namespaces)
        {
            return MapLowerCaseUrlRoute(context, name, url, null, namespaces);
        }

        /// <summary>
        /// ������·�ɵ�UrlRoute��д
        /// </summary>
        public static RewriteLowerCaseUrlRoute MapLowerCaseUrlRoute(this AreaRegistrationContext context,
            string name,
            string url,
            object defaults,
            object constraints)
        {
            return MapLowerCaseUrlRoute(context, name, url, defaults, constraints, null);
        }

        /// <summary>
        /// ������·�ɵ�UrlRoute��д
        /// </summary>
        public static RewriteLowerCaseUrlRoute MapLowerCaseUrlRoute(this AreaRegistrationContext context,
            string name,
            string url,
            object defaults,
            string[] namespaces)
        {
            return MapLowerCaseUrlRoute(context, name, url, defaults, null, namespaces);
        }

        /// <summary>
        /// ������·�ɵ�UrlRoute��д
        /// </summary>
        public static RewriteLowerCaseUrlRoute MapLowerCaseUrlRoute(this AreaRegistrationContext context,
            string name,
            string url,
            object defaults,
            object constraints,
            string[] namespaces)
        {
            if ((namespaces == null) && (context.Namespaces != null))
            {
                namespaces = context.Namespaces.ToArray();
            }
            RewriteLowerCaseUrlRoute route = MapLowerCaseUrlRoute(context.Routes, name, url, defaults, constraints, namespaces);
            route.DataTokens["area"] = context.AreaName;
            bool flag = (namespaces == null) || (namespaces.Length == 0);
            route.DataTokens["UseNamespaceFallback"] = flag;
            return route;
        }
    }
}