// -----------------------------------------------------------------------
//  <copyright file="EntityMapStoreBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-07 3:20</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data;
using OSharp.Core.Data.Entity;
using OSharp.Core.Identity.Models;
using OSharp.Core.Security.Dtos;
using OSharp.Core.Security.Models;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;
using OSharp.Utility.Filter;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 数据（角色、用户）映射存储基类
    /// </summary>
    public abstract class EntityMapStoreBase<TEntityRoleMap, TEntityRoleMapKey, TEntityUserMap, TEntityUserMapKey,
        TEntityRoleMapDto, TEntityUserMapDto, TEntityInfo, TEntityInfoKey, TRole, TRoleKey, TUser, TUserKey>
        : IEntityRoleStore<TEntityRoleMapDto, TEntityRoleMapKey, TEntityInfoKey, TRoleKey>,
          IEntityUserStore<TEntityUserMapDto, TEntityUserMapKey, TEntityInfoKey, TUserKey>
        where TEntityRoleMap : IEntityRoleMap<TEntityRoleMapKey, TEntityInfo, TEntityInfoKey, TRole, TRoleKey>
        where TEntityUserMap : IEntityUserMap<TEntityUserMapKey, TEntityInfo, TEntityInfoKey, TUser, TUserKey>
        where TEntityRoleMapDto : EntityRoleMapBaseDto<TEntityRoleMapKey, TEntityInfoKey, TRoleKey>
        where TEntityUserMapDto : EntityUserMapBaseDto<TEntityUserMapKey, TEntityInfoKey, TUserKey>
        where TEntityInfo : EntityInfoBase<TEntityInfoKey>
        where TRole : RoleBase<TRoleKey>
        where TUser : UserBase<TUserKey>
    {
        /// <summary>
        /// 获取或设置 数据实体仓储对象
        /// </summary>
        public IRepository<TEntityInfo, TEntityInfoKey> EntityInfoRepository { get; set; }

        /// <summary>
        /// 获取或设置 角色仓储对象
        /// </summary>
        public IRepository<TRole, TRoleKey> RoleRepository { get; set; }

        /// <summary>
        /// 获取或设置 数据角色映射仓储对象
        /// </summary>
        public IRepository<TEntityRoleMap, TEntityRoleMapKey> EntityRoleMapRepository { get; set; }

        /// <summary>
        /// 获取或设置 用户仓储对象
        /// </summary>
        public IRepository<TUser, TUserKey> UserRepository { get; set; }

        /// <summary>
        /// 获取或设置 数据用户映射仓储对象
        /// </summary>
        public IRepository<TEntityUserMap, TEntityUserMapKey> EntityUserMapRepository { get; set; }

        #region Implementation of IEntityRoleStore<in TEntityRoleMapDto,in TEntityRoleMapKey,in TEntityInfoKey,in TRoleKey>

        /// <summary>
        /// 增加数据角色映射信息
        /// </summary>
        /// <param name="dto">数据角色映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> AddEntityRoleMapAsync(TEntityRoleMapDto dto)
        {
            dto.CheckNotNull("dto");
            if (await EntityRoleMapRepository.CheckExistsAsync(m => m.EntityInfo.Id.Equals(dto.EntityInfoId) && m.Role.Id.Equals(dto.RoleId)))
            {
                return new OperationResult(OperationResultType.Error, "指定数据与角色的数据角色映射信息已存在");
            }
            TEntityInfo entityInfo = await EntityInfoRepository.GetByKeyAsync(dto.EntityInfoId);
            if (entityInfo == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的数据实体信息不存在");
            }
            TRole role = await RoleRepository.GetByKeyAsync(dto.RoleId);
            if (role == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的角色信息不存在");
            }
            TEntityRoleMap map = dto.MapTo<TEntityRoleMap>();
            map.EntityInfo = entityInfo;
            map.Role = role;
            if (dto.FilterGroup != null)
            {
                map.FilterGroupJson = dto.FilterGroup.ToJsonString();
            }
            return await EntityRoleMapRepository.InsertAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "数据角色映射信息添加成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 编辑数据角色映射信息
        /// </summary>
        /// <param name="dto">数据角色映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> EditEntityRoleMapAsync(TEntityRoleMapDto dto)
        {
            dto.CheckNotNull("dto" );
            TEntityRoleMap map = await EntityRoleMapRepository.GetByKeyAsync(dto.Id);
            if (map == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的数据角色映射信息不存在");
            }
            map = dto.MapTo(map);
            if (!map.EntityInfo.Id.Equals(dto.EntityInfoId))
            {
                TEntityInfo entityInfo = await EntityInfoRepository.GetByKeyAsync(dto.EntityInfoId);
                if (entityInfo == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "指定编号的数据实体信息不存在");
                }
                map.EntityInfo = entityInfo;
            }
            if (!map.Role.Id.Equals(dto.RoleId))
            {
                TRole role = await RoleRepository.GetByKeyAsync(dto.RoleId);
                if (role == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "指定编号的角色信息不存在");
                }
                map.Role = role;
            }
            if (map.FilterGroupJson != dto.FilterGroup.ToJsonString())
            {
                map.FilterGroupJson = dto.FilterGroup.ToJsonString();
            }
            return await EntityRoleMapRepository.UpdateAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "数据角色映射信息编辑成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 删除数据角色映射信息
        /// </summary>
        /// <param name="id">数据角色映射编号</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> DeleteEntityRoleMapAsync(TEntityRoleMapKey id)
        {
            TEntityRoleMap map = await EntityRoleMapRepository.GetByKeyAsync(id);
            if (map == null)
            {
                return OperationResult.NoChanged;
            }
            return await EntityRoleMapRepository.DeleteAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "数据角色映射信息删除成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 查找指定数据与角色的查询条件组
        /// </summary>
        /// <param name="entityInfoId">数据实体编号</param>
        /// <param name="roleId">角色编号</param>
        /// <returns>过滤条件组</returns>
        public virtual Task<FilterGroup> GetRoleFilterGroup(TEntityInfoKey entityInfoId, TRoleKey roleId)
        {
            var result = EntityRoleMapRepository.Entities.Where(m => m.EntityInfo.Id.Equals(entityInfoId) && m.Role.Id.Equals(roleId))
                .Select(m => new { m.FilterGroup }).FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            return Task.FromResult(result.FilterGroup);
        }

        #endregion

        #region Implementation of IEntityUserStore<in TEntityUserMapDto,in TEntityUserMapKey,in TEntityInfoKey,in TUserKey>

        /// <summary>
        /// 增加数据用户映射信息
        /// </summary>
        /// <param name="dto">数据用户映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> AddEntityUserMapAsync(TEntityUserMapDto dto)
        {
            dto.CheckNotNull("dto");
            if (await EntityUserMapRepository.CheckExistsAsync(m => m.EntityInfo.Id.Equals(dto.EntityInfoId) && m.User.Id.Equals(dto.UserId)))
            {
                return new OperationResult(OperationResultType.Error, "指定数据与角色的数据角色映射信息已存在");
            }
            TEntityInfo entityInfo = await EntityInfoRepository.GetByKeyAsync(dto.EntityInfoId);
            if (entityInfo == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的数据实体信息不存在");
            }
            TUser user = await UserRepository.GetByKeyAsync(dto.UserId);
            if (user == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的角色信息不存在");
            }
            TEntityUserMap map = dto.MapTo<TEntityUserMap>();
            map.EntityInfo = entityInfo;
            map.User = user;
            if (dto.FilterGroup != null)
            {
                map.FilterGroupJson = dto.FilterGroup.ToJsonString();
            }
            return await EntityUserMapRepository.InsertAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "数据角色映射信息添加成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 编辑数据用户映射信息
        /// </summary>
        /// <param name="dto">数据用户映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> EditEntityUserMapAsync(TEntityUserMapDto dto)
        {
            dto.CheckNotNull("dto");
            TEntityUserMap map = await EntityUserMapRepository.GetByKeyAsync(dto.Id);
            if (map == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的数据角色映射信息不存在");
            }
            map = dto.MapTo(map);
            if (!map.EntityInfo.Id.Equals(dto.EntityInfoId))
            {
                TEntityInfo entityInfo = await EntityInfoRepository.GetByKeyAsync(dto.EntityInfoId);
                if (entityInfo == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "指定编号的数据实体信息不存在");
                }
                map.EntityInfo = entityInfo;
            }
            if (!map.User.Id.Equals(dto.UserId))
            {
                TUser user = await UserRepository.GetByKeyAsync(dto.UserId);
                if (user == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "指定编号的角色信息不存在");
                }
                map.User = user;
            }
            if (map.FilterGroupJson != dto.FilterGroup.ToJsonString())
            {
                map.FilterGroupJson = dto.FilterGroup.ToJsonString();
            }
            return await EntityUserMapRepository.UpdateAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "数据角色映射信息编辑成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 删除数据用户映射信息
        /// </summary>
        /// <param name="id">数据用户映射编号</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> DeleteEntityUserMapAsync(TEntityUserMapKey id)
        {
            TEntityUserMap map = await EntityUserMapRepository.GetByKeyAsync(id);
            if (map == null)
            {
                return OperationResult.NoChanged;
            }
            return await EntityUserMapRepository.DeleteAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "数据角色映射信息删除成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 查找指定数据与用户的查询条件组
        /// </summary>
        /// <param name="entityInfoId">数据实体编号</param>
        /// <param name="roleId">用户编号</param>
        /// <returns>过滤条件组</returns>
        public virtual Task<FilterGroup> GetUserFilterGroup(TEntityInfoKey entityInfoId, TUserKey roleId)
        {
            var result = EntityUserMapRepository.Entities.Where(m => m.EntityInfo.Id.Equals(entityInfoId) && m.User.Id.Equals(roleId))
                .Select(m => new { m.FilterGroup }).FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            return Task.FromResult(result.FilterGroup);
        }

        #endregion
    }
}