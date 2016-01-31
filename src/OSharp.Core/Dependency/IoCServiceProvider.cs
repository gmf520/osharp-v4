// -----------------------------------------------------------------------
//  <copyright file="IocServiceProvider.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-09 15:57</last-date>
// -----------------------------------------------------------------------

using System;


namespace OSharp.Core.Dependency
{
    /// <summary>
    /// 默认IoC服务提供者实现
    /// </summary>
    public class IocServiceProvider : IServiceProvider
    {
        private readonly IIocResolver _resolver;

        /// <summary>
        /// 初始化一个<see cref="IocServiceProvider"/>类型的新实例
        /// </summary>
        public IocServiceProvider(IIocResolver resolver)
        {
            _resolver = resolver;
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
            return _resolver.Resolve(serviceType);
        }
    }
}