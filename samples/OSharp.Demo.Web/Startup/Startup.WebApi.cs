using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;

using Newtonsoft.Json;

using OSharp.Demo.Web.Areas.Service;
using OSharp.Web.Http.Caching;
using OSharp.Web.Http.Filters;
using OSharp.Web.Http.Handlers;
using OSharp.Web.Http.Routing;
using OSharp.Web.Http.Selectors;

using Owin;


namespace OSharp.Demo.Web
{
    public partial class Startup
    {
        public void ConfigurationWebApi(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration
            {
                DependencyResolver = GlobalConfiguration.Configuration.DependencyResolver
            };

            FieldInfo suffix = typeof(DefaultHttpControllerSelector).GetField("ControllerSuffix", BindingFlags.Static | BindingFlags.Public);
            if (suffix != null)
            {
                suffix.SetValue(null, "ApiController");
            }

            config.Services.Replace(typeof(IHttpControllerSelector), new AreaHttpControllerSelector(config));

            #region 如果为控制台，请取消注释此段代码，在/Areas/Service/ServiceAreaRegistration.cs 中注释的代码
            //config.Routes.MapHttpRoute(
            //  "ServiceActionApi",
            //  "api/service/{controller}/{action}/{id}",
            //  new { area = "Service", id = RouteParameter.Optional }
            //  );
            //config.Routes.MapHttpRoute(
            //    "ServiceApi",
            //    "api/service/{controller}/{id}",
            //    new { area = "Service", id = RouteParameter.Optional }
            //    );
            #endregion

            config.Routes.MapHttpRoute(
                "ActionApi",
                "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional }/*,new { action = new StartsWithConstraint()}*/);
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }/*,new { action = new StartsWithConstraint() }*/
            );

            config.Filters.Add(new ExceptionHandlingAttribute());

            var formatter = config.Formatters.JsonFormatter;
            formatter.SerializerSettings.PreserveReferencesHandling =
                PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.MessageHandlers.Add(new ThrottlingHandler(new InMemoryThrottleStore(), id => 60, TimeSpan.FromMinutes(1)));

            app.UseWebApi(config);
        }
    }
}