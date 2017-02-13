// -----------------------------------------------------------------------
//  <copyright file="IUserRoleMapStore.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-21 18:31</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

using OSharp.Core.Dependency;
using OSharp.Core.Identity.Dtos;
using OSharp.Utility.Data;


namespace OSharp.Core.Identity
{
    /// <summary>
    /// 定义用户角色映射存储
    /// </summary>
    public interface IUserRoleMapStore<in TUserRoleMapInputDto, in TKey, in TUserKey, TRoleKey> : IScopeDependency
        where TUserRoleMapInputDto : UserRoleMapBaseInputDto<TKey, TUserKey, TRoleKey>
    {
        /// <summary>
        /// 添加用户角色映射信息
        /// </summary>
        /// <param name="dto">用户角色映射信息输入DTO</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> CreateUserRoleMapAsync(TUserRoleMapInputDto dto);

        /// <summary>
        /// 编辑用户角色映射信息
        /// </summary>
        /// <param name="dto">用户角色映射信息输入DTO</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateUserRoleMapAsync(TUserRoleMapInputDto dto);

        /// <summary>
        /// 删除用户角色映射信息
        /// </summary>
        /// <param name="id">用户角色映射编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteUserRoleMapAsync(TKey id);

        /// <summary>
        /// 获取指定用户的有效角色编号集合
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>有效的角色编号集合</returns>
        Task<IList<TRoleKey>> GetRoleIdsAsync(TUserKey userId);

        /// <summary>
        /// 获取指定用户的有效角色名称集合
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>有效的角色名称集合</returns>
        Task<IList<string>> GetRolesAsync(TUserKey userId);

        /// <summary>
        /// 返回用户是有拥有指定角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="roleId">要判断的角色名称</param>
        /// <returns>是否拥有</returns>
        Task<bool> IsInRoleAsync(TUserKey userId, TRoleKey roleId);
    }
}