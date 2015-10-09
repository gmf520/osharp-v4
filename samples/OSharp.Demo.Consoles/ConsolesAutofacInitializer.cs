// -----------------------------------------------------------------------
//  <copyright file="ConsolesAutofacInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-08 19:38</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using OSharp.App.Local.Initialize;
using OSharp.Core.Dependency;


namespace OSharp.Demo.Consoles
{
    public class ConsolesAutofacInitializer : LocalAutofacIocInitializer
    {
        /// <summary>
        /// 添加自定义服务映射
        /// </summary>
        /// <param name="services">服务信息集合</param>
        protected override void AddCustomTypes(IServiceCollection services)
        {
            base.AddCustomTypes(services);
            services.AddSingleton<Program, Program>();
        }
    }
}