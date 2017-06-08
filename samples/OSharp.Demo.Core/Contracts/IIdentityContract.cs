// -----------------------------------------------------------------------
//  <copyright file="IIdentityContract.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-24 16:13</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using OSharp.Core.Dependency;
using OSharp.Demo.Dtos.Identity;
using OSharp.Demo.Models.Identity;
using OSharp.Utility.Data;


namespace OSharp.Demo.Contracts
{
    /// <summary>
    /// 业务契约——身份认证模块
    /// </summary>
    public interface IIdentityContract : IScopeDependency
    {
        #region 组织机构信息业务

        /// <summary>
        /// 获取 组织机构信息查询数据集
        /// </summary>
        IQueryable<Organization> Organizations { get; }

        /// <summary>
        /// 检查组织机构信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的组织机构信息编号</param>
        /// <returns>组织机构信息是否存在</returns>
        bool CheckOrganizationExists(Expression<Func<Organization, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加组织机构信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的组织机构信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddOrganizations(params OrganizationInputDto[] inputDtos);

        /// <summary>
        /// 更新组织机构信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的组织机构信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditOrganizations(params OrganizationInputDto[] inputDtos);

        /// <summary>
        /// 删除组织机构信息信息
        /// </summary>
        /// <param name="ids">要删除的组织机构信息编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteOrganizations(params int[] ids);

        #endregion

        #region 角色信息业务

        /// <summary>
        /// 获取 角色信息查询数据集
        /// </summary>
        IQueryable<Role> Roles { get; }

        /// <summary>
        /// 检查角色信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的角色信息编号</param>
        /// <returns>角色信息是否存在</returns>
        bool CheckRoleExists(Expression<Func<Role, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加角色信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的角色信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddRoles(params RoleInputDto[] inputDtos);

        /// <summary>
        /// 更新角色信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的角色信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditRoles(params RoleInputDto[] inputDtos);

        /// <summary>
        /// 删除角色信息信息
        /// </summary>
        /// <param name="ids">要删除的角色信息编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteRoles(params int[] ids);

        #endregion

        #region 用户信息业务

        /// <summary>
        /// 获取 用户信息查询数据集
        /// </summary>
        IQueryable<User> Users { get; }

        /// <summary>
        /// 检查用户信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的用户信息编号</param>
        /// <returns>用户信息是否存在</returns>
        bool CheckUserExists(Expression<Func<User, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加用户信息信息
        /// </summary>
        /// <param name="dtos">要添加的用户信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> AddUsers(params UserInputDto[] dtos);

        /// <summary>
        /// 更新用户信息信息
        /// </summary>
        /// <param name="dtos">包含更新信息的用户信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> EditUsers(params UserInputDto[] dtos);

        /// <summary>
        /// 删除用户信息信息
        /// </summary>
        /// <param name="ids">要删除的用户信息编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteUsers(params int[] ids);

        /// <summary>
        /// 设置用户的角色
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <param name="roleIds">角色编号集合</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> SetUserRoles(int id, int[] roleIds);

        #endregion
    }
}