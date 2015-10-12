// -----------------------------------------------------------------------
//  <copyright file="RouteHelper.cs" company="OSharp��Դ�Ŷ�">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>������</last-editor>
//  <last-date>2014-08-29 15:11</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

using OSharp.Utility;


namespace OSharp.Web.Mvc.Routing
{
    /// <summary>
    /// ·�ɸ���������
    /// </summary>
    public static class RouteHelper
    {
        /// <summary>
        /// ��������ռ䵽ָ�����Ƶ�·��������
        /// </summary>
        public static void AddRouteNamespaces(string routeName, params string[] namespaces)
        {
            routeName.CheckNotNull("routeName");
            namespaces.CheckNotNull("namespaces");

            if (namespaces.Length == 0)
            {
                return;
            }

            const string namespacesKey = "Namespaces";
            RouteBase routeBase = RouteTable.Routes[routeName];
            if (routeBase == null || !(routeBase is Route))
            {
                return;
            }
            Route route = routeBase as Route;
            if (!route.DataTokens.ContainsKey(namespacesKey))
            {
                route.DataTokens.Add(namespacesKey, namespaces);
            }
            else
            {
                List<string> existsNamespaces = ((string[])route.DataTokens[namespacesKey]).ToList();
                foreach (string @namespace in namespaces)
                {
                    if (existsNamespaces.All(m => m != @namespace))
                    {
                        existsNamespaces.Add(@namespace);
                    }
                }
                route.DataTokens[namespacesKey] = existsNamespaces.ToArray();
            }
        }
    }
}