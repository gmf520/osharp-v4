// -----------------------------------------------------------------------
//  <copyright file="AjaxOnlyAttribute.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-22 16:34</last-date>
// -----------------------------------------------------------------------

using System;
using System.Web.Mvc;

using OSharp.Web.Mvc.Properties;


namespace OSharp.Web.Mvc.Security
{
    /// <summary>
    /// 限制当前功能只允许以Ajax的方式来访问
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
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