// -----------------------------------------------------------------------
//  <copyright file="ISecurityRole.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-13 1:14</last-date>
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
    /// 定义角色权限安全管理功能
    /// </summary>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    /// <typeparam name="TFunction">功能类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    /// <typeparam name="TModuleKey">模块编号类型</typeparam>
    public interface ISecurityRole<in TRole, TRoleKey, TFunction, TFunctionKey, in TModuleKey> : IScopeDependency
        where TRole : IRole<TRoleKey>, IEntity<TRoleKey>
        where TFunction : IFunction, IEntity<TFunctionKey>
        where TModuleKey : IEquatable<TModuleKey>
        where TRoleKey : IEquatable<TRoleKey>
        where TFunctionKey : IEquatable<TFunctionKey>
    {
        /// <summary>
        /// 获取指定角色的允许功能集合
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns>允许的功能集合</returns>
        IEnumerable<TFunction> GetRoleAllFunctions(TRole role);

        /// <summary>
        /// 给角色添加模块权限
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <param name="moduleIds">要添加的模块编号集合</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> SetRoleModules(TRole role, params TModuleKey[] moduleIds);
    }
}