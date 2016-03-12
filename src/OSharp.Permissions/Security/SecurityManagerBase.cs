// -----------------------------------------------------------------------
//  <copyright file="SecurityManagerBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-13 1:28</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;
using OSharp.Core.Security.Models;
using OSharp.Utility.Data;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 权限安全管理基类
    /// </summary>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    /// <typeparam name="TFunction">功能类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    /// <typeparam name="TModule">模块类型</typeparam>
    /// <typeparam name="TModuleKey">模块编号类型</typeparam>
    public abstract class SecurityManagerBase<TRole, TRoleKey, TUser, TUserKey, TFunction, TFunctionKey, TModule, TModuleKey>
        : ISecurityManager<TRole, TRoleKey, TUser, TUserKey, TFunction, TFunctionKey, TModuleKey>
        where TRole : IRole<TRoleKey>, IEntity<TRoleKey>
        where TUser : IUser<TUserKey>, IEntity<TUserKey>
        where TFunction : IFunction, IEntity<TFunctionKey>
        where TModule : IModule<TModuleKey, TModule, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey>
        where TModuleKey : struct, IEquatable<TModuleKey>
        where TRoleKey : IEquatable<TRoleKey>
        where TUserKey : IEquatable<TUserKey>
        where TFunctionKey : IEquatable<TFunctionKey>
    {
        #region Implementation of ISecurityRole<in TRole,TRoleKey,TFunction,TFunctionKey,in TModuleKey>

        /// <summary>
        /// 获取指定角色的允许功能集合
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns>允许的功能集合</returns>
        public virtual Task<IEnumerable<TFunction>> GetRoleAllowedFunctions(TRole role)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 给角色添加模块权限
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <param name="moduleIds">要添加的模块编号集合</param>
        /// <returns>业务操作结果</returns>
        public virtual Task<OperationResult> SetRoleModules(TRole role, params TModuleKey[] moduleIds)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of ISecurityUser<in TUser,TUserKey,TFunction,TFunctionKey,in TModuleKey>

        /// <summary>
        /// 获取指定用户的允许功能集合
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>允许的功能集合</returns>
        public virtual Task<IEnumerable<TFunction>> GetUserAllowedFunctions(TUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 给用户添加模块权限
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="moduleIds">要添加的模块编号集合</param>
        /// <returns>业务操作结果</returns>
        public virtual Task<OperationResult> SetUserModules(TUser user, params TModuleKey[] moduleIds)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 给用户添加特殊功能限制
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="functions">要添加的功能编号及控制类型集合</param>
        /// <returns></returns>
        public virtual Task<OperationResult> SetUserFunctions(TUser user, params Tuple<TFunctionKey, FilterType>[] functions)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}