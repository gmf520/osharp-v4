// -----------------------------------------------------------------------
//  <copyright file="IAllAssemblyFinder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-10 12:03</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Dependency;


namespace OSharp.Core.Reflection
{
    /// <summary>
    /// 定义所有程序集查找器
    /// </summary>
    public interface IAllAssemblyFinder : IAssemblyFinder, ISingletonDependency
    { }
}