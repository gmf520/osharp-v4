// -----------------------------------------------------------------------
//  <copyright file="ISingletonDependency.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-29 18:21</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Core.Dependency
{
    /// <summary>
    /// 实现此接口的类型将被注册为<see cref="LifetimeStyle.Singleton"/>模式
    /// </summary>
    public interface ISingletonDependency : IDependency
    { }
}