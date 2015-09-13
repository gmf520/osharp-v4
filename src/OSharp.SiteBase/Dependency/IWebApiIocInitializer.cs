// -----------------------------------------------------------------------
//  <copyright file="IWebApiIocInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-02 22:32</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Initialize;


namespace OSharp.SiteBase.Dependency
{
    /// <summary>
    /// 定义WebApi依赖注入初始化器
    /// </summary>
    public interface IWebApiIocInitializer : IIocInitializer
    { }
}