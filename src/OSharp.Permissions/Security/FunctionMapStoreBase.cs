// -----------------------------------------------------------------------
//  <copyright file="FunctionMapStoreBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-13 0:41</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OSharp.Core.Data;
using OSharp.Core.Data.Extensions;
using OSharp.Core.Identity.Models;
using OSharp.Core.Mapping;
using OSharp.Core.Security.Dtos;
using OSharp.Core.Security.Models;
using OSharp.Utility;
using OSharp.Utility.Data;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 功能（角色、用户）映射存储基类
    /// </summary>
    /// <typeparam name="TFunctionUserMap">功能用户映射类型</typeparam>
    /// <typeparam name="TFunctionUserMapKey">功能用户映射编号类型</typeparam>
    /// <typeparam name="TFunctionUserMapDto">功能用户映射DTO类型</typeparam>
    /// <typeparam name="TFunction">功能类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    public abstract class FunctionMapStoreBase<TFunctionUserMap, TFunctionUserMapKey,
        TFunctionUserMapDto, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey>
        : IFunctionUserStore<TFunctionUserMapDto, TFunctionUserMapKey, TFunctionKey, TUserKey>
        where TFunctionUserMap : FunctionUserMapBase<TFunctionUserMapKey, TFunction, TFunctionKey, TUser, TUserKey>
        where TFunctionUserMapDto : FunctionUserMapBaseInputDto<TFunctionUserMapKey, TFunctionKey, TUserKey>
        where TFunction : FunctionBase<TFunctionKey>
        where TRole : RoleBase<TRoleKey>
        where TUser : UserBase<TUserKey>
        where TFunctionUserMapKey : IEquatable<TFunctionUserMapKey>
        where TFunctionKey : IEquatable<TFunctionKey>
        where TRoleKey : IEquatable<TRoleKey>
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// 获取或设置 功能仓储对象
        /// </summary>
        public IRepository<TFunction, TFunctionKey> FunctionRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 角色仓储对象
        /// </summary>
        public IRepository<TRole, TRoleKey> RoleRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 用户仓储对象
        /// </summary>
        public IRepository<TUser, TUserKey> UserRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 功能用户映射仓储对象
        /// </summary>
        public IRepository<TFunctionUserMap, TFunctionUserMapKey> FunctionUserMapRepository { protected get; set; }

        #region Implementation of IFunctionUserStore<in TFunctionUserMapDto,in TFunctionUserMapKey,in TFunctionKey,TUserKey>

        /// <summary>
        /// 增加功能用户映射信息
        /// </summary>
        /// <param name="dto">功能用户映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> CreateFunctionUserMapAsync(TFunctionUserMapDto dto)
        {
            dto.CheckNotNull("dto");
            if (await FunctionUserMapRepository.CheckExistsAsync(m => m.Function.Id.Equals(dto.FunctionId) && m.User.Id.Equals(dto.UserId)))
            {
                return OperationResult.Success;
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
            await FunctionUserMapRepository.InsertAsync(map);
            return new OperationResult(OperationResultType.Success, "功能用户映射信息添加成功");
        }

        /// <summary>
        /// 编辑功能用户映射信息
        /// </summary>
        /// <param name="dto">功能用户映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> UpdateFunctionUserMapAsync(TFunctionUserMapDto dto)
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
            await FunctionUserMapRepository.UpdateAsync(map);
            return new OperationResult(OperationResultType.Success, "功能用户映射信息编辑成功");
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
            await FunctionUserMapRepository.DeleteAsync(map);
            return new OperationResult(OperationResultType.Success, "功能用户映射信息删除成功");
        }

        /// <summary>
        /// 获取功能的用户及其限制类型
        /// </summary>
        /// <param name="functionId">功能编号</param>
        /// <returns>用户及其限制类型的集合</returns>
        public virtual Task<IEnumerable<Tuple<string, FilterType>>> GetUsersAsync(TFunctionKey functionId)
        {
            var result = FunctionUserMapRepository.Entities.Where(m => m.Function.Id.Equals(functionId))
                .Unlocked().Select(m => new Tuple<string, FilterType>(m.User.UserName, m.FilterType));
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