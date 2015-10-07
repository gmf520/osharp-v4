// -----------------------------------------------------------------------
//  <copyright file="AutofacServiceScopeFactory.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-07 10:08</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using OSharp.Core.Dependency;


namespace OSharp.Autofac
{
    /// <summary>
    /// Autofac服务生命周期创建工厂，用于创建生命周期范围
    /// </summary>
    public class AutofacServiceScopeFactory : IServiceScopeFactory
    {
        private readonly ILifetimeScope _lifetimeScope;

        /// <summary>
        /// 初始化一个<see cref="AutofacServiceScopeFactory"/>类型的新实例
        /// </summary>
        public AutofacServiceScopeFactory(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        /// <summary>
        /// 创建服务作用范围
        /// </summary>
        /// <returns></returns>
        public IServiceScope CreateScope()
        {
            return new AutofacServiceScope(_lifetimeScope.BeginLifetimeScope());
        }
    }
}