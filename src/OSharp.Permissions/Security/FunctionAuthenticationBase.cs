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
    public abstract class FunctionAuthenticationBase<TFunction, TFunctionKey> : IFunctionAuthentication<TFunction>
        where TFunction : class, IFunction, IEntity<TFunctionKey>
        where TFunctionKey : IEquatable<TFunctionKey>
    {
        /// <summary>
        /// 验证当前用户是否有执行指定功能的权限
        /// </summary>
        /// <param name="function">要检查的功能信息</param>
        /// <returns></returns>
        public AuthenticationResult Authenticate(TFunction function)
        {
            ClaimsPrincipal user = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (user == null)
            {
                return new AuthenticationResult(AuthenticationResultType.Error, "当前用户信息格式不正确，仅支持ClaimsPrincipal类型的用户信息");
            }
            return AuthenticateCore(function, user);
        }

        /// <summary>
        /// 重写以实现权限检查核心验证操作
        /// </summary>
        /// <param name="user">当前在线用户信息</param>
        /// <param name="function">要验证的功能信息</param>
        /// <returns></returns>
        protected virtual AuthenticationResult AuthenticateCore(TFunction function, ClaimsPrincipal user)
        {
            if (function == null)
            {
                return new AuthenticationResult(AuthenticationResultType.FunctionNotFound);
            }
            //功能禁用
            if (function.IsLocked)
            {
                return new AuthenticationResult(AuthenticationResultType.FunctionLocked, "功能“{0}”已被禁用，无法执行".FormatWith(function.Name));
            }
            //匿名访问
            if (function.FunctionType == FunctionType.Anonymouse)
            {
                return AuthenticationResult.Allowed;
            }
            //登录无效
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return new AuthenticationResult(AuthenticationResultType.LoggedOut);
            }
            //登录限制，无角色限制
            if (function.FunctionType == FunctionType.Logined)
            {
                return AuthenticationResult.Allowed;
            }
            //角色限制
            ClaimsIdentity identity = user.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return new AuthenticationResult(AuthenticationResultType.Error, "当前用户标识IIdentity格式不正确，仅支持ClaimsIdentity类型的用户标识");
            }
            string[] roleNames = identity.GetClaimValues(ClaimTypes.Role);



            throw new NotImplementedException();
        }
    }








    ///// <summary>
    ///// 功能权限检查基类
    ///// </summary>
    ///// <typeparam name="TFunction">功能类型</typeparam>
    ///// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    //public abstract class FunctionAuthenticationBase<TFunction, TFunctionKey>
    //    : IFunctionAuthentication<TFunction>
    //    where TFunction : FunctionBase<TFunctionKey>
    //    where TFunctionKey : IEquatable<TFunctionKey>
    //{
    ///// <summary>
    ///// 获取或设置 登录限制功能权限检查
    ///// </summary>
    //public ILoginedAnthentication<TFunction, TFunctionKey> LoginedAnthentication { get; set; }

    ///// <summary>
    ///// 获取或设置 角色限制功能权限检查
    ///// </summary>
    //public IRoleLimitAuthentication<TFunction, TFunctionKey> RoleLimitAuthentication { get; set; }

    ///// <summary>
    ///// 验证当前用户是否有执行指定功能的权限
    ///// </summary>
    ///// <param name="function">要检查的功能信息</param>
    ///// <returns>验证操作结果</returns>
    //public AuthenticationResult Authenticate(TFunction function)
    //{
    //    function.CheckNotNull("function");
    //    ClaimsPrincipal user = Thread.CurrentPrincipal as ClaimsPrincipal;
    //    if (user == null)
    //    {
    //        return new AuthenticationResult(AuthenticationResultType.PurviewLack, "当前用户信息格式不正确，仅支持ClaimsPrincipal类型的用户信息");
    //    }
    //    return AuthenticateCore(user, function);
    //}

    ///// <summary>
    ///// 重写以实现权限检查核心操作
    ///// </summary>
    ///// <returns></returns>
    //private AuthenticationResult AuthenticateCore(ClaimsPrincipal user, TFunction function)
    //{
    //    user.CheckNotNull("user");
    //    function.CheckNotNull("function");
    //    if (function.IsLocked)
    //    {
    //        return new AuthenticationResult(AuthenticationResultType.FunctionLocked, "功能“{0}”已被冻结，无法执行".FormatWith(function.Name));
    //    }
    //    if (function.FunctionType == FunctionType.Anonymouse)
    //    {
    //        return AuthenticationResult.Allowed;
    //    }
    //if (function.FunctionType == FunctionType.Logined)
    //{
    //    return LoginedAnthentication.Authenticate(user, function);
    //}
    //return RoleLimitAuthentication.Authenticate(user, function);
    //    }
    //}
}