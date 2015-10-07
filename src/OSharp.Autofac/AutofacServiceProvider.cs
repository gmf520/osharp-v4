// -----------------------------------------------------------------------
//  <copyright file="AutofacServiceProvider.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-07 9:54</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;


namespace OSharp.Autofac
{
    /// <summary>
    /// Autofac服务提供者
    /// </summary>
    public class AutofacServiceProvider : IServiceProvider
    {
        private readonly IComponentContext _context;

        /// <summary>
        /// 初始化一个<see cref="AutofacServiceProvider"/>类型的新实例
        /// </summary>
        public AutofacServiceProvider(IComponentContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 获取指定类型的服务对象。
        /// </summary>
        /// <returns>
        /// <paramref name="serviceType"/> 类型的服务对象。 - 或 - 如果没有 <paramref name="serviceType"/> 类型的服务对象，则为 null。
        /// </returns>
        /// <param name="serviceType">一个对象，它指定要获取的服务对象的类型。</param><filterpriority>2</filterpriority>
        public object GetService(Type serviceType)
        {
            return _context.ResolveOptional(serviceType);
        }
    }
}