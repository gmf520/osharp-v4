// -----------------------------------------------------------------------
//  <copyright file="IModuleStore.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-13 0:59</last-date>
// -----------------------------------------------------------------------

using System;
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
    public interface IModuleStore<TModule, in TModuleKey, in TModuleInputDto, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey>
        where TModule : IModule<TModuleKey, TModule, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey>, IEntity<TModuleKey>
        where TModuleInputDto : ModuleBaseInputDto<TModuleKey>
        where TFunction : IFunction, IEntity<TFunctionKey>
        where TRole : IRole<TRoleKey>, IEntity<TRoleKey>
        where TUser : IUser<TUserKey>, IEntity<TUserKey>
        where TModuleKey : struct
    {
        /// <summary>
        /// 检查模块信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的模块信息编号</param>
        /// <returns>模块信息是否存在</returns>
        Task<bool> CheckTModuleExists(Expression<Func<TModule, bool>> predicate, TModuleKey id = default(TModuleKey));

        /// <summary>
        /// 添加模块信息信息
        /// </summary>
        /// <param name="dto">要添加的模块信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> CreateTModule(TModuleInputDto dto);

        /// <summary>
        /// 更新模块信息信息
        /// </summary>
        /// <param name="dto">包含更新信息的模块信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateTModule(TModuleInputDto dto);

        /// <summary>
        /// 删除模块信息信息
        /// </summary>
        /// <param name="id">要删除的模块信息编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteTModule(TModuleKey id);
    }
}