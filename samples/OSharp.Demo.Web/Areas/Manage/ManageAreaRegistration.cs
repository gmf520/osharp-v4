using System.Web.Mvc;

using OSharp.Web.Mvc.Routing;


namespace OSharp.Demo.Web.Areas.Manage
{
    public class ManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Manage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapLowerCaseUrlRoute(
                "Manage_default",
                "Manage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "OSharp.Demo.Web.Areas.Manage.Controllers" }
            );
        }
    }
}