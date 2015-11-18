// 源文件头信息：
// <copyright file="AreaHttpControllerSelector.cs">
// Copyright(c)2012-2014 66SOFT.All rights reserved.
// CLR版本：4.0.30319.239
// 开发组织：柳柳软件坊
// 公司网站：http://www.66soft.net
// 所属工程：Gmf.Web
// 最后修改：郭明锋
// 最后修改：2014/05/28 13:33
// </copyright>

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

using OSharp.Utility.Extensions;
using OSharp.Web.Http.Properties;


namespace OSharp.Web.Http.Selectors
{
    /// <summary>
    /// WebApi区域控制器选择器
    /// </summary>
    public class AreaHttpControllerSelector : DefaultHttpControllerSelector
    {
        private const string AreaRouteVariableName = "area";

        private readonly HttpConfiguration _configuration;
        private readonly Lazy<ConcurrentDictionary<string, Type>> _apiControllerTypes;

        /// <summary>
        /// 初始化一个<see cref="AreaHttpControllerSelector"/>类型的新实例
        /// </summary>
        public AreaHttpControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        {
            _configuration = configuration;
            _apiControllerTypes = new Lazy<ConcurrentDictionary<string, Type>>(GetControllerTypes);
        }

        /// <summary>
        /// 由Http请求获取控制台描述信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            return GetApiController(request);
        }

        #region 私有方法

        private static ConcurrentDictionary<string, Type> GetControllerTypes()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Dictionary<string, Type> types = assemblies.SelectMany(a => a.GetTypes()
                .Where(t => !t.IsAbstract && t.Name.EndsWith(ControllerSuffix, StringComparison.OrdinalIgnoreCase)
                    && typeof(IHttpController).IsAssignableFrom(t))).ToDictionary(t => t.FullName, t => t);
            return new ConcurrentDictionary<string, Type>(types);
        }

        private HttpControllerDescriptor GetApiController(HttpRequestMessage request)
        {
            string areaName = GetAreaName(request);
            string controllerName = GetControllerName(request);
            if (controllerName == null)
            {
                throw new InvalidOperationException(Resources.ApiControllerNameIsNull);
            }
            Type type = GetControllerType(areaName, controllerName);
            return new HttpControllerDescriptor(_configuration, controllerName, type);
        }

        private static string GetAreaName(HttpRequestMessage request)
        {
            IHttpRouteData data = request.GetRouteData();
            object areaName;
            if (data.Route == null || data.Route.DataTokens == null)
            {
                if (data.Values.TryGetValue(AreaRouteVariableName, out areaName))
                {
                    return areaName.ToString();
                }
                return null;
            }
            return data.Route.DataTokens.TryGetValue(AreaRouteVariableName, out areaName) ? areaName.ToString() : null;
        }

        private Type GetControllerType(string areaName, string controllerName)
        {
            IEnumerable<KeyValuePair<string, Type>> query = _apiControllerTypes.Value.AsEnumerable();
            query = string.IsNullOrEmpty(areaName) ? query.WithoutAreaName() : query.ByAreaName(areaName);
            Type type = query.ByControllerName(controllerName).Select(m => m.Value).SingleOrDefault();
            if (type == null)
            {
                throw new Exception("未找到名称为“{0}”的Api控制器。".FormatWith(controllerName));
            }
            return type;
        }

        #endregion
    }


    internal static class ControllerTypeSpecifications
    {
        internal static IEnumerable<KeyValuePair<string, Type>> ByAreaName(this IEnumerable<KeyValuePair<string, Type>> query, string areaName)
        {
            string areaNameToFind = string.Format(CultureInfo.InvariantCulture, ".{0}.", areaName);
            return query.Where(x => x.Key.IndexOf(areaNameToFind, StringComparison.OrdinalIgnoreCase) != -1);
        }

        internal static IEnumerable<KeyValuePair<string, Type>> WithoutAreaName(this IEnumerable<KeyValuePair<string, Type>> query)
        {
            return query.Where(x => x.Key.IndexOf(".areas.", StringComparison.OrdinalIgnoreCase) == -1);
        }

        internal static IEnumerable<KeyValuePair<string, Type>> ByControllerName(this IEnumerable<KeyValuePair<string, Type>> query,
            string controllerName)
        {
            string controllerNameToFind = string.Format(CultureInfo.InvariantCulture,
                ".{0}{1}",
                controllerName,
                DefaultHttpControllerSelector.ControllerSuffix);
            return query.Where(x => x.Key.EndsWith(controllerNameToFind, StringComparison.OrdinalIgnoreCase));
        }
    }
}