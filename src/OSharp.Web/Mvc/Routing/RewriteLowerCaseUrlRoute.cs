// -----------------------------------------------------------------------
//  <copyright file="RewriteLowerCaseUrlRoute.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-08-29 15:12</last-date>
// -----------------------------------------------------------------------

using System.Web;
using System.Web.Routing;


namespace OSharp.Web.Mvc.Routing
{
    /// <summary>
    /// Url小写转换辅助类 重写Route类的GetVirtualPath方法，把Url中非参数字符转换为小写
    /// </summary>
    public class RewriteLowerCaseUrlRoute : Route
    {
        #region 字段

        private static readonly string[] RequireKeys = new[] { "area", "controller", "action", "plugin" };

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化一个<see cref="RewriteLowerCaseUrlRoute"/>类的新实例
        /// </summary>
        /// <param name="url"> </param>
        /// <param name="routeHandler"> </param>
        public RewriteLowerCaseUrlRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        { }

        /// <summary>
        /// 初始化一个<see cref="RewriteLowerCaseUrlRoute"/>类的新实例
        /// </summary>
        /// <param name="url"> </param>
        /// <param name="defaults"> </param>
        /// <param name="routeHandler"> </param>
        public RewriteLowerCaseUrlRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        { }

        /// <summary>
        /// 初始化一个<see cref="RewriteLowerCaseUrlRoute"/>类的新实例
        /// </summary>
        /// <param name="url"> </param>
        /// <param name="defaults"> </param>
        /// <param name="constraints"> </param>
        /// <param name="routeHandler"> </param>
        public RewriteLowerCaseUrlRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        { }

        /// <summary>
        /// 初始化一个<see cref="RewriteLowerCaseUrlRoute"/>类的新实例
        /// </summary>
        /// <param name="url"> </param>
        /// <param name="defaults"> </param>
        /// <param name="constraints"> </param>
        /// <param name="dataTokens"> </param>
        /// <param name="routeHandler"> </param>
        public RewriteLowerCaseUrlRoute(string url,
            RouteValueDictionary defaults,
            RouteValueDictionary constraints,
            RouteValueDictionary dataTokens,
            IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        { }

        #endregion

        #region 公有方法

        /// <summary>
        /// 解释分隔后的URL
        /// </summary>
        /// <param name="httpContext"> </param>
        /// <returns> </returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData result = base.GetRouteData(httpContext);
            if (result == null)
            {
                return null;
            }
            RouteValueDictionary dict = result.Values;
            foreach (string key in RequireKeys)
            {
                if (!dict.ContainsKey(key))
                {
                    continue;
                }
                object value = dict[key];
                if (!(value is string))
                {
                    continue;
                }
                dict[key] = LowerCaseUrlConverter.Restore(value as string);
            }
            return result;
        }

        /// <summary>
        /// 把Url中非参数字符转换为小写分隔的形式
        /// </summary>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            RouteValueDictionary valueDict = new RouteValueDictionary(values);
            foreach (string key in RequireKeys)
            {
                if (!valueDict.ContainsKey(key))
                {
                    continue;
                }
                object value = valueDict[key];
                if (!(value is string))
                {
                    continue;
                }
                valueDict[key] = LowerCaseUrlConverter.Spliter(value as string);
            }
            return base.GetVirtualPath(requestContext, valueDict);
        }

        #endregion
    }
}