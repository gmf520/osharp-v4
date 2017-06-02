// -----------------------------------------------------------------------
//  <copyright file="FunctionAuthenticationBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-03 20:05</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;

using OSharp.Core.Data;
using OSharp.Core.Extensions;
using OSharp.Utility;
using OSharp.Utility.Extensions;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 功能权限检查基类
    /// </summary>
    /// <typeparam name="TFunction">功能类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    public abstract class FunctionAuthorizationBase<TFunction, TFunctionKey> : IFunctionAuthorization<TFunction>
        where TFunction : class, IFunction, IEntity<TFunctionKey>
        where TFunctionKey : IEquatable<TFunctionKey>
    {
        /// <summary>
        /// 获取或设置 功能权限信息缓存
        /// </summary>
        public IFunctionAuthCache<TFunctionKey> FunctionAuthCache { get; set; }

        /// <summary>
        /// 验证当前用户是否有执行指定功能的权限
        /// </summary>
        /// <param name="function">要检查的功能信息</param>
        /// <returns></returns>
        public AuthorizationResult Authenticate(TFunction function)
        {
            ClaimsPrincipal user = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (user == null)
            {
                return new AuthorizationResult(AuthorizationResultType.Error, "当前用户信息格式不正确，仅支持ClaimsPrincipal类型的用户信息");
            }
            return AuthenticateCore(function, user);
        }

        /// <summary>
        /// 重写以实现权限检查核心验证操作
        /// </summary>
        /// <param name="user">当前在线用户信息</param>
        /// <param name="function">要验证的功能信息</param>
        /// <returns></returns>
        protected virtual AuthorizationResult AuthenticateCore(TFunction function, ClaimsPrincipal user)
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
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return new AuthorizationResult(AuthorizationResultType.LoggedOut);
            }
            //登录限制，无角色限制
            if (function.FunctionType == FunctionType.Logined)
            {
                return AuthorizationResult.Allowed;
            }
            //角色限制
            ClaimsIdentity identity = user.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return new AuthorizationResult(AuthorizationResultType.Error, "当前用户标识IIdentity格式不正确，仅支持ClaimsIdentity类型的用户标识");
            }
            //检查角色-功能的权限
            string[] userRoleNames = identity.GetClaimValues(ClaimTypes.Role);
            string[] functionRoleNames = FunctionAuthCache.GetFunctionRoles(function.Id).ToArray();
            if (userRoleNames.Intersect(functionRoleNames).Any())
            {
                return new AuthorizationResult(AuthorizationResultType.Allowed);
            }
            //检查用户-功能的权限
            TFunctionKey[] functionIds = FunctionAuthCache.GetUserFunctions(user.Identity.Name).ToArray();
            if (functionIds.Contains(function.Id))
            {
                return new AuthorizationResult(AuthorizationResultType.Allowed);
            }
            return new AuthorizationResult(AuthorizationResultType.PurviewLack);
        }
    }
}