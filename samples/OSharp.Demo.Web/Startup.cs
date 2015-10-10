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
using OSharp.Core;
using OSharp.Core.Caching;
using OSharp.Core.Dependency;
using OSharp.Demo.Web;
using OSharp.Logging.Log4Net;
using OSharp.Web.Http.Initialize;
using OSharp.Web.Mvc.Initialize;

using Owin;

[assembly: OwinStartup(typeof(Startup))]


namespace OSharp.Demo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888

            ICacheProvider provider = new RuntimeMemoryCacheProvider();
            CacheManager.SetProvider(provider, CacheLevel.First);

            IServicesBuilder builder = new ServicesBuilder(new ServiceBuildOptions());
            IServiceCollection services = builder.Build();
            services.AddLog4NetServices();
            services.AddDataServices();

            app.UseMvcInitialize(services, new MvcAutofacIocBuilder());
            app.UseWebApiInitialize(services, new WebApiAutofacIocBuilder());

            ConfigurationWebApi(app);
            ConfigureSignalR(app);
        }
    }
}