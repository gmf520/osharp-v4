// -----------------------------------------------------------------------
//  <copyright file="FunctionAuthenticationBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-03 20:05</last-date>
// -----------------------------------------------------------------------

using System;
using System.Security.Claims;
using System.Threading;

using OSharp.Utility;
using OSharp.Utility.Extensions;


namespace OSharp.Core.Security
{
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