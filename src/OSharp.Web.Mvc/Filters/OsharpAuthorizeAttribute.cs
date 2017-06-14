// -----------------------------------------------------------------------
//  <copyright file="OsharpAuthorizeAttribute.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2017 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2017-06-02 23:02</last-date>
// -----------------------------------------------------------------------

using System;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

using OSharp.Core.Security;
using OSharp.Web.Mvc.Extensions;


namespace OSharp.Web.Mvc.Filters
{
    /// <summary>
    /// 功能权限授权验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class OsharpAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly object _typeId = new object();

        /// <summary>
        /// 获取或设置 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 获取或设置 功能权限验证执行类
        /// </summary>
        public IFunctionAuthorization FunctionAuthorization { get; set; }

        /// <summary> 
        /// When implemented in a derived class, gets a unique identifier for this <see cref="T:System.Attribute"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Object"/> that is a unique identifier for the attribute.
        /// </returns>
        public override object TypeId
        {
            get { return _typeId; }
        }

        /// <summary>
        /// 在需要授权时调用。
        /// </summary>
        /// <param name="filterContext">筛选器上下文。</param>
        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (OutputCacheAttribute.IsChildActionCacheActive(filterContext))
            {
                throw new InvalidOperationException("AuthorizeAttribute_CannotUseWithinChildActionCache");
            }
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }
            IFunction function = filterContext.GetExecuteFunction(ServiceProvider);
            AuthorizationResult result = AuthorizeCore(filterContext.HttpContext, function);
            if (result.ResultType != AuthorizationResultType.Allowed)
            {
                HandleUnauthorizedRequest(filterContext, result);
            }
            else
            {
                HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
                cache.SetProxyMaxAge(new TimeSpan(0L));
                cache.AddValidationCallback(CacheValidateHandler, function);
            }
        }

        /// <summary>
        /// 重写以实现功能权限的核心验证逻辑的实现
        /// </summary>
        /// <param name="httpContext">Http请求上下文</param>
        /// <param name="function">要验证的功能</param>
        /// <returns></returns>
        protected virtual AuthorizationResult AuthorizeCore(HttpContextBase httpContext, IFunction function)
        {
            IPrincipal user = httpContext.User;
            AuthorizationResult result = FunctionAuthorization.Authorize(function, user);
            return result;
        }

        /// <summary>
        /// Processes HTTP requests that fail authorization.
        /// </summary>
        /// <param name="filterContext">Encapsulates the information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute"/>. The <paramref name="filterContext"/> object contains the controller, HTTP context, request context, action result, and route data.</param>
        /// <param name="result">权限验证结果</param>
        protected virtual void HandleUnauthorizedRequest(AuthorizationContext filterContext, AuthorizationResult result)
        {
            AuthorizationResultType type = result.ResultType;
            switch (type)
            {
                case AuthorizationResultType.LoggedOut:
                    filterContext.Result = new HttpUnauthorizedResult();
                    break;
                case AuthorizationResultType.PurviewLack:
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                    break;
                case AuthorizationResultType.FunctionLocked:
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Gone, "Function is Locked");
                    break;
                case AuthorizationResultType.FunctionNotFound:
                    filterContext.Result = new HttpNotFoundResult();
                    break;
                case AuthorizationResultType.Error:
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                    break;
            }
        }

        /// <summary>
        /// Called when the caching module requests authorization.
        /// </summary>
        /// <returns>
        /// A reference to the validation status.
        /// </returns>
        /// <param name="httpContext">The HTTP context, which encapsulates all HTTP-specific information about an individual HTTP request.</param>
        /// <param name="function">要验证的功能</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="httpContext"/> parameter is null.</exception>
        protected virtual HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext, IFunction function)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            return AuthorizeCore(httpContext, function) != AuthorizationResult.Allowed ? HttpValidationStatus.IgnoreThisRequest : HttpValidationStatus.Valid;
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            IFunction function = data as IFunction;
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context), function);
        }
    }
}