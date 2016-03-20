// -----------------------------------------------------------------------
//  <copyright file="SignalRHubMethodInfoFinder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-12 21:04</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

using Microsoft.AspNet.SignalR.Hubs;
using OSharp.Core.Security;
using OSharp.Utility.Extensions;
using OSharp.Web.SignalR.Properties;


namespace OSharp.Web.SignalR.Initialize
{
    /// <summary>
    /// Hub 方法信息查找器
    /// </summary>
    public class SignalRHubMethodInfoFinder : IFunctionMethodInfoFinder
    {
        /// <summary>
        /// 查找指定条件的方法信息
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
            if (!typeof(IHub).IsAssignableFrom(type))
            {
                throw new InvalidOperationException(Resources.HubMethodInfoFinder_TypeNotHubType.FormatWith(type.FullName));
            }
            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(m => m.DeclaringType == type && !m.IsSpecialName).ToArray();
            return methods;
        }
    }
}