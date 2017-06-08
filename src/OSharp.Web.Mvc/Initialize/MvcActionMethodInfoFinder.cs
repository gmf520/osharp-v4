// -----------------------------------------------------------------------
//  <copyright file="MvcActionMethodInfoFinder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-12 21:07</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

using OSharp.Core.Security;
using OSharp.Utility.Extensions;
using OSharp.Web.Mvc.Properties;


namespace OSharp.Web.Mvc.Initialize
{
    /// <summary>
    /// MVC功能方法查找器
    /// </summary>
    public class MvcActionMethodInfoFinder : IFunctionMethodInfoFinder
    {
        /// <summary>
        /// 查找指定条件的功能方法信息
        /// </summary>
        /// <param name="type">控制器类型</param>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        public MethodInfo[] Find(Type type, Func<MethodInfo, bool> predicate)
        {
            return FindAll(type).Where(predicate).ToArray();
        }

        /// <summary>
        /// 从指定类型查找功能方法信息
        /// </summary>
        /// <param name="type">控制器类型</param>
        /// <returns></returns>
        public MethodInfo[] FindAll(Type type)
        {
            if (!typeof(Controller).IsAssignableFrom(type))
            {
                throw new InvalidOperationException(Resources.ActionMethodInfoFinder_TypeNotMvcControllerType.FormatWith(type.FullName));
            }
            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(m => typeof(ActionResult).IsAssignableFrom(m.ReturnType)
                    || m.ReturnType.IsGenericType
                        && m.ReturnType.GetGenericTypeDefinition() == typeof(Task<>)
                        && typeof(ActionResult).IsAssignableFrom(m.ReturnType.GetGenericArguments()[0]))
                    .ToArray();
            return methods;
        }
    }
}