using System.Web.Mvc;

namespace OSharp.Demo.Web.Areas.BootStrap
{
    public class BootStrapAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BootStrap";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BootStrap_default",
                "BootStrap/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}