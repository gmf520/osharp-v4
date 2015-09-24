using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Controllers;


namespace OSharp.Web.Http.ModelBinders
{
    public class MvcStyleBindingAttribute : Attribute, IControllerConfiguration
    {
        public void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
        {
            controllerSettings.Services.Replace(typeof(IActionValueBinder), new MvcActionValueBinder());
        }
    }
}
