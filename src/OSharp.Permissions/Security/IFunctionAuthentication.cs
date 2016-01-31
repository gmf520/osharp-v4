// -----------------------------------------------------------------------
//  <copyright file="IFunctionAuthentication.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-03 13:55</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Dependency;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 定义功能权限验证器
    /// </summary>
    /// <typeparam name="TFunction">功能类型</typeparam>
    public interface IFunctionAuthentication<in TFunction> : ISingletonDependency
    {
        /// <summary>
        /// 验证当前用户是否有执行指定功能的权限
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        AuthenticationResult Authenticate(TFunction function);
    }
}