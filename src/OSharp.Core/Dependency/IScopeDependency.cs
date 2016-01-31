// -----------------------------------------------------------------------
//  <copyright file="IScopeDependency.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-06 19:31</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Core.Dependency
{
    /// <summary>
    /// 实现此接口的类型将被注册为<see cref="LifetimeStyle.Scoped"/>模式
    /// </summary>
    public interface IScopeDependency : IDependency
    { }
}