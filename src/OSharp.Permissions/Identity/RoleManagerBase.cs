// -----------------------------------------------------------------------
//  <copyright file="RoleManagerBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-02-29 15:57</last-date>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using OSharp.Core.Dependency;
using OSharp.Core.Identity.Models;


namespace OSharp.Core.Identity
{
    /// <summary>
    /// 角色管理器基类
    /// </summary>
    /// <typeparam name="TRole"></typeparam>
    /// <typeparam name="TRoleKey"></typeparam>
    public abstract class RoleManagerBase<TRole, TRoleKey> : RoleManager<TRole, TRoleKey>, IScopeDependency
        where TRole : RoleBase<TRoleKey>
        where TRoleKey : IEquatable<TRoleKey>
    {
        /// <summary>
        /// 初始化一个<see cref="RoleManagerBase{TRole,TRokeKey}"/>类型的新实例
        /// </summary>
        protected RoleManagerBase(IRoleStore<TRole, TRoleKey> store)
            : base(store)
        { }

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns></returns>
        public override async Task<IdentityResult> UpdateAsync(TRole role)
        {
            if (role.IsSystem)
            {
                return new IdentityResult("系统角色不允许编辑");
            }
            return await base.UpdateAsync(role);
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns></returns>
        public override async Task<IdentityResult> DeleteAsync(TRole role)
        {
            if (role.IsSystem)
            {
                return new IdentityResult("系统角色不允许删除");
            }
            return await base.DeleteAsync(role);
        }

    }
}