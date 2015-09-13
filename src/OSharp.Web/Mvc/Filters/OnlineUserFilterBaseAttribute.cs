// -----------------------------------------------------------------------
//  <copyright file="OnlineUserFilterAttribute.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-15 13:09</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using OSharp.Web.Security;


namespace OSharp.Web.Mvc.Filters
{
    /// <summary>
    /// 网站在线用户过滤器基类，用于把Iprincipal.Identity转换为在线用户信息，或更新用户最后活动相关信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class OnlineUserFilterBaseAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 获取 在线用户存储实例，重写时最好能保持<see cref="OnlineUserStoreBase"/>的单例
        /// </summary>
        public abstract OnlineUserStoreBase OnlineUserStore { get; }

        /// <summary>
        /// 获取 忽略的活动地址
        /// </summary>
        public virtual string[] IgnoreActivityUrls
        {
            get { return new string[] { }; }
        }

        /// <summary>
        /// 创建<see cref="OnlineUser"/>实例
        /// </summary>
        public abstract OnlineUser CreateOnlineUser(string name, HttpContextBase context);

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContextBase context = filterContext.HttpContext;
            if (context == null || !context.User.Identity.IsAuthenticated)
            {
                return;
            }
            OnlineUser user = OnlineUserStore.GetCurrentSiteUser(context.User);
            if (user == null)
            {
                string name = context.User.Identity.Name;
                try
                {
                    user = CreateOnlineUser(name, context);
                }
                catch (UnauthorizedAccessException e)
                {
                    filterContext.Result = new HttpUnauthorizedResult(e.Message);
                    return;
                }
            }
            string url = context.Request.Path;
            if (!context.Request.IsAjaxRequest() && !IgnoreActivityUrls.Contains(url))
            {
                user.LastActivityUrl = url;
            }
            user.IpAddress = context.Request.UserHostAddress;
            user.LastActivityTime = DateTime.Now;
            OnlineUserStore.Set(user);
        }
    }
}