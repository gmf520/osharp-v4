// -----------------------------------------------------------------------
//  <copyright file="UserRoleMapStoreBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-21 23:02</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OSharp.Core.Data;
using OSharp.Core.Data.Extensions;
using OSharp.Core.Identity.Dtos;
using OSharp.Core.Identity.Models;
using OSharp.Core.Mapping;
using OSharp.Utility;
using OSharp.Utility.Data;


namespace OSharp.Core.Identity
{
    /// <summary>
    /// 用户角色映射存储基类
    /// </summary>
    public abstract class UserRoleMapStoreBase<TUserRoleMap, TUserRoleMapKey, TUserRoleMapInputDto, TUser, TUserKey, TRole, TRoleKey>
        : IUserRoleMapStore<TUserRoleMapInputDto, TUserRoleMapKey, TUserKey, TRoleKey>
        where TUserRoleMap : UserRoleMapBase<TUserRoleMapKey, TUser, TUserKey, TRole, TRoleKey>
        where TUserRoleMapInputDto : UserRoleMapBaseInputDto<TUserRoleMapKey, TUserKey, TRoleKey>
        where TRole : RoleBase<TRoleKey>
        where TUser : UserBase<TUserKey>
        where TUserRoleMapKey : IEquatable<TUserRoleMapKey>
        where TUserKey : IEquatable<TUserKey>
        where TRoleKey : IEquatable<TRoleKey>
    {
        /// <summary>
        /// 获取或设置 用户信息仓储对象
        /// </summary>
        public IRepository<TUser, TUserKey> UserRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 角色信息仓储对象
        /// </summary>
        public IRepository<TRole, TRoleKey> RoleRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 用户角色映射信息仓储对象
        /// </summary>
        public IRepository<TUserRoleMap, TUserRoleMapKey> UserRoleMapRepository { protected get; set; }

        /// <summary>
        /// 添加用户角色映射信息
        /// </summary>
        /// <param name="dto">用户角色映射信息输入DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> AddUserRoleMapAsync(TUserRoleMapInputDto dto)
        {
            dto.CheckNotNull("dto");
            bool exists = await UserRoleMapRepository.CheckExistsAsync(m => m.User.Id.Equals(dto.UserId) && m.Role.Id.Equals(dto.RoleId));
            if (exists)
            {
                return OperationResult.Success;
            }
            TUserRoleMap map = dto.MapTo<TUserRoleMap>();
            TUser user = await UserRepository.GetByKeyAsync(dto.UserId);
            if (user == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的用户信息不存在");
            }
            map.User = user;
            TRole role = await RoleRepository.GetByKeyAsync(dto.RoleId);
            if (role == null)
            {
                return new OperationResult(OperationResultType.Success, "指定编号的角色信息不存在");
            }
            map.Role = role;
            await UserRoleMapRepository.InsertAsync(map);
            return OperationResult.Success;
        }

        /// <summary>
        /// 编辑用户角色映射信息
        /// </summary>
        /// <param name="dto">用户角色映射信息输入DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> EditUserRoleMapAsync(TUserRoleMapInputDto dto)
        {
            dto.CheckNotNull("dto");
            var data = (await UserRoleMapRepository.GetByPredicateAsync(m => m.Id.Equals(dto.Id))).Select(m => new
            {
                Map = m,
                UserId = m.User.Id,
                RoleId = m.Role.Id
            }).FirstOrDefault();
            //TUserRoleMap map = await UserRoleMapRepository.GetByKeyAsync(dto.Id);
            if (data == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的用户角色映射信息不存在");
            }
            TUserRoleMap map = dto.MapTo(data.Map);
            //map = dto.MapTo(map);
            if (!data.UserId.Equals(dto.UserId))
            //if (!map.User.Id.Equals(dto.UserId))
            {
                TUser user = await UserRepository.GetByKeyAsync(dto.UserId);
                if (user == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "指定编号的用户信息不存在");
                }
                map.User = user;
            }
            if (!data.RoleId.Equals(dto.RoleId))
            //if (!map.Role.Id.Equals(dto.RoleId))
            {
                TRole role = await RoleRepository.GetByKeyAsync(dto.RoleId);
                if (role == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "指定编号的角色信息不存在");
                }
                map.Role = role;
            }
            await UserRoleMapRepository.UpdateAsync(map);
            return OperationResult.Success;
        }

        /// <summary>
        /// 删除用户角色映射信息
        /// </summary>
        /// <param name="id">用户角色映射编号</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> DeleteUserRoleMapAsync(TUserRoleMapKey id)
        {
            TUserRoleMap map = await UserRoleMapRepository.GetByKeyAsync(id);
            if (map == null)
            {
                return OperationResult.Success;
            }
            await UserRoleMapRepository.DeleteAsync(map);
            return OperationResult.Success;
        }

        /// <summary>
        /// 获取指定用户的有效角色编号集合
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>有效的角色编号集合</returns>
        public virtual Task<IList<TRoleKey>> GetRoleIdsAsync(TUserKey userId)
        {
            IList<TRoleKey> ids = UserRoleMapRepository.Entities.Where(m => m.User.Id.Equals(userId))
                .Unlocked().Unexpired().Select(m => m.Role.Id).ToList();
            return Task.FromResult(ids);
        }

        /// <summary>
        /// 获取指定用户的有效角色名称集合
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>有效的角色名称集合</returns>
        public virtual Task<IList<string>> GetRolesAsync(TUserKey userId)
        {
            IList<string> names = UserRoleMapRepository.Entities.Where(m => m.User.Id.Equals(userId))
                .Unlocked().Unexpired().Select(m => m.Role.Name).ToList();
            return Task.FromResult(names);
        }

        /// <summary>
        /// 返回用户是有拥有指定角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="roleId">要判断的角色名称</param>
        /// <returns>是否拥有</returns>
        public virtual Task<bool> IsInRoleAsync(TUserKey userId, TRoleKey roleId)
        {
            bool exist = UserRoleMapRepository.Entities.Where(m => m.User.Id.Equals(userId) && m.Role.Id.Equals(roleId))
                .Unlocked().Unexpired().Any();
            return Task.FromResult(exist);
        }

        /// <summary>
        /// 返回用户是有拥有指定角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="roleName">要判断的角色名称</param>
        /// <returns>是否拥有</returns>
        public virtual Task<bool> IsInRoleAsync(TUserKey userId, string roleName)
        {
            bool exist = UserRoleMapRepository.Entities.Where(m => m.User.Id.Equals(userId) && m.Role.Name.Equals(roleName))
                .Unlocked().Unexpired().Any();
            return Task.FromResult(exist);
        }
    }
}