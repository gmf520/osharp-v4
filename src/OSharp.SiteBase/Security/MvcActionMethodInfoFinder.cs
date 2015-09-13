// -----------------------------------------------------------------------
//  <copyright file="MvcActionMethodInfoFinder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-08 2:23</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

using OSharp.Core.Reflection;
using OSharp.Utility.Extensions;


namespace OSharp.SiteBase.Security
{
    /// <summary>
    /// MVC功能查找器
    /// </summary>
    public class MvcActionMethodInfoFinder : IMethodInfoFinder
    {
        #region Implementation of IFinder<out MethodInfo>

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
        /// <param name="type">控制器类型</param>
        /// <returns></returns>
        public MethodInfo[] FindAll(Type type)
        {
            if (!typeof(Controller).IsAssignableFrom(type))
            {
                throw new InvalidOperationException("类型“{0}”不是MVC控制器类型".FormatWith(type.FullName));
            }
            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(m => typeof(ActionResult).IsAssignableFrom(m.ReturnType) || m.ReturnType == typeof(Task<ActionResult>))
                .ToArray();
            return methods;
        }

        #endregion
    }
}