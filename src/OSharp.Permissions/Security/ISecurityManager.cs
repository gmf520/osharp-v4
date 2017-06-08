// -----------------------------------------------------------------------
//  <copyright file="ISecurityManager.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-13 1:23</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 定义安全权限功能
    /// </summary>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    /// <typeparam name="TFunction">功能类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    /// <typeparam name="TModuleKey">模块编号类型</typeparam>
    public interface ISecurityManager<in TRole, TRoleKey, in TUser, TUserKey, TFunction, TFunctionKey, in TModuleKey>
        : ISecurityRole<TRole, TRoleKey, TFunction, TFunctionKey, TModuleKey>,
          ISecurityUser<TUser, TUserKey, TFunction, TFunctionKey, TModuleKey>
        where TRole : IRole<TRoleKey>, IEntity<TRoleKey>
        where TUser : IUser<TUserKey>, IEntity<TUserKey>
        where TFunction : IFunction, IEntity<TFunctionKey>
        where TModuleKey : struct
    { }
}