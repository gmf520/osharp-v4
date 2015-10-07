// -----------------------------------------------------------------------
//  <copyright file="IServiceScopeFactory.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-06 20:32</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OSharp.Core.Dependency
{
    /// <summary>
    /// 定义<see cref="IServiceScope"/>的创建
    /// </summary>
    public interface IServiceScopeFactory
    {
        /// <summary>
        /// 创建服务作用范围
        /// </summary>
        /// <returns></returns>
        IServiceScope CreateScope();
    }
}