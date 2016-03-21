// -----------------------------------------------------------------------
//  <copyright file="BaseController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-25 14:49</last-date>
// -----------------------------------------------------------------------

using System;
using System.Web.Mvc;

using OSharp.Utility.Logging;
using OSharp.Web.Mvc.Logging;
using OSharp.Web.Mvc.UI;


namespace OSharp.Web.Mvc
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    [OperateLogFilter]
    public abstract class BaseController : Controller
    {
        protected readonly ILogger Logger;

        protected BaseController()
        {
            Logger = LogManager.GetLogger(GetType());
        }

        /// <summary>
        /// 获取或设置 依赖注入服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// Called when an unhandled exception occurs in the action.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            Exception exception = filterContext.Exception;
            Logger.Error(exception.Message, exception);
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var message = "Ajax请求异常：";
                if (exception is HttpAntiForgeryException)
                {
                    message += "安全性验证失败。<br>请刷新页面重试，详情请查看系统日志。";
                }
                else
                {
                    message += exception.Message;
                }
                filterContext.Result = Json(new AjaxResult(message, AjaxResultType.Error));
                filterContext.ExceptionHandled = true;
            }
        }
    }
}