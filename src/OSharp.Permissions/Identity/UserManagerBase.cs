using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using OSharp.Core.Identity.Models;


namespace OSharp.Core.Identity
{
    /// <summary>
    /// 用户管理器基类
    /// </summary>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户主键类型</typeparam>
    public abstract class UserManagerBase<TUser, TUserKey> : UserManager<TUser, TUserKey>, IPasswordValidator
        where TUser : UserBase<TUserKey>
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="store"></param>
        protected UserManagerBase(IUserStore<TUser, TUserKey> store)
            : base(store)
        { }

        /// <summary>
        /// 验证用户名与密码是否匹配
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        public async virtual Task<bool> Validate(string userName, string password)
        {
            TUser user = await base.FindAsync(userName, password);
            return user != null;
        }

        [Obsolete("当前框架中UserManager不支持AddToRole操作，请使用IUserRoleMapStore接口进行相关操作")]
        public override Task<IdentityResult> AddToRoleAsync(TUserKey userId, string role)
        {
            throw new NotSupportedException("当前框架中UserManager不支持AddToRole操作，请使用IUserRoleMapStore接口进行相关操作");
        }

        [Obsolete("当前框架中UserManager不支持AddToRoles操作，请使用IUserRoleMapStore接口进行相关操作")]
        public override Task<IdentityResult> AddToRolesAsync(TUserKey userId, params string[] roles)
        {
            throw new NotSupportedException("当前框架中UserManager不支持AddToRoles操作，请使用IUserRoleMapStore接口进行相关操作");
        }

        [Obsolete("当前框架中UserManager不支持RemoveFromRole操作，请使用IUserRoleMapStore接口进行相关操作")]
        public override Task<IdentityResult> RemoveFromRoleAsync(TUserKey userId, string role)
        {
            throw new NotSupportedException("当前框架中UserManager不支持RemoveFromRole操作，请使用IUserRoleMapStore接口进行相关操作");
        }

        [Obsolete("当前框架中UserManager不支持RemoveFromRoles操作，请使用IUserRoleMapStore接口进行相关操作")]
        public override Task<IdentityResult> RemoveFromRolesAsync(TUserKey userId, params string[] roles)
        {
            throw new NotSupportedException("当前框架中UserManager不支持RemoveFromRoles操作，请使用IUserRoleMapStore接口进行相关操作");
        }

        [Obsolete("当前框架中UserManager不支持IsInRole操作，请使用IUserRoleMapStore接口进行相关操作")]
        public override Task<bool> IsInRoleAsync(TUserKey userId, string role)
        {
            throw new NotSupportedException("当前框架中UserManager不支持IsInRole操作，请使用IUserRoleMapStore接口进行相关操作");
        }

        [Obsolete("当前框架中UserManager不支持GetRoles操作，请使用IUserRoleMapStore接口进行相关操作")]
        public override Task<IList<string>> GetRolesAsync(TUserKey userId)
        {
            throw new NotSupportedException("当前框架中UserManager不支持GetRoles操作，请使用IUserRoleMapStore接口进行相关操作");
        }

    }
}
