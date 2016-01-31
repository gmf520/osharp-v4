// -----------------------------------------------------------------------
//  <copyright file="ITypeFinder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-28 11:30</last-date>
// -----------------------------------------------------------------------

using System;


namespace OSharp.Core.Reflection
{
    /// <summary>
    /// 定义类型查找行为
    /// </summary>
    public interface ITypeFinder : IFinder<Type>
    { }
}