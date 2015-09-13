// -----------------------------------------------------------------------
//  <copyright file="UserStoreBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-03 12:16</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;
using OSharp.Core.Dependency;
using OSharp.Core.Identity.Models;
using OSharp.Utility;


namespace OSharp.Core.Identity
{
    /// <summary>
    /// 用户存储基类
    /// </summary>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    /// <typeparam name="TUserRoleMap">用户角色映射类型</typeparam>
    /// <typeparam name="TUserRoleMapKey">用户角色映射编号类型</typeparam>
    /// <typeparam name="TUserLogin">用户第三方登录类型</typeparam>
    /// <typeparam name="TUserLoginKey">用户第三方登录编号类型</typeparam>
    /// <typeparam name="TUserClaim">用户摘要标识类型</typeparam>
    /// <typeparam name="TUserClaimKey">用户摘要标识编号类型</typeparam>
    public abstract class UserStoreBase<TUser, TUserKey, TRole, TRoleKey, TUserRoleMap, TUserRoleMapKey, TUserLogin, TUserLoginKey, TUserClaim, TUserClaimKey> :
        IQueryableUserStore<TUser, TUserKey>,
        IUserRoleStore<TUser, TUserKey>,
        IUserLoginStore<TUser, TUserKey>,
        IUserClaimStore<TUser, TUserKey>,
        IUserPasswordStore<TUser, TUserKey>,
        IUserSecurityStampStore<TUser, TUserKey>,
        IUserEmailStore<TUser, TUserKey>,
        IUserPhoneNumberStore<TUser, TUserKey>,
        IUserTwoFactorStore<TUser, TUserKey>,
        IUserLockoutStore<TUser, TUserKey>,
        ILifetimeScopeDependency
        where TUser : UserBase<TUserKey>
        where TRole : RoleBase<TRoleKey>
        where TUserRoleMap : IUserRoleMap<TUserRoleMapKey, TUser, TUserKey, TRole, TRoleKey>, new()
        where TUserLogin : IUserLogin<TUserLoginKey, TUser, TUserKey>, new()
        where TUserClaim : IUserClaim<TUserClaimKey, TUser, TUserKey>, new()
    {
        private bool _disposed;

        #region 仓储属性

        /// <summary>
        /// 获取或设置 用户仓储对象
        /// </summary>
        public IRepository<TUser, TUserKey> UserRepository { get; set; }

        /// <summary>
        /// 获取或设置 角色仓储对象
        /// </summary>
        public IRepository<TRole, TRoleKey> RoleRepository { get; set; }

        /// <summary>
        /// 获取或设置 用户角色映射仓储对象
        /// </summary>
        public IRepository<TUserRoleMap, TUserRoleMapKey> UserRoleMapRepository { get; set; }

        /// <summary>
        /// 获取或设置 用户第三方登录仓储对象
        /// </summary>
        public IRepository<TUserLogin, TUserLoginKey> UserLoginRepository { get; set; }

        /// <summary>
        /// 获取或设置 用户摘要标识仓储对象
        /// </summary>
        public IRepository<TUserClaim, TUserClaimKey> UserClaimRepository { get; set; }

        #endregion

        #region Implementation of IQueryableUserStore<TUser,in TUserKey>

        /// <summary>
        /// 获取 用户查询数据集
        /// </summary>
        public IQueryable<TUser> Users
        {
            get { return UserRepository.Entities; }
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _disposed = true;
        }

        #endregion

        #region Implementation of IUserStore<TUser,in TUserKey>

        /// <summary>
        /// Insert a new user
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public virtual async Task CreateAsync(TUser user)
        {
            user.CheckNotNull("user");
            await UserRepository.InsertAsync(user);
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public virtual async Task UpdateAsync(TUser user)
        {
            user.CheckNotNull("user");
            await UserRepository.UpdateAsync(user);
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public virtual async Task DeleteAsync(TUser user)
        {
            user.CheckNotNull("user");
            await UserRepository.DeleteAsync(user);
        }

        /// <summary>
        /// Finds a user
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        public virtual async Task<TUser> FindByIdAsync(TUserKey userId)
        {
            return await UserRepository.GetByKeyAsync(userId);
        }

        /// <summary>
        /// Find a user by name
        /// </summary>
        /// <param name="userName"/>
        /// <returns/>
        public virtual async Task<TUser> FindByNameAsync(string userName)
        {
            userName.CheckNotNull("userName");
            return (await UserRepository.GetByPredicateAsync(m => m.UserName.ToUpper() == userName.ToUpper())).FirstOrDefault();
        }

        #endregion

        #region Implementation of IUserRoleStore<TUser,in TUserKey>

        /// <summary>
        /// Adds a user to a role
        /// </summary>
        /// <param name="user"/><param name="roleName"/>
        /// <returns/>
        public virtual async Task AddToRoleAsync(TUser user, string roleName)
        {
            user.CheckNotNull("user");
            roleName.CheckNotNull("roleName");
            bool exists = UserRoleMapRepository.Entities.Any(m => m.User.Id.Equals(user.Id) && m.Role.Name.Equals(roleName));
            if (exists)
            {
                return;
            }
            TRole role = (await RoleRepository.GetByPredicateAsync(m => m.Name.Equals(roleName))).FirstOrDefault();
            if (role == null)
            {
                throw new InvalidOperationException("指定名称的角色信息不存在");
            }
            TUserRoleMap map = new TUserRoleMap() { User = user, Role = role };
            await UserRoleMapRepository.InsertAsync(map);
        }

        /// <summary>
        /// Removes the role for the user
        /// </summary>
        /// <param name="user"/><param name="roleName"/>
        /// <returns/>
        public virtual async Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            user.CheckNotNull("user");
            roleName.CheckNotNull("roleName");
            TUserRoleMap map = (await UserRoleMapRepository.GetByPredicateAsync(m => m.User.Id.Equals(user.Id) && m.Role.Name.Equals(roleName)))
                .FirstOrDefault();
            if (map == null)
            {
                return;
            }
            await UserRoleMapRepository.DeleteAsync(map);
        }

        /// <summary>
        /// Returns the roles for this user
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public virtual async Task<IList<string>> GetRolesAsync(TUser user)
        {
            user.CheckNotNull("user");
            return await Task.FromResult(UserRoleMapRepository.Entities.Where(m => m.User.Id.Equals(user.Id)).Select(m => m.Role.Name).ToList());
        }

        /// <summary>
        /// Returns true if a user is in the role
        /// </summary>
        /// <param name="user"/><param name="roleName"/>
        /// <returns/>
        public virtual async Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            user.CheckNotNull("user");
            roleName.CheckNotNull("roleName");
            return await Task.FromResult(UserRoleMapRepository.Entities.Any(m => m.User.Id.Equals(user.Id) && m.Role.Name.Equals(roleName)));
        }

        #endregion

        #region Implementation of IUserLoginStore<TUser,in TUserKey>

        public virtual async Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            user.CheckNotNull("user");
            login.CheckNotNull("login");
            TUserLogin userLogin = new TUserLogin() { LoginProvider = login.LoginProvider, ProviderKey = login.ProviderKey, User = user };
            await UserLoginRepository.InsertAsync(userLogin);
        }

        public virtual async Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            user.CheckNotNull("user");
            login.CheckNotNull("login");
            await UserLoginRepository.DeleteAsync(
                m => m.User.Id.Equals(user.Id) && m.LoginProvider == login.LoginProvider && m.ProviderKey == login.ProviderKey);
        }

        public virtual async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            user.CheckNotNull("user");
            IQueryable<UserLoginInfo> result = UserLoginRepository.Entities.Where(m => m.User.Id.Equals(user.Id))
                .Select(m => new UserLoginInfo(m.LoginProvider, m.ProviderKey));
            return await Task.FromResult(result.ToList());
        }

        public virtual async Task<TUser> FindAsync(UserLoginInfo login)
        {
            login.CheckNotNull("login");
            string provider = login.LoginProvider;
            string key = login.ProviderKey;
            var user = UserLoginRepository.Entities.Where(m => m.ProviderKey == key && m.LoginProvider == provider)
                .Select(m => new { m.User.Id }).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            return await UserRepository.GetByKeyAsync(user.Id);
        }

        #endregion

        #region Implementation of IUserClaimStore<TUser,in TUserKey>

        public virtual async Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            user.CheckNotNull("user");
            IQueryable<Claim> claims = UserClaimRepository.Entities.Where(m => m.User.Id.Equals(user.Id))
                .Select(m => new Claim(m.ClaimType, m.ClaimValue));
            return await Task.FromResult(claims.ToList());
        }

        public virtual async Task AddClaimAsync(TUser user, Claim claim)
        {
            user.CheckNotNull("user");
            claim.CheckNotNull("claim");
            TUserClaim userClaim = new TUserClaim() { ClaimType = claim.Type, ClaimValue = claim.Value, User = user };
            await UserClaimRepository.InsertAsync(userClaim);
        }

        public virtual async Task RemoveClaimAsync(TUser user, Claim claim)
        {
            user.CheckNotNull("user");
            claim.CheckNotNull("claim");
            string type = claim.Type;
            string value = claim.Value;
            await UserClaimRepository.DeleteAsync(m => m.User.Id.Equals(user.Id) && m.ClaimType == type && m.ClaimValue == value);
        }

        #endregion

        #region Implementation of IUserPasswordStore<TUser,in TUserKey>

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            user.CheckNotNull("user" );
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(TUser user)
        {
            user.CheckNotNull("user" );
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        #endregion

        #region Implementation of IUserSecurityStampStore<TUser,in TUserKey>

        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            user.CheckNotNull("user" );
            user.SecurityStmp = stamp;
            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(TUser user)
        {
            user.CheckNotNull("user" );
            return Task.FromResult(user.SecurityStmp);
        }

        #endregion

        #region Implementation of IUserEmailStore<TUser,in TUserKey>

        public Task SetEmailAsync(TUser user, string email)
        {
            user.CheckNotNull("user" );
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(TUser user)
        {
            user.CheckNotNull("user" );
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            user.CheckNotNull("user" );
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            user.CheckNotNull("user" );
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public async Task<TUser> FindByEmailAsync(string email)
        {
            email.CheckNotNull("email" );
            return (await UserRepository.GetByPredicateAsync(m => m.Email.Equals(email))).FirstOrDefault();
        }

        #endregion

        #region Implementation of IUserPhoneNumberStore<TUser,in TUserKey>

        public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            user.CheckNotNull("user" );
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(TUser user)
        {
            user.CheckNotNull("user" );
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            user.CheckNotNull("user" );
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            user.CheckNotNull("user" );
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        #endregion

        #region Implementation of IUserTwoFactorStore<TUser,in TUserKey>

        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            user.CheckNotNull("user" );
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            user.CheckNotNull("user" );
            return Task.FromResult(user.TwoFactorEnabled);
        }

        #endregion

        #region Implementation of IUserLockoutStore<TUser,in TUserKey>

        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            user.CheckNotNull("user" );
            return Task.FromResult(user.LockoutEndDateUtc.HasValue
                ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                : new DateTimeOffset());
        }

        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            user.CheckNotNull("user" );
            user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? (DateTime?)null : lockoutEnd.UtcDateTime;
            return Task.FromResult(0);
        }

        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            user.CheckNotNull("user" );
            user.AccessFailedCount++;
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(TUser user)
        {
            user.CheckNotNull("user" );
            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            user.CheckNotNull("user" );
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            user.CheckNotNull("user" );
            return Task.FromResult(user.LockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            user.CheckNotNull("user" );
            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        #endregion
    }
}