// -----------------------------------------------------------------------
//  <copyright file="IModuleStore.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-13 0:59</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;
using OSharp.Core.Security.Dtos;
using OSharp.Core.Security.Models;
using OSharp.Utility.Data;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 定义模块存储的功能
    /// </summary>
    /// <typeparam name="TModule">模块类型</typeparam>
    /// <typeparam name="TModuleKey">模块编号类型</typeparam>
    /// <typeparam name="TModuleInputDto">模块输入类型</typeparam>
    /// <typeparam name="TFunction">功能类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    public interface IModuleStore<TModule, in TModuleKey, in TModuleInputDto, out TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey>
        where TModule : IModule<TModuleKey, TModule, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey>, IEntity<TModuleKey>
        where TModuleInputDto : ModuleBaseInputDto<TModuleKey>
        where TFunction : IFunction, IEntity<TFunctionKey>
        where TRole : IRole<TRoleKey>, IEntity<TRoleKey>
        where TUser : IUser<TUserKey>, IEntity<TUserKey>
        where TModuleKey : IEquatable<TModuleKey>
        where TFunctionKey : IEquatable<TFunctionKey>
        where TRoleKey : IEquatable<TRoleKey>
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// 获取 模块信息查询数据集
        /// </summary>
        IQueryable<TModule> Modules { get; }

        /// <summary>
        /// 检查模块信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的模块信息编号</param>
        /// <returns>模块信息是否存在</returns>
        Task<bool> CheckModuleExists(Expression<Func<TModule, bool>> predicate, TModuleKey id = default(TModuleKey));

        /// <summary>
        /// 添加模块信息信息
        /// </summary>
        /// <param name="dto">要添加的模块信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> CreateModule(TModuleInputDto dto);

        /// <summary>
        /// 更新模块信息信息
        /// </summary>
        /// <param name="dto">包含更新信息的模块信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateModule(TModuleInputDto dto);

        /// <summary>
        /// 删除模块信息信息
        /// </summary>
        /// <param name="id">要删除的模块信息编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteModule(TModuleKey id);

        /// <summary>
        /// 获取指定模块及其父模块的所有可用功能集合
        /// </summary>
        /// <param name="id">要查询的顶模块信息</param>
        /// <returns>允许的功能集合</returns>
        IEnumerable<TFunction> GetAllFunctions(TModuleKey id);

        /// <summary>
        /// 设置模块拥有的功能
        /// </summary>
        /// <param name="id">模块编号</param>
        /// <param name="functionIds">功能编号集合</param>
        /// <returns></returns>
        Task<OperationResult> SetModuleFunctions(TModuleKey id, TFunctionKey[] functionIds);
    }
}