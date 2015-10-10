// -----------------------------------------------------------------------
//  <copyright file="SignalRFrameworkInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-09 18:18</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Dependency;
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

        /// <summary>
        /// 获取 依赖注入解析器
        /// </summary>
        protected override IIocResolver IocResolver
        {
            get { return new SignalRIocResolver(); }
        }
    }
}