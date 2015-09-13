// -----------------------------------------------------------------------
//  <copyright file="FunctionMapStoreBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-04 14:22</last-date>
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


namespace OSharp.Core.Security
{
    /// <summary>
    /// 功能（角色、用户）映射存储基类
    /// </summary>
    /// <typeparam name="TFunctionRoleMap">功能角色映射类型</typeparam>
    /// <typeparam name="TFunctionRoleMapKey">功能角色映射编号类型</typeparam>
    /// <typeparam name="TFunctionUserMap">功能用户映射类型</typeparam>
    /// <typeparam name="TFunctionUserMapKey">功能用户映射编号类型</typeparam>
    /// <typeparam name="TFunctionRoleMapDto">功能角色映射DTO类型</typeparam>
    /// <typeparam name="TFunctionUserMapDto">功能用户映射DTO类型</typeparam>
    /// <typeparam name="TFunction">功能类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    public abstract class FunctionMapStoreBase<TFunctionRoleMap, TFunctionRoleMapKey, TFunctionUserMap, TFunctionUserMapKey,
        TFunctionRoleMapDto, TFunctionUserMapDto, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey>
        : IFunctionRoleStore<TFunctionRoleMapDto, TFunctionRoleMapKey, TFunctionKey, TRoleKey>,
          IFunctionUserStore<TFunctionUserMapDto, TFunctionUserMapKey, TFunctionKey, TUserKey>
        where TFunctionRoleMap : IFunctionRoleMap<TFunctionRoleMapKey, TFunction, TFunctionKey, TRole, TRoleKey>
        where TFunctionUserMap : IFunctionUserMap<TFunctionUserMapKey, TFunction, TFunctionKey, TUser, TUserKey>
        where TFunctionRoleMapDto : FunctionRoleMapBaseDto<TFunctionRoleMapKey, TFunctionKey, TRoleKey>
        where TFunctionUserMapDto : FunctionUserMapBaseDto<TFunctionUserMapKey, TFunctionKey, TUserKey>
        where TFunction : FunctionBase<TFunctionKey>
        where TRole : RoleBase<TRoleKey>
        where TUser : UserBase<TUserKey>
    {
        /// <summary>
        /// 获取或设置 功能仓储对象
        /// </summary>
        public IRepository<TFunction, TFunctionKey> FunctionRepository { get; set; }

        /// <summary>
        /// 获取或设置 角色仓储对象
        /// </summary>
        public IRepository<TRole, TRoleKey> RoleRepository { get; set; }

        /// <summary>
        /// 获取或设置 功能角色映射仓储对象
        /// </summary>
        public IRepository<TFunctionRoleMap, TFunctionRoleMapKey> FunctionRoleMapRepository { get; set; }

        /// <summary>
        /// 获取或设置 用户仓储对象
        /// </summary>
        public IRepository<TUser, TUserKey> UserRepository { get; set; }

        /// <summary>
        /// 获取或设置 功能用户映射仓储对象
        /// </summary>
        public IRepository<TFunctionUserMap, TFunctionUserMapKey> FunctionUserMapRepository { get; set; }

        #region Implementation of IFunctionRoleStore<in TFunctionRoleMapDto,in TFunctionRoleMapKey,in TFunctionKey,TRoleKey>

        /// <summary>
        /// 增加功能角色映射信息
        /// </summary>
        /// <param name="dto">功能角色映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> AddFunctionRoleMapAsync(TFunctionRoleMapDto dto)
        {
            dto.CheckNotNull("dto");
            if (await FunctionRoleMapRepository.CheckExistsAsync(m => m.Function.Id.Equals(dto.FunctionId) && m.Role.Id.Equals(dto.RoleId)))
            {
                return new OperationResult(OperationResultType.Error, "指定功能与角色的功能角色映射信息已存在");
            }
            TFunction function = await FunctionRepository.GetByKeyAsync(dto.FunctionId);
            if (function == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的功能信息不存在");
            }
            TRole role = await RoleRepository.GetByKeyAsync(dto.RoleId);
            if (role == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的角色信息不存在");
            }
            TFunctionRoleMap map = dto.MapTo<TFunctionRoleMap>();
            map.Function = function;
            map.Role = role;
            return await FunctionRoleMapRepository.InsertAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "功能角色映射信息添加成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 编辑功能角色映射信息
        /// </summary>
        /// <param name="dto">功能角色映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> EditFunctionRoleMapAsync(TFunctionRoleMapDto dto)
        {
            dto.CheckNotNull("dto");
            TFunctionRoleMap map = await FunctionRoleMapRepository.GetByKeyAsync(dto.Id);
            if (map == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的功能角色映射信息不存在");
            }
            map = dto.MapTo(map);
            if (!map.Function.Id.Equals(dto.FunctionId))
            {
                TFunction function = await FunctionRepository.GetByKeyAsync(dto.FunctionId);
                if (function == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "指定编号的功能信息不存在");
                }
                map.Function = function;
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
            return await FunctionRoleMapRepository.UpdateAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "功能角色映射信息编辑成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 删除功能角色映射信息
        /// </summary>
        /// <param name="id">功能角色映射编号</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> DeleteFunctionRoleMapAsync(TFunctionRoleMapKey id)
        {
            TFunctionRoleMap map = await FunctionRoleMapRepository.GetByKeyAsync(id);
            if (map == null)
            {
                return OperationResult.NoChanged;
            }
            return await FunctionRoleMapRepository.DeleteAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "功能角色映射信息删除成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 获取功能的角色及其限制类型
        /// </summary>
        /// <param name="functionId">功能编号</param>
        /// <returns>角色及其限制类型的集合</returns>
        public virtual Task<IEnumerable<Tuple<string, FilterType>>> GetRolesAsync(TFunctionKey functionId)
        {
            var result = FunctionRoleMapRepository.Entities.Where(m => m.Function.Id.Equals(functionId))
                .Select(m => new Tuple<string, FilterType>(m.Role.Name, m.FilterType));
            return Task.FromResult(result.AsEnumerable());
        }

        /// <summary>
        /// 验证功能是否允许访问
        /// </summary>
        /// <param name="functionId">功能编号</param>
        /// <param name="roleNames">要验证的角色名</param>
        /// <returns>是否允许访问</returns>
        public virtual async Task<bool> IsRolesVisiteEnabledAsync(TFunctionKey functionId, params string[] roleNames)
        {
            Tuple<string, FilterType>[] tuples = (await GetRolesAsync(functionId)).ToArray();
            foreach (string roleName in roleNames)
            {
                if (tuples.Any(m => m.Item1 == roleName && m.Item2 == FilterType.Refused))
                {
                    return false;
                }
                if (tuples.Any(m => m.Item1 == roleName && m.Item2 == FilterType.Allowed))
                {
                    return true;
                }
            }
            return true;
        }

        #endregion

        #region Implementation of IFunctionUserStore<in TFunctionUserMapDto,in TFunctionUserMapKey,in TFunctionKey,TUserKey>

        /// <summary>
        /// 增加功能用户映射信息
        /// </summary>
        /// <param name="dto">功能用户映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> AddFunctionUserMapAsync(TFunctionUserMapDto dto)
        {
            dto.CheckNotNull("dto");
            if (await FunctionUserMapRepository.CheckExistsAsync(m => m.Function.Id.Equals(dto.FunctionId) && m.User.Id.Equals(dto.UserId)))
            {
                return new OperationResult(OperationResultType.Error, "指定功能与用户的功能用户映射信息已存在");
            }
            TFunction function = await FunctionRepository.GetByKeyAsync(dto.FunctionId);
            if (function == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的功能信息不存在");
            }
            TUser user = await UserRepository.GetByKeyAsync(dto.UserId);
            if (user == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的用户信息不存在");
            }
            TFunctionUserMap map = dto.MapTo<TFunctionUserMap>();
            map.Function = function;
            map.User = user;
            return await FunctionUserMapRepository.InsertAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "功能用户映射信息添加成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 编辑功能用户映射信息
        /// </summary>
        /// <param name="dto">功能用户映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> EditFunctionUserMapAsync(TFunctionUserMapDto dto)
        {
            dto.CheckNotNull("dto");
            TFunctionUserMap map = await FunctionUserMapRepository.GetByKeyAsync(dto.Id);
            if (map == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的功能用户映射信息不存在");
            }
            map = dto.MapTo(map);
            if (!map.Function.Id.Equals(dto.FunctionId))
            {
                TFunction function = await FunctionRepository.GetByKeyAsync(dto.FunctionId);
                if (function == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "指定编号的功能信息不存在");
                }
                map.Function = function;
            }
            if (!map.User.Id.Equals(dto.UserId))
            {
                TUser user = await UserRepository.GetByKeyAsync(dto.UserId);
                if (user == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "指定编号的用户信息不存在");
                }
                map.User = user;
            }
            return await FunctionUserMapRepository.UpdateAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "功能用户映射信息编辑成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 删除功能用户映射信息
        /// </summary>
        /// <param name="id">功能用户映射编号</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> DeleteFunctionUserMapAsync(TFunctionUserMapKey id)
        {
            TFunctionUserMap map = await FunctionUserMapRepository.GetByKeyAsync(id);
            if (map == null)
            {
                return OperationResult.NoChanged;
            }
            return await FunctionUserMapRepository.DeleteAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "功能用户映射信息删除成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 获取功能的用户及其限制类型
        /// </summary>
        /// <param name="functionId">功能编号</param>
        /// <returns>用户及其限制类型的集合</returns>
        public virtual Task<IEnumerable<Tuple<string, FilterType>>> GetUsersAsync(TFunctionKey functionId)
        {
            var result = FunctionUserMapRepository.Entities.Where(m => m.Function.Id.Equals(functionId))
                .Select(m => new Tuple<string, FilterType>(m.User.UserName, m.FilterType));
            return Task.FromResult(result.AsEnumerable());
        }

        /// <summary>
        /// 验证功能是否允许访问
        /// </summary>
        /// <param name="functionId">功能编号</param>
        /// <param name="userNames">要验证的用户名</param>
        /// <returns>是否允许访问</returns>
        public virtual async Task<bool> IsUsersVisiteEnabledAsync(TFunctionKey functionId, params string[] userNames)
        {
            Tuple<string, FilterType>[] tuples = (await GetUsersAsync(functionId)).ToArray();
            foreach (string userName in userNames)
            {
                if (tuples.Any(m => m.Item1 == userName && m.Item2 == FilterType.Refused))
                {
                    return false;
                }
                if (tuples.Any(m => m.Item1 == userName && m.Item2 == FilterType.Allowed))
                {
                    return true;
                }
            }
            return true;
        }

        #endregion
    }
}