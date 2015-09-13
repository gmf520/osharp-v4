// -----------------------------------------------------------------------
//  <copyright file="OnlineUserFilterBaseAttribute.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-15 13:09</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using OSharp.Web.Http.Extensions;
using OSharp.Web.Security;


namespace OSharp.Web.Http.Filters
{
    /// <summary>
    /// API在线用户过滤器基类，用于把Iprincipal.Identity转换为在线用户信息，或更新用户最后活动相关信息
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
        public abstract OnlineUser CreateOnlineUser(string name, HttpRequestMessage request);

        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
#if NET45
            IPrincipal principal = actionContext.RequestContext.Principal;
#else
            IPrincipal principal = Thread.CurrentPrincipal;
#endif
            //用户未登录，不统计游客信息
            if ( principal == null || !principal.Identity.IsAuthenticated)
            {
                return;
            }
            OnlineUser user = OnlineUserStore.GetCurrentApiUser(principal);
            if (user == null)
            {
                string name = principal.Identity.Name;
                try
                {
                    user = CreateOnlineUser(name, actionContext.Request);
                }
                catch (UnauthorizedAccessException e)
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, e.Message);
                    return;
                }
            }
            string url = actionContext.Request.RequestUri.AbsolutePath;
            if (!IgnoreActivityUrls.Contains(url))
            {
                user.LastActivityUrl = url;
            }
            user.IpAddress = actionContext.Request.GetClientIpAddress();
            user.LastActivityTime = DateTime.Now;
            OnlineUserStore.Set(user);
        }
    }
}