// -----------------------------------------------------------------------
//  <copyright file="UserManagerBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-08 9:16</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using OSharp.Core.Identity.Dtos;
using OSharp.Core.Identity.Models;
using OSharp.Utility.Data;


namespace OSharp.Core.Identity
{
    /// <summary>
    /// 用户管理器基类
    /// </summary>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户主键类型</typeparam>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    /// <typeparam name="TUserRoleMap">用户角色映射类型</typeparam>
    /// <typeparam name="TUserRoleMapKey">用户角色映射编号类型</typeparam>
    /// <typeparam name="TUserRoleMapInputDto">用户角色映射输入类型</typeparam>
    public abstract class UserManagerBase<TUser, TUserKey, TRole, TRoleKey, TUserRoleMap, TUserRoleMapInputDto, TUserRoleMapKey>
        : UserManager<TUser, TUserKey>, IPasswordValidator
        where TUser : UserBase<TUserKey>
        where TRole : RoleBase<TRoleKey>
        where TUserRoleMap : UserRoleMapBase<TUserRoleMapKey, TUser, TUserKey, TRole, TRoleKey>, new()
        where TUserRoleMapInputDto : UserRoleMapBaseInputDto<TUserRoleMapKey, TUserKey, TRoleKey>
        where TUserKey : IEquatable<TUserKey>
        where TRoleKey : IEquatable<TRoleKey>
        where TUserRoleMapKey : IEquatable<TUserRoleMapKey>
    {
        /// <summary>
        /// 初始化一个<see cref="UserManagerBase"/>类型的新实例
        /// </summary>
        protected UserManagerBase(IUserStore<TUser, TUserKey> store)
            : base(store)
        { }

        /// <summary>
        /// 获取或设置 用户角色映射存储
        /// </summary>
        public IUserRoleMapStore<TUserRoleMapInputDto, TUserRoleMapKey, TUserKey, TRoleKey> UserRoleMapStore { get; set; }

        /// <summary>
        /// 验证用户名与密码是否匹配
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        public virtual async Task<bool> Validate(string userName, string password)
        {
            TUser user = await base.FindAsync(userName, password);
            return user != null;
        }
        
        /// <summary>
        /// 添加用户角色映射信息
        /// </summary>
        /// <param name="dto">用户角色映射信息输入DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual Task<OperationResult> CreateUserRoleMapAsync(TUserRoleMapInputDto dto)
        {
            return UserRoleMapStore.CreateUserRoleMapAsync(dto);
        }

        /// <summary>
        /// 编辑用户角色映射信息
        /// </summary>
        /// <param name="dto">用户角色映射信息输入DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual Task<OperationResult> UpdateUserRoleMapAsync(TUserRoleMapInputDto dto)
        {
            return UserRoleMapStore.UpdateUserRoleMapAsync(dto);
        }

        /// <summary>
        /// 删除用户角色映射信息
        /// </summary>
        /// <param name="id">用户角色映射编号</param>
        /// <returns>业务操作结果</returns>
        public virtual Task<OperationResult> DeleteUserRoleMapAsync(TUserRoleMapKey id)
        {
            return UserRoleMapStore.DeleteUserRoleMapAsync(id);
        }

        /// <summary>
        /// 获取指定用户的有效角色编号集合
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>有效的角色编号集合</returns>
        public virtual Task<IList<TRoleKey>> GetRoleIdsAsync(TUserKey userId)
        {
            return UserRoleMapStore.GetRoleIdsAsync(userId);
        }
        
        /// <summary>
        /// 返回用户是有拥有指定角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="roleId">要判断的角色名称</param>
        /// <returns>是否拥有</returns>
        public virtual Task<bool> IsInRoleAsync(TUserKey userId, TRoleKey roleId)
        {
            return UserRoleMapStore.IsInRoleAsync(userId, roleId);
        }
    }
}
