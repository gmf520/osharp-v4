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
using OSharp.AutoMapper;
using OSharp.Core.Caching;
using OSharp.Core.Dependency;
using OSharp.Core.Security;
using OSharp.Data.Entity;
using Byone.Core.Identity;
using Byone.Core.Services;
using Byone.Site;
using OSharp.Logging.Log4Net;
using OSharp.Web.Http.Initialize;
using OSharp.Web.Mvc.Initialize;
using OSharp.Web.SignalR.Initialize;

using Owin;

[assembly: OwinStartup(typeof(Startup))]


namespace Byone.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ICacheProvider provider = new RuntimeMemoryCacheProvider();
            CacheManager.SetProvider(provider, CacheLevel.First);

            IServicesBuilder builder = new ServicesBuilder();
            IServiceCollection services = builder.Build();
            services.AddLog4NetServices();
            services.AddDataServices();
            services.AddAutoMapperServices();
            services.AddOAuthServices();
            services.AddByoneServices(app);

            app.UseOsharpMvc(new MvcAutofacIocBuilder(services));
            IIocBuilder apiAutofacIocBuilder = new WebApiAutofacIocBuilder(services);
            app.UseOsharpWebApi(apiAutofacIocBuilder);
            //app.UseOsharpSignalR(new SignalRAutofacIocBuilder(services));

            app.ConfigureOAuth(apiAutofacIocBuilder.ServiceProvider);
            app.ConfigureWebApi();
            //app.ConfigureSignalR();
        }
    }
}