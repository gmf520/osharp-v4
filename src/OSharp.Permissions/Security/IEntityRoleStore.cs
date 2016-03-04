// -----------------------------------------------------------------------
//  <copyright file="IEntityRoleStore.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-07 2:34</last-date>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using OSharp.Core.Dependency;
using OSharp.Core.Security.Dtos;
using OSharp.Utility.Data;
using OSharp.Utility.Filter;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 定义数据角色映射存储
    /// </summary>
    public interface IEntityRoleStore<in TEntityRoleMapDto, in TKey, in TEntityInfoKey, in TRoleKey> : IScopeDependency
        where TEntityRoleMapDto : EntityRoleMapBaseInputDto<TKey, TEntityInfoKey, TRoleKey>
    {
        /// <summary>
        /// 增加数据角色映射信息
        /// </summary>
        /// <param name="dto">数据角色映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> CreateEntityRoleMapAsync(TEntityRoleMapDto dto);

        /// <summary>
        /// 编辑数据角色映射信息
        /// </summary>
        /// <param name="dto">数据角色映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateEntityRoleMapAsync(TEntityRoleMapDto dto);

        /// <summary>
        /// 删除数据角色映射信息
        /// </summary>
        /// <param name="id">数据角色映射编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteEntityRoleMapAsync(TKey id);

        /// <summary>
        /// 查找指定数据与角色的查询条件组
        /// </summary>
        /// <param name="entityInfoId">数据实体编号</param>
        /// <param name="roleId">角色编号</param>
        /// <returns>过滤条件组</returns>
        Task<FilterGroup> GetRoleFilterGroup(TEntityInfoKey entityInfoId, TRoleKey roleId);
    }
}