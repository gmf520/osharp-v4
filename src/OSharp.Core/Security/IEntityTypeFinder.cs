﻿// -----------------------------------------------------------------------
//  <copyright file="IEntityTypeFinder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-12 21:29</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Dependency;
using OSharp.Core.Reflection;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 定义实体类型查找
    /// </summary>
    public interface IEntityTypeFinder : ITypeFinder, ISingletonDependency
    { }
}