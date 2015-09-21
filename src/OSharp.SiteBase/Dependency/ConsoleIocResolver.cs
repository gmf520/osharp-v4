// -----------------------------------------------------------------------
//  <copyright file="MvcIocResolver.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-01 23:42</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

using Autofac;

using OSharp.Core.Dependency;


namespace OSharp.SiteBase.Dependency
{
    /// <summary>
    /// Console依赖注入对象获取器
    /// </summary>
    public class ConsoleIocResolver : IIocResolver
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="container"></param>
        public ConsoleIocResolver(IContainer container)
        {
            Container = container;
        }
        private IContainer Container { get; }
        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return Container.Resolve(type);
        }

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public IEnumerable<T> Resolves<T>()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public IEnumerable<object> Resolves(Type type)
        {
            throw new NotImplementedException();
        }
    }
}