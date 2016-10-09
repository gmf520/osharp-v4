// -----------------------------------------------------------------------
//  <copyright file="ISecurityUser.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-13 1:18</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;
using OSharp.Core.Dependency;
using OSharp.Utility.Data;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 定义用户权限安全管理功能
    /// </summary>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    /// <typeparam name="TFunction">功能类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    /// <typeparam name="TModuleKey">模块编号类型</typeparam>
    public interface ISecurityUser<in TUser, TUserKey, TFunction, TFunctionKey, in TModuleKey> : IScopeDependency
        where TUser : IUser<TUserKey>, IEntity<TUserKey>
        where TFunction : IFunction, IEntity<TFunctionKey>
        where TModuleKey : struct
    {
        /// <summary>
        /// 获取赋予给用户的功能集合，不包含用户拥有的角色赋予的功能集合
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>允许的功能集合</returns>
        IEnumerable<TFunction> GetUserAllFunctions(TUser user);

        /// <summary>
        /// 给用户添加模块权限
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="moduleIds">要添加的模块编号集合</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> SetUserModules(TUser user, params TModuleKey[] moduleIds);
    }
}