using System.Web.Http;
using System.Web.Mvc;

namespace OSharp.Demo.Web.Areas.Service
{
    public class ServiceAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Service";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Routes.MapHttpRoute(
                "ServiceActionApi",
                "api/service/{controller}/{action}/{id}",
                new {area="Service",id=RouteParameter.Optional}
            );
            config.Routes.MapHttpRoute(
                "ServiceApi",
                "api/service/{controller}/{id}",
                new { area = "Service", id = RouteParameter.Optional }
            );
             
            context.MapRoute(
                "Service_default",
                "api/service/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}