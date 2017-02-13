// -----------------------------------------------------------------------
//  <copyright file="ObjectFactory.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-06 20:36</last-date>
// -----------------------------------------------------------------------

using System;


namespace OSharp.Core.Dependency
{
    /// <summary>
    /// 对象创建委托
    /// </summary>
    /// <param name="provider">服务提供者</param>
    /// <param name="args">构造函数的参数</param>
    /// <returns></returns>
    public delegate object ObjectFactory(IServiceProvider provider, object[] args);
}