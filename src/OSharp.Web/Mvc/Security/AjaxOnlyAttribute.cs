using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using OSharp.Web.Properties;


namespace OSharp.Web.Mvc.Security
{
    /// <summary>
    /// 限制当前功能只允许以Ajax的方式来访问
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AjaxOnlyAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Called before an action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new ContentResult
                {
                    Content = Resources.Mvc_ActionAttribute_AjaxOnlyMessage
                };
            }
        }
    }
}
