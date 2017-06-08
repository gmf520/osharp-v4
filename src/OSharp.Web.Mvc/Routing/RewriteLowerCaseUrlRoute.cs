// -----------------------------------------------------------------------
//  <copyright file="RewriteLowerCaseUrlRoute.cs" company="OSharp��Դ�Ŷ�">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>������</last-editor>
//  <last-date>2014-08-29 15:12</last-date>
// -----------------------------------------------------------------------

using System.Web;
using System.Web.Routing;


namespace OSharp.Web.Mvc.Routing
{
    /// <summary>
    /// UrlСдת�������� ��дRoute���GetVirtualPath��������Url�зǲ����ַ�ת��ΪСд
    /// </summary>
    public class RewriteLowerCaseUrlRoute : Route
    {
        #region �ֶ�

        private static readonly string[] RequireKeys = new[] { "area", "controller", "action", "plugin" };

        #endregion

        #region ���캯��

        /// <summary>
        /// ��ʼ��һ��<see cref="RewriteLowerCaseUrlRoute"/>�����ʵ��
        /// </summary>
        /// <param name="url"> </param>
        /// <param name="routeHandler"> </param>
        public RewriteLowerCaseUrlRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        { }

        /// <summary>
        /// ��ʼ��һ��<see cref="RewriteLowerCaseUrlRoute"/>�����ʵ��
        /// </summary>
        /// <param name="url"> </param>
        /// <param name="defaults"> </param>
        /// <param name="routeHandler"> </param>
        public RewriteLowerCaseUrlRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        { }

        /// <summary>
        /// ��ʼ��һ��<see cref="RewriteLowerCaseUrlRoute"/>�����ʵ��
        /// </summary>
        /// <param name="url"> </param>
        /// <param name="defaults"> </param>
        /// <param name="constraints"> </param>
        /// <param name="routeHandler"> </param>
        public RewriteLowerCaseUrlRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        { }

        /// <summary>
        /// ��ʼ��һ��<see cref="RewriteLowerCaseUrlRoute"/>�����ʵ��
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

        #region ���з���

        /// <summary>
        /// ���ͷָ����URL
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
        /// ��Url�зǲ����ַ�ת��ΪСд�ָ�����ʽ
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