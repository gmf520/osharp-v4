// -----------------------------------------------------------------------
//  <copyright file="SignalRIocResolver.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-06 15:29</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNet.SignalR;

using OSharp.Core.Dependency;


namespace OSharp.Web.SignalR.Initialize
{
    /// <summary>
    /// SignalR依赖注入解析器
    /// </summary>
    public class SignalRIocResolver : IIocResolver
    {
        /// <summary>
        /// 获取或设置 带生命周期作用域的Hub对象解析委托
        /// </summary>
        public static Func<Type, object> LifetimeResolveFunc { private get; set; }

        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            if (LifetimeResolveFunc != null)
            {
                object obj = LifetimeResolveFunc(type);
                if (obj != null)
                {
                    return obj;
                }
            }
            return GlobalHost.DependencyResolver.GetService(type);
        }

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public IEnumerable<T> Resolves<T>()
        {
            return Resolves(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public IEnumerable<object> Resolves(Type type)
        {
            if (LifetimeResolveFunc != null)
            {
                Type typeToResolve = typeof(IEnumerable<>).MakeGenericType(type);
                Array array = LifetimeResolveFunc(typeToResolve) as Array;
                if (array != null)
                {
                    return array.Cast<object>();
                }
            }
            return GlobalHost.DependencyResolver.GetServices(type);
        }
    }
}