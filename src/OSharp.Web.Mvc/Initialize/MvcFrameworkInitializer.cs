// -----------------------------------------------------------------------
//  <copyright file="MvcFrameworkInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-29 20:09</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Initialize;


namespace OSharp.Web.Mvc.Initialize
{
    /// <summary>
    /// MVC框架初始化器
    /// </summary>
    public class MvcFrameworkInitializer : FrameworkInitializerBase
    {
        /// <summary>
        /// 初始化一个<see cref="MvcFrameworkInitializer"/>类型的新实例
        /// </summary>
        public MvcFrameworkInitializer(MvcInitializeOptions options)
            : base(options)
        { }
    }
}