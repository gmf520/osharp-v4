// -----------------------------------------------------------------------
//  <copyright file="IRoleLimitAuthentication.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-03 19:53</last-date>
// -----------------------------------------------------------------------

using System.Security.Claims;

using OSharp.Core.Dependency;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 定义特定角色类型的功能权限检查
    /// </summary>
    /// <typeparam name="TFunction">功能类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    public interface IRoleLimitAuthentication<in TFunction, TFunctionKey> : ISingletonDependency
        where TFunction : FunctionBase<TFunctionKey>
    {
        /// <summary>
        /// 执行功能权限验证
        /// </summary>
        /// <param name="user">在线用户信息</param>
        /// <param name="function">功能信息</param>
        /// <returns>权限验证结果</returns>
        AuthenticationResult Authenticate(ClaimsPrincipal user, TFunction function);
    }
}