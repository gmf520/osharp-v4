// -----------------------------------------------------------------------
//  <copyright file="RoleLimitAuthentication.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-03 20:23</last-date>
// -----------------------------------------------------------------------

using System;
using System.Security.Claims;

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;
using OSharp.Core.Identity.Models;
using OSharp.Core.Security.Models;
using OSharp.Utility.Extensions;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 特定角色类型的功能权限检查
    /// </summary>
    /// <typeparam name="TFunction">功能类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    /// <typeparam name="TFunctionRoleMap">功能角色映射类型</typeparam>
    /// <typeparam name="TFunctionRoleMapKey">功能角色映射编号类型</typeparam>
    /// <typeparam name="TFunctionUserMap">功能用户映射类型</typeparam>
    /// <typeparam name="TFunctionUserMapKey">功能用户编号类型</typeparam>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    public class RoleLimitAuthentication<TFunction, TFunctionKey, TFunctionRoleMap, TFunctionRoleMapKey, TFunctionUserMap, TFunctionUserMapKey, TRole,
        TRoleKey, TUser, TUserKey> : IRoleLimitAuthentication<TFunction, TFunctionKey>
        where TFunction : FunctionBase<TFunctionKey>
        where TFunctionRoleMap : IFunctionRoleMap<TFunctionRoleMapKey, TFunction, TFunctionKey, TRole, TRoleKey>
        where TFunctionUserMap : IFunctionUserMap<TFunctionUserMapKey, TFunction, TFunctionKey, TUser, TUserKey>
        where TRole : RoleBase<TRoleKey>
        where TUser : UserBase<TUserKey>
        where TFunctionKey : IEquatable<TFunctionKey>
        where TFunctionRoleMapKey : IEquatable<TFunctionRoleMapKey>
        where TFunctionUserMapKey : IEquatable<TFunctionUserMapKey>
        where TRoleKey : IEquatable<TRoleKey>
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// 执行功能权限验证
        /// </summary>
        /// <param name="user">在线用户信息</param>
        /// <param name="function">功能信息</param>
        /// <returns>权限验证结果</returns>
        public AuthenticationResult Authenticate(ClaimsPrincipal user, TFunction function)
        {
            if (function.FunctionType != FunctionType.RoleLimit)
            {
                return new AuthenticationResult(AuthenticationResultType.Error, "功能“{0}”不是角色限制类型".FormatWith(function.Name));
            }
            if (!user.Identity.IsAuthenticated)
            {
                return new AuthenticationResult(AuthenticationResultType.LoggedOut, "当前用户未登录或登录已失效");
            }
            throw new NotImplementedException("特定角色的功能权限验证逻辑尚未实现");
        }
    }
}