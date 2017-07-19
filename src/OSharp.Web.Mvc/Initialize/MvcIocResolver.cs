// -----------------------------------------------------------------------
//  <copyright file="MvcIocResolver.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-06 15:27</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Web.Mvc;

using OSharp.Core.Dependency;


namespace OSharp.Web.Mvc.Initialize
{
    /// <summary>
    /// MVC依赖注入对象解析器
    /// </summary>
    public class MvcIocResolver : IIocResolver
    {
        /// <summary>
        /// 从全局容器中解析对象委托
        /// </summary>
        public static Func<Type, object> GlobalResolveFunc { private get; set; }

        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            try
            {
                return DependencyResolver.Current.GetService<T>();
            }
            catch (Exception)
            {
                if (GlobalResolveFunc != null)
                {
                    return (T)GlobalResolveFunc(typeof(T));
                }
                return default(T);
            }
        }

        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            try
            {
                return DependencyResolver.Current.GetService(type);
            }
            catch (Exception)
            {
                if (GlobalResolveFunc != null)
                {
                    return GlobalResolveFunc(type);
                }
                return null;
            }
            
        }

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public IEnumerable<T> Resolves<T>()
        {
            return DependencyResolver.Current.GetServices<T>();
        }

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public IEnumerable<object> Resolves(Type type)
        {
            return DependencyResolver.Current.GetServices(type);
        }
    }
}