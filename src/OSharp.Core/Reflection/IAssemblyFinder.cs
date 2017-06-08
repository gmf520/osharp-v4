// -----------------------------------------------------------------------
//  <copyright file="IAssemblyFinder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-28 11:31</last-date>
// -----------------------------------------------------------------------

using System.Reflection;


namespace OSharp.Core.Reflection
{
    /// <summary>
    /// 定义程序集查找器
    /// </summary>
    public interface IAssemblyFinder : IFinder<Assembly>
    { }
}