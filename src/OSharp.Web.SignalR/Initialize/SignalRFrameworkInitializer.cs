// -----------------------------------------------------------------------
//  <copyright file="SignalRFrameworkInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-29 20:29</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Initialize;


namespace OSharp.Web.SignalR.Initialize
{
    /// <summary>
    /// SignalR框架初始化
    /// </summary>
    public class SignalRFrameworkInitializer : FrameworkInitializerBase
    {
        /// <summary>
        /// 初始化一个<see cref="SignalRFrameworkInitializer"/>类型的新实例
        /// </summary>
        public SignalRFrameworkInitializer(SignalRInitializeOptions options)
            : base(options)
        { }
    }
}