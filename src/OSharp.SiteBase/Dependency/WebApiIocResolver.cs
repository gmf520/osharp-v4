// -----------------------------------------------------------------------
//  <copyright file="WebApiIocResolver.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-01 23:45</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using OSharp.Core.Dependency;


namespace OSharp.SiteBase.Dependency
{
    /// <summary>
    /// WebApi依赖注入对象获取器
    /// </summary>
    public class WebApiIocResolver : IIocResolver
    {
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
            return GlobalConfiguration.Configuration.DependencyResolver.GetService(type);
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
            return GlobalConfiguration.Configuration.DependencyResolver.GetServices(type);
        }
    }
}