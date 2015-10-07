// -----------------------------------------------------------------------
//  <copyright file="AutofacServiceScope.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-07 10:04</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using OSharp.Core.Dependency;
using OSharp.Utility;


namespace OSharp.Autofac
{
    /// <summary>
    /// Autofac服务生命周期范围
    /// </summary>
    public class AutofacServiceScope : Disposable, IServiceScope
    {
        private readonly ILifetimeScope _lifetimeScope;

        /// <summary>
        /// 初始化一个<see cref="AutofacServiceScope"/>类型的新实例
        /// </summary>
        public AutofacServiceScope(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
            ServiceProvider = _lifetimeScope.Resolve<IServiceProvider>();
        }

        /// <summary>
        /// 获取 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 重写以实现释放派生类资源的逻辑
        /// </summary>
        protected override void Disposing()
        {
            _lifetimeScope.Dispose();
        }
    }
}