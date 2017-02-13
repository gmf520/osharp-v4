// -----------------------------------------------------------------------
//  <copyright file="HandleAjaxErrorAttribute.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-29 14:00</last-date>
// -----------------------------------------------------------------------

using System;
using System.Web.Mvc;

using OSharp.Web.Mvc.UI;


namespace OSharp.Web.Mvc.Filters
{
    /// <summary>
    /// 表示一个特性，该特性用于处理Ajax操作方法引发的异常，此特性应在<see cref="System.Web.Mvc.HandleErrorAttribute"/>之前调用。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class HandleAjaxErrorAttribute : FilterAttribute, IExceptionFilter
    {
        #region Implementation of IExceptionFilter

        /// <summary>
        /// 在发生异常时调用。
        /// </summary>
        /// <param name="filterContext">筛选器上下文。</param>
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult()
                {
                    Data = new AjaxResult("Ajax操作引发异常：" + filterContext.Exception.Message, AjaxResultType.Error),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                filterContext.ExceptionHandled = true;
            }
        }

        #endregion
    }
}