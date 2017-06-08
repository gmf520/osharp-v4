// -----------------------------------------------------------------------
//  <copyright file="IdentityService.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-30 3:35</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Data;
using OSharp.Demo.Contracts;
using OSharp.Demo.Models.Identity;


namespace OSharp.Demo.Services
{
    /// <summary>
    /// 业务实现——身份认证模块
    /// </summary>
    public partial class IdentityService : IIdentityContract
    {
        /// <summary>
        /// 获取或设置 组织机构信息仓储操作对象
        /// </summary>
        public IRepository<Organization, int> OrganizationRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 角色信息仓储对象
        /// </summary>
        public IRepository<Role, int> RoleRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 用户信息仓储对象
        /// </summary>
        public IRepository<User, int> UserRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 用户扩展信息仓储对象
        /// </summary>
        public IRepository<UserExtend, int> UserExtendRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 用户角色映射信息仓储对象
        /// </summary>
        public IRepository<UserRoleMap, int> UserRoleMapRepository { protected get; set; }
    }
}