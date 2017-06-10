// -----------------------------------------------------------------------
//  <copyright file="IFunctionAuthentication.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2017 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2017-06-10 13:45</last-date>
// -----------------------------------------------------------------------

using System.Security.Principal;

using OSharp.Core.Dependency;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 定义功能权限验证器
    /// </summary>
    public interface IFunctionAuthorization : ISingletonDependency
    {
        /// <summary>
        /// 验证当前用户是否有执行指定功能的权限
        /// </summary>
        /// <param name="function">要验证的功能</param>
        /// <returns>功能权限验证结果</returns>
        AuthorizationResult Authorize(IFunction function);

        /// <summary>
        /// 验证用户是否有执行指定功能的权限
        /// </summary>
        /// <param name="function">要验证的功能</param>
        /// <param name="principal">在线用户信息</param>
        /// <returns>功能权限验证结果</returns>
        AuthorizationResult Authorize(IFunction function, IPrincipal principal);
    }
}