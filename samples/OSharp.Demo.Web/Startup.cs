// -----------------------------------------------------------------------
//  <copyright file="Startup.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-29 23:12</last-date>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using Microsoft.Owin;

using OSharp.Autofac.Http;
using OSharp.Autofac.Mvc;
using OSharp.Autofac.SignalR;
using OSharp.Core.Initialize;
using OSharp.Demo.Web;
using OSharp.SiteBase.Initialize;
using OSharp.Web.Http.Initialize;
using OSharp.Web.Mvc.Initialize;
using OSharp.Web.SignalR.Initialize;

using Owin;

[assembly: OwinStartup(typeof(Startup))]


namespace OSharp.Demo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888

            IBasicLoggingInitializer loggingInitializer = new Log4NetLoggingInitializer();
            app.UseMvcInitialize(new MvcInitializeOptions(loggingInitializer, new MvcAutofacIocInitializer()));
            app.UseWebApiInitialize(new WebApiInitializeOptions(loggingInitializer, new WebApiAutofacIocInitializer()));

            ConfigurationWebApi(app);
            ConfigureSignalR(app);
        }
    }
}