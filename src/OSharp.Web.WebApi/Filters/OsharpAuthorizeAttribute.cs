using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using OSharp.Core.Security;
using OSharp.Web.Http.Extensions;


namespace OSharp.Web.Http.Filters
{
    /// <summary>
    /// 功能权限授权验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class OsharpAuthorizeAttribute : AuthorizationFilterAttribute
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
        /// 在过程请求授权时调用。
        /// </summary>
        /// <param name="actionContext">操作上下文，它封装有关使用 <see cref="T:System.Web.Http.Filters.AuthorizationFilterAttribute"/> 的信息。</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext == null)
                throw new ArgumentNullException("actionContext");
            if (SkipAuthorization(actionContext))
                return;
            AuthorizationResult result = AuthorizeCore(actionContext);
            if (result.ResultType == AuthorizationResultType.Allowed)
            {
                return;
            }
            HandleUnauthorizedRequest(actionContext, result);
        }

        /// <summary>
        /// 重写以实现功能权限验证的逻辑
        /// </summary>
        /// <param name="actionContext">功能上下文</param>
        /// <returns>权限验证结果</returns>
        protected virtual AuthorizationResult AuthorizeCore(HttpActionContext actionContext)
        {
            IFunction function = actionContext.Request.GetExecuteFunction(ServiceProvider);
            IPrincipal principal = actionContext.ControllerContext.RequestContext.Principal;
            AuthorizationResult result = FunctionAuthorization.Authorize(function, principal);
            return result;
        }

        /// <summary>
        /// 处理授权失败的请求。
        /// </summary>
        /// <param name="actionContext">上下文。</param>
        /// <param name="result">功能权限验证结果</param>
        protected virtual void HandleUnauthorizedRequest(HttpActionContext actionContext, AuthorizationResult result)
        {
            if (actionContext == null)
                throw new ArgumentNullException("actionContext");
            AuthorizationResultType type = result.ResultType;
            string msg = StringToISO_8859_1(result.Message);
            switch (type)
            {
                case AuthorizationResultType.LoggedOut:
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, msg);
                    break;
                case AuthorizationResultType.PurviewLack:
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, msg);
                    break;
                case AuthorizationResultType.FunctionLocked:
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Gone, msg);
                    break;
                case AuthorizationResultType.FunctionNotFound:
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.NotFound, msg);
                    break;
                case AuthorizationResultType.Error:
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, msg);
                    break;
            }
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }

        /// <summary>
        /// 转换为ISO_8859_1
        /// </summary>
        /// <param name="srcText"></param>
        /// <returns></returns>
        private string StringToISO_8859_1(string srcText)
        {
            char[] src = srcText.ToCharArray();
            return src.Select(t => @"&#" + (int)t + ";").Aggregate("", (current, str) => current + str);
        }
    }
}
