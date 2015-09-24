// -----------------------------------------------------------------------
//  <copyright file="WebApiActionMethodInfoFinder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-21 18:48</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;

using OSharp.Core.Reflection;
using OSharp.SiteBase.Properties;
using OSharp.Utility.Extensions;


namespace OSharp.SiteBase.Http
{
    /// <summary>
    /// WebApi功能查找器
    /// </summary>
    public class WebApiActionMethodInfoFinder : IMethodInfoFinder
    {
        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <param name="type">控制器类型</param>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        public MethodInfo[] Find(Type type, Func<MethodInfo, bool> predicate)
        {
            return FindAll(type).Where(predicate).ToArray();
        }

        /// <summary>
        /// 从指定类型查找方法信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public MethodInfo[] FindAll(Type type)
        {
            if (!typeof(ApiController).IsAssignableFrom(type))
            {
                throw new InvalidOperationException(Resources.WebApiActionMethodInfoFinder_TypeNotApiControllerType.FormatWith(type.FullName));
            }
            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(m => typeof(IHttpActionResult).IsAssignableFrom(m.ReturnType) || m.ReturnType == typeof(Task<IHttpActionResult>))
                .ToArray();
            return methods;
        }
    }
}