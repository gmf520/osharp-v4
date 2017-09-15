// -----------------------------------------------------------------------
//  <copyright file="FunctionAuthCacheBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2017 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2017-06-02 16:17</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;

using Microsoft.AspNet.Identity;

using OSharp.Core.Caching;
using OSharp.Core.Data;
using OSharp.Core.Dependency;
using OSharp.Core.Security.Models;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 功能权限配置缓存基类
    /// </summary>
    /// <typeparam name="TFunction">功能类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    /// <typeparam name="TModule">模块类型</typeparam>
    /// <typeparam name="TModuleKey">模块编号类型</typeparam>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    public abstract class FunctionAuthCacheBase<TFunction, TFunctionKey, TModule, TModuleKey, TRole, TRoleKey, TUser, TUserKey>
        : IFunctionAuthCache<TFunctionKey>
        where TFunction : class, IFunction, IEntity<TFunctionKey>
        where TModule : class, IModule<TModuleKey, TModule, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey>
        where TRole : EntityBase<TRoleKey>, IRole<TRoleKey>
        where TUser : EntityBase<TUserKey>, IUser<TUserKey>
        where TFunctionKey : IEquatable<TFunctionKey>
        where TModuleKey : IEquatable<TModuleKey>
        where TRoleKey : IEquatable<TRoleKey>
        where TUserKey : IEquatable<TUserKey>
    {
        private readonly ICache _cache;

        /// <summary>
        /// 初始化一个<see cref="FunctionAuthCacheBase"/>类型的新实例
        /// </summary>
        protected FunctionAuthCacheBase()
        {
            _cache = CacheManager.GetCacher(GetType());
        }

        /// <summary>
        /// 获取或设置 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 创建功能权限缓存
        /// </summary>
        public virtual void BuildCaches()
        {
            _cache.Clear();
            //只重建 功能-角色集合 的映射，用户-功能 的映射，遇到才即时创建并缓存
            IRepository<TFunction, TFunctionKey> functionRepository = ServiceProvider.GetService<IRepository<TFunction, TFunctionKey>>();
            TFunction[] functions = functionRepository.Entities.Where(m => !m.IsLocked).ToArray();
            foreach (TFunction function in functions)
            {
                GetFunctionRoles(function.Id);
            }
        }

        /// <summary>
        /// 移除指定功能的缓存
        /// </summary>
        /// <param name="functionIds">功能编号集合</param>
        public virtual void RemoveFunctionCaches(TFunctionKey[] functionIds)
        {
            foreach (TFunctionKey functionId in functionIds)
            {
                string key = $"FunctionRoles_{functionId}";
                _cache.Remove(key);
            }
        }

        /// <summary>
        /// 移除指定用户的缓存
        /// </summary>
        /// <param name="userNames">用户编号集合</param>
        public virtual void RemoveUserCaches(string[] userNames)
        {
            foreach (string userName in userNames)
            {
                string key = $"UserFunctions_{userName}";
                _cache.Remove(key);
            }
        }

        /// <summary>
        /// 获取能执行指定功能的所有角色
        /// </summary>
        /// <param name="functionId">功能编号</param>
        /// <returns>能执行功能的角色名称集合</returns>
        public virtual string[] GetFunctionRoles(TFunctionKey functionId)
        {
            string key = $"FunctionRoles_{functionId}";
            string[] roleNames = _cache.Get<string[]>(key);
            if (roleNames == null)
            {
                IRepository<TModule, TModuleKey> moduleRepository = ServiceProvider.GetService<IRepository<TModule, TModuleKey>>();
                roleNames = moduleRepository.Entities.Where(m => m.Functions.Any(n => n.Id.Equals(functionId)))
                    .SelectMany(m => m.Roles.Select(n => n.Name)).Distinct().ToArray();
                _cache.Set(key, roleNames);
            }
            return roleNames;
        }

        /// <summary>
        /// 获取指定用户的所有特权功能
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户的所有特权功能</returns>
        public virtual TFunctionKey[] GetUserFunctions(string userName)
        {
            string key = $"UserFunctions_{userName}";
            TFunctionKey[] functionIds = _cache.Get<TFunctionKey[]>(key);
            if (functionIds == null)
            {
                IRepository<TModule, TModuleKey> moduleRepository = ServiceProvider.GetService<IRepository<TModule, TModuleKey>>();
                functionIds = moduleRepository.Entities.Where(m => m.Users.Any(n => n.UserName == userName))
                    .SelectMany(m => m.Functions.Select(n => n.Id)).Distinct().ToArray();
                _cache.Set(key, functionIds);
            }
            return functionIds;
        }
    }
}