// -----------------------------------------------------------------------
//  <copyright file="FunctionAuthenticationBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-03 20:05</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;

using OSharp.Core.Data;
using OSharp.Core.Extensions;
using OSharp.Utility.Extensions;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 功能权限检查基类
    /// </summary>
    /// <typeparam name="TFunction">功能类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    public abstract class FunctionAuthorizationBase<TFunction, TFunctionKey> : IFunctionAuthorization
        where TFunction : class, IFunction, IEntity<TFunctionKey>
        where TFunctionKey : IEquatable<TFunctionKey>
    {
        /// <summary>
        /// 初始化一个<see cref="FunctionAuthorizationBase{TFunction, TFunctionKey}"/>类型的新实例
        /// </summary>
        protected FunctionAuthorizationBase()
        {
            SuperRoleName = "系统管理员";
        }

        /// <summary>
        /// 获取 超级管理员角色
        /// </summary>
        protected virtual string SuperRoleName { get; private set; }
        
        /// <summary>
        /// 获取或设置 功能权限信息缓存
        /// </summary>
        public IFunctionAuthCache<TFunctionKey> FunctionAuthCache { get; set; }

        /// <summary>
        /// 验证当前用户是否有执行指定功能的权限
        /// </summary>
        /// <param name="function">要检查的功能信息</param>
        /// <returns></returns>
        public AuthorizationResult Authorize(IFunction function)
        {
            IPrincipal principal = Thread.CurrentPrincipal;
            return Authorize(function, principal);
        }

        /// <summary>
        /// 验证用户是否有执行指定功能的权限
        /// </summary>
        /// <param name="function">要验证的功能</param>
        /// <param name="principal">在线用户信息</param>
        /// <returns>功能权限验证结果</returns>
        public AuthorizationResult Authorize(IFunction function, IPrincipal principal)
        {
            return AuthorizeCore(function, principal);
        }

        /// <summary>
        /// 重写以实现权限检查核心验证操作
        /// </summary>
        /// <param name="function">要验证的功能信息</param>
        /// <param name="principal">当前用户在线信息</param>
        /// <returns></returns>
        protected virtual AuthorizationResult AuthorizeCore(IFunction function, IPrincipal principal)
        {
            if (function == null)
            {
                return new AuthorizationResult(AuthorizationResultType.FunctionNotFound);
            }
            //功能禁用
            if (function.IsLocked)
            {
                return new AuthorizationResult(AuthorizationResultType.FunctionLocked, "功能“{0}”已被禁用，无法执行".FormatWith(function.Name));
            }
            //匿名访问
            if (function.FunctionType == FunctionType.Anonymouse)
            {
                return AuthorizationResult.Allowed;
            }
            //登录无效
            if (principal == null || !principal.Identity.IsAuthenticated)
            {
                return new AuthorizationResult(AuthorizationResultType.LoggedOut);
            }
            //登录限制，无角色限制
            if (function.FunctionType == FunctionType.Logined)
            {
                return AuthorizationResult.Allowed;
            }
            return AuthorizeRoleLimit(function, principal);
        }

        /// <summary>
        /// 重写以实现 角色限制 的功能的功能权限验证
        /// </summary>
        /// <param name="function">要验证的功能信息</param>
        /// <param name="user">用户在线信息</param>
        /// <returns>功能权限验证结果</returns>
        protected virtual AuthorizationResult AuthorizeRoleLimit(IFunction function, IPrincipal user)
        {
            ClaimsPrincipal claimsUser = user as ClaimsPrincipal;
            if (claimsUser == null)
            {
                return new AuthorizationResult(AuthorizationResultType.Error, "当前用户信息格式不正确，仅支持ClaimsPrincipal类型的用户信息");
            }
            //角色限制
            ClaimsIdentity identity = claimsUser.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return new AuthorizationResult(AuthorizationResultType.Error, "当前用户标识IIdentity格式不正确，仅支持ClaimsIdentity类型的用户标识");
            }
            TFunction function1 = function as TFunction;
            if (function1 == null)
            {
                return new AuthorizationResult(AuthorizationResultType.Error,
                    "要检测的功能类型为“{0}”，不是要求的“{1}”类型".FormatWith(function.GetType()),
                    typeof(TFunction));
            }

            //检查角色-功能的权限
            string[] userRoleNames = identity.GetClaimValues(ClaimTypes.Role);

            //如果是超级管理员角色
            if (userRoleNames.Contains(SuperRoleName))
            {
                return AuthorizationResult.Allowed;
            }

            string[] functionRoleNames = FunctionAuthCache.GetFunctionRoles(function1.Id);
            if (userRoleNames.Intersect(functionRoleNames).Any())
            {
                return new AuthorizationResult(AuthorizationResultType.Allowed);
            }
            //检查用户-功能的权限
            TFunctionKey[] functionIds = FunctionAuthCache.GetUserFunctions(user.Identity.Name);
            if (functionIds.Contains(function1.Id))
            {
                return AuthorizationResult.Allowed;
            }
            return new AuthorizationResult(AuthorizationResultType.PurviewLack);
        }
    }
}