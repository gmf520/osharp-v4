// -----------------------------------------------------------------------
//  <copyright file="AppBuilderExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-29 23:00</last-date>
// -----------------------------------------------------------------------

using System.Web.Mvc;

using OSharp.Core;
using OSharp.Core.Dependency;
using OSharp.Utility;
using OSharp.Web.Mvc.Binders;

using Owin;


namespace OSharp.Web.Mvc.Initialize
{
    /// <summary>
    /// <see cref="IAppBuilder"/>初始化扩展
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// 初始化Mvc框架
        /// </summary>
        public static IAppBuilder UseOsharpMvc(this IAppBuilder app, IIocBuilder iocBuilder)
        {
            iocBuilder.CheckNotNull("iocBuilder");

            ModelBinders.Binders.Add(typeof(string), new StringTrimModelBinder());

            IFrameworkInitializer initializer = new FrameworkInitializer();
            initializer.Initialize(iocBuilder);
            return app;
        }
    }
}