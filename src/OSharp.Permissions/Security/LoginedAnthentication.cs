// -----------------------------------------------------------------------
//  <copyright file="LoginedAnthentication.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-03 20:08</last-date>
// -----------------------------------------------------------------------

using System;
using System.Security.Claims;

using OSharp.Utility.Extensions;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 登录验证类型的功能权限检查
    /// </summary>
    /// <typeparam name="TFunction">功能类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    public class LoginedAnthentication<TFunction, TFunctionKey> : ILoginedAnthentication<TFunction, TFunctionKey>
        where TFunction : FunctionBase<TFunctionKey>
        where TFunctionKey : IEquatable<TFunctionKey>
    {
        /// <summary>
        /// 执行功能权限验证
        /// </summary>
        /// <param name="user">在线用户信息</param>
        /// <param name="function">功能信息</param>
        /// <returns>权限验证结果</returns>
        public virtual AuthenticationResult Authenticate(ClaimsPrincipal user, TFunction function)
        {
            if (function.FunctionType != FunctionType.Logined)
            {
                return new AuthenticationResult(AuthenticationResultType.Error, "功能“{0}”不是登录验证类型".FormatWith(function.Name));
            }
            return user.Identity.IsAuthenticated
                ? AuthenticationResult.Allowed
                : new AuthenticationResult(AuthenticationResultType.LoggedOut, "当前用户未登录或登录已失效");
        }
    }
}