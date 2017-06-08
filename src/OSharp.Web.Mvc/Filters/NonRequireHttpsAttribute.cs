// -----------------------------------------------------------------------
//  <copyright file="NonRequireHttpsAttribute.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-11-13 21:39</last-date>
// -----------------------------------------------------------------------

using System;
using System.Web;
using System.Web.Mvc;


namespace OSharp.Web.Mvc.Filters
{
    /// <summary>
    /// 将套字连接HTTPS转换为普通HTTP连接
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class NonRequireHttpsAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// Called when authorization is required.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (!filterContext.HttpContext.Request.IsSecureConnection)
            {
                return;
            }
            HandleHttpsRequest(filterContext);
        }

        private void HandleHttpsRequest(AuthorizationContext filterContext)
        {
            if (!string.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("HttpMethod必须为GET");
            }
            HttpRequestBase request = filterContext.HttpContext.Request;
            if (request.Url != null)
            {
                filterContext.Result = new RedirectResult("http://" + request.Url.Host + request.RawUrl);
            }
        }
    }
}