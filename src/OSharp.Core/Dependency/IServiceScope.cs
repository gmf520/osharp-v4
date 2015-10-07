// -----------------------------------------------------------------------
//  <copyright file="IServiceScope.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-06 20:31</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OSharp.Core.Dependency
{
    /// <summary>
    /// 描述服务生命周期范围
    /// </summary>
    public interface IServiceScope : IDisposable
    {
        /// <summary>
        /// 获取 服务提供者
        /// </summary>
        IServiceProvider ServiceProvider { get; }
    }
}