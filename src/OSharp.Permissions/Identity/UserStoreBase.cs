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
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;
using OSharp.Core.Data.Extensions;
using OSharp.Core.Dependency;
using OSharp.Core.Identity.Dtos;
using OSharp.Core.Identity.Models;
using OSharp.Core.Mapping;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;
#pragma warning disable 414


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
    /// <typeparam name="TUserRoleMapInputDto">用户角色映射输入类型</typeparam>
    public abstract class UserStoreBase<TUser, TUserKey, TRole, TRoleKey, TUserRoleMap, TUserRoleMapInputDto, TUserRoleMapKey, TUserLogin, TUserLoginKey, TUserClaim, TUserClaimKey> :
        IQueryableUserStore<TUser, TUserKey>,
        IUserRoleStore<TUser, TUserKey>,
        IUserRoleMapStore<TUserRoleMapInputDto, TUserRoleMapKey, TUserKey, TRoleKey>,
        IUserLoginStore<TUser, TUserKey>,
        IUserClaimStore<TUser, TUserKey>,
        IUserPasswordStore<TUser, TUserKey>,
        IUserSecurityStampStore<TUser, TUserKey>,
        IUserEmailStore<TUser, TUserKey>,
        IUserPhoneNumberStore<TUser, TUserKey>,
        IUserTwoFactorStore<TUser, TUserKey>,
        IUserLockoutStore<TUser, TUserKey>,
        IScopeDependency
        where TUser : UserBase<TUserKey>
        where TRole : RoleBase<TRoleKey>
        where TUserRoleMap : UserRoleMapBase<TUserRoleMapKey, TUser, TUserKey, TRole, TRoleKey>, new()
        where TUserRoleMapInputDto : UserRoleMapBaseInputDto<TUserRoleMapKey, TUserKey, TRoleKey>
        where TUserLogin : UserLoginBase<TUserLoginKey, TUser, TUserKey>, new()
        where TUserClaim : UserClaimBase<TUserClaimKey, TUser, TUserKey>, new()
        where TUserKey : IEquatable<TUserKey>
        where TRoleKey : IEquatable<TRoleKey>
        where TUserRoleMapKey : IEquatable<TUserRoleMapKey>
        where TUserLoginKey : IEquatable<TUserLoginKey>
        where TUserClaimKey : IEquatable<TUserClaimKey>
    {
        private bool _disposed;

        #region 仓储属性

        /// <summary>
        /// 获取或设置 用户仓储对象
        /// </summary>
        public IRepository<TUser, TUserKey> UserRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 角色仓储对象
        /// </summary>
        public IRepository<TRole, TRoleKey> RoleRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 用户角色映射仓储对象
        /// </summary>
        public IRepository<TUserRoleMap, TUserRoleMapKey> UserRoleMapRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 用户第三方登录仓储对象
        /// </summary>
        public IRepository<TUserLogin, TUserLoginKey> UserLoginRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 用户摘要标识仓储对象
        /// </summary>
        public IRepository<TUserClaim, TUserClaimKey> UserClaimRepository { protected get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
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
            if (user.NickName.IsMissing())
            {
                user.NickName = user.UserName;
            }
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
            if (user.NickName.IsMissing())
            {
                user.NickName = user.UserName;
            }
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
            return await Task.Run(() => UserRepository.TrackEntities.FirstOrDefault(m => m.UserName.ToUpper() == userName.ToUpper()));
        }

        #endregion

        #region Implementation of IUserRoleStore<TUser,in TUserKey>

        /// <summary>
        /// 给用户添加角色
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        public virtual async Task AddToRoleAsync(TUser user, string roleName)
        {
            user.CheckNotNull("user");
            roleName.CheckNotNull("roleName");
            bool exist = await UserRoleMapRepository.CheckExistsAsync(m => m.User.Id.Equals(user.Id) && m.Role.Name == roleName);
            if (exist)
            {
                return;
            }
            TRole role = RoleRepository.TrackEntities.FirstOrDefault(m => m.Name == roleName);
            if (role == null)
            {
                throw new InvalidOperationException("名称为“{0}”的角色信息不存在".FormatWith(roleName));
            }
            TUserRoleMap map = new TUserRoleMap() { User = user, Role = role };
            await UserRoleMapRepository.InsertAsync(map);
        }

        /// <summary>
        /// 移除用户的角色
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        public virtual async Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            user.CheckNotNull("user");
            roleName.CheckNotNull("roleName");
            TUserRoleMap map = UserRoleMapRepository.TrackEntities.FirstOrDefault(m => m.User.Id.Equals(user.Id)
                && m.Role.Name == roleName);
            if (map == null)
            {
                return;
            }
            await UserRoleMapRepository.DeleteAsync(map);
        }

        /// <summary>
        /// 获取用户的角色名称名
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public virtual Task<IList<string>> GetRolesAsync(TUser user)
        {
            IList<string> roleNames = UserRoleMapRepository.Entities.Where(m => m.User.Id.Equals(user.Id))
                .Unlocked().Unexpired().Select(m => m.Role.Name).ToList();
            return Task.FromResult(roleNames);
        }

        /// <summary>
        /// 判断用户是否
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        public virtual Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            bool exist = UserRoleMapRepository.Entities.Where(m => m.User.Id.Equals(user.Id) && m.Role.Name == roleName)
                .Unlocked().Unexpired().Any();
            return Task.FromResult(exist);
        }

        #endregion

        #region Implementation of IUserRoleMapStore<in TUserRoleMapInputDto,in TUserRoleMapKey,in TUserKey,TRoleKey>

        /// <summary>
        /// 添加用户角色映射信息
        /// </summary>
        /// <param name="dto">用户角色映射信息输入DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> CreateUserRoleMapAsync(TUserRoleMapInputDto dto)
        {
            dto.CheckNotNull("dto");
            dto.ThrowIfTimeInvalid();
            Expression<Func<TUserRoleMap, bool>> predicate = m => m.User.Id.Equals(dto.UserId) && m.Role.Id.Equals(dto.RoleId);
            TUserRoleMap map = UserRoleMapRepository.TrackEntities.Where(predicate).FirstOrDefault();
            if (map != null)
            {
                return new OperationResult(OperationResultType.Error,
                    "“{0}-{1}”的映射信息已存在，不能重复添加".FormatWith(map.User.NickName, map.Role.Name));
            }

            map = dto.MapTo<TUserRoleMap>();
            TUser user = UserRepository.GetByKey(dto.UserId);
            if (user == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的用户信息不存在");
            }
            map.User = user;
            TRole role = RoleRepository.GetByKey(dto.RoleId);
            if (role == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的角色信息不存在");
            }
            map.Role = role;
            return await UserRoleMapRepository.InsertAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "用户“{0}”与角色“{1}”的映射信息创建成功".FormatWith(user.UserName, role.Name))
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 编辑用户角色映射信息
        /// </summary>
        /// <param name="dto">用户角色映射信息输入DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> UpdateUserRoleMapAsync(TUserRoleMapInputDto dto)
        {
            dto.CheckNotNull("dto");
            dto.ThrowIfTimeInvalid();
            Expression<Func<TUserRoleMap, bool>> predicate = m => m.User.Id.Equals(dto.UserId) && m.Role.Id.Equals(dto.RoleId);
            TUserRoleMap map = UserRoleMapRepository.TrackEntities.Where(predicate).FirstOrDefault();
            if (map != null && !map.Id.Equals(dto.Id))
            {
                return new OperationResult(OperationResultType.Error,
                    "“{0}-{1}”的指派信息已存在，不能重复添加".FormatWith(map.User.NickName, map.Role.Name));
            }
            if (map == null || !map.Id.Equals(dto.Id))
            {
                map = await UserRoleMapRepository.GetByKeyAsync(dto.Id);
            }
            map = dto.MapTo(map);
            if (map.User == null || !map.User.Id.Equals(dto.UserId))
            {
                TUser user = UserRepository.GetByKey(dto.UserId);
                if (user == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "指定编号的用户信息不存在");
                }
                map.User = user;
            }
            if (map.Role == null || !map.Role.Id.Equals(dto.RoleId))
            {
                TRole role = RoleRepository.GetByKey(dto.RoleId);
                if (role == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "指定编号的角色信息不存在");
                }
                map.Role = role;
            }
            return await UserRoleMapRepository.UpdateAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "用户“{0}”与角色“{1}”的映射信息更新成功".FormatWith(map.User.UserName, map.Role.Name))
                : OperationResult.NoChanged;
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
                return OperationResult.NoChanged;
            }
            return await UserRoleMapRepository.DeleteAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "用户角色映射信息删除成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 获取指定用户的有效角色编号集合
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>有效的角色编号集合</returns>
        public virtual Task<IList<TRoleKey>> GetRoleIdsAsync(TUserKey userId)
        {
            IList<TRoleKey> roleIds = UserRoleMapRepository.Entities.Where(m => m.User.Id.Equals(userId))
                .Unlocked().Unexpired().Select(m => m.Role.Id).ToList();
            return Task.FromResult(roleIds);
        }

        /// <summary>
        /// 获取指定用户的有效角色名称集合
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>有效的角色名称集合</returns>
        public virtual Task<IList<string>> GetRolesAsync(TUserKey userId)
        {
            IList<string> roleNames = UserRoleMapRepository.Entities.Where(m => m.User.Id.Equals(userId))
                .Unlocked().Unexpired().Select(m => m.Role.Name).ToList();
            return Task.FromResult(roleNames);
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

        #endregion

        #region Implementation of IUserLoginStore<TUser,in TUserKey>

        /// <summary>
        /// 添加用户第三方登录信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="login">第三方登录信息</param>
        /// <returns></returns>
        public virtual async Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            user.CheckNotNull("user");
            login.CheckNotNull("login");
            TUserLogin userLogin = new TUserLogin() { LoginProvider = login.LoginProvider, ProviderKey = login.ProviderKey, User = user };
            await UserLoginRepository.InsertAsync(userLogin);
        }

        /// <summary>
        /// 移除用户第三方登录信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="login">第三方登录信息</param>
        /// <returns></returns>
        public virtual async Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            user.CheckNotNull("user");
            login.CheckNotNull("login");
            await UserLoginRepository.DeleteAsync(
                m => m.User.Id.Equals(user.Id) && m.LoginProvider == login.LoginProvider && m.ProviderKey == login.ProviderKey);
        }

        /// <summary>
        /// 由用户信息获取所有第三方登录信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public virtual async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            user.CheckNotNull("user");
            IQueryable<UserLoginInfo> result = UserLoginRepository.Entities.Where(m => m.User.Id.Equals(user.Id))
                .Select(m => new UserLoginInfo(m.LoginProvider, m.ProviderKey));
            return await Task.FromResult(result.ToList());
        }

        /// <summary>
        /// 由第三方登录信息查找所属用户信息
        /// </summary>
        /// <param name="login">第三方登录信息</param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取用户的所有摘要标识信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public virtual async Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            user.CheckNotNull("user");
            var claims = UserClaimRepository.Entities.Where(m => m.User.Id.Equals(user.Id))
                .Select(m => new { m.ClaimType, m.ClaimValue }).ToList()
                .Select(m => new Claim(m.ClaimType, m.ClaimValue));
            return await Task.FromResult(claims.ToList());
        }

        /// <summary>
        /// 添加用户摘要标识信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="claim">摘要标识信息</param>
        /// <returns></returns>
        public virtual async Task AddClaimAsync(TUser user, Claim claim)
        {
            user.CheckNotNull("user");
            claim.CheckNotNull("claim");
            TUserClaim userClaim = new TUserClaim() { ClaimType = claim.Type, ClaimValue = claim.Value, User = user };
            await UserClaimRepository.InsertAsync(userClaim);
        }

        /// <summary>
        /// 移除用户摘要标识信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="claim">摘要标识信息</param>
        /// <returns></returns>
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

        /// <summary>
        /// 设置用户登录密码
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="passwordHash">登录密码的Hash字符串</param>
        /// <returns></returns>
        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            user.CheckNotNull("user");
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        /// <summary>
        /// 获取用户登录密码的Hash字符串
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<string> GetPasswordHashAsync(TUser user)
        {
            user.CheckNotNull("user");
            return Task.FromResult(user.PasswordHash);
        }

        /// <summary>
        /// 判断用户是否存在登录密码
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        #endregion

        #region Implementation of IUserSecurityStampStore<TUser,in TUserKey>

        /// <summary>
        /// 设置用户的安全标识
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="stamp">安全标识</param>
        /// <returns></returns>
        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            user.CheckNotNull("user");
            user.SecurityStmp = stamp;
            return Task.FromResult(0);
        }

        /// <summary>
        /// 获取用户的安全标识
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<string> GetSecurityStampAsync(TUser user)
        {
            user.CheckNotNull("user");
            return Task.FromResult(user.SecurityStmp);
        }

        #endregion

        #region Implementation of IUserEmailStore<TUser,in TUserKey>

        /// <summary>
        /// 设置用户邮箱
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="email">邮箱信息</param>
        /// <returns></returns>
        public Task SetEmailAsync(TUser user, string email)
        {
            user.CheckNotNull("user");
            user.Email = email;
            return Task.FromResult(0);
        }

        /// <summary>
        /// 获取用户邮箱
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<string> GetEmailAsync(TUser user)
        {
            user.CheckNotNull("user");
            return Task.FromResult(user.Email);
        }

        /// <summary>
        /// 判断用户邮箱是否已确认
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            user.CheckNotNull("user");
            return Task.FromResult(user.EmailConfirmed);
        }

        /// <summary>
        /// 设置用户邮箱是否确认
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="confirmed">是否确认</param>
        /// <returns></returns>
        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            user.CheckNotNull("user");
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        /// <summary>
        /// 通过邮箱查找用户信息
        /// </summary>
        /// <param name="email">邮箱信息</param>
        /// <returns></returns>
        public async Task<TUser> FindByEmailAsync(string email)
        {
            email.CheckNotNull("email");
            return await Task.Run(() => UserRepository.TrackEntities.FirstOrDefault(m => m.Email.Equals(email)));
        }

        #endregion

        #region Implementation of IUserPhoneNumberStore<TUser,in TUserKey>

        /// <summary>
        /// 设置用户电话号码
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="phoneNumber">电话号码</param>
        /// <returns></returns>
        public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            user.CheckNotNull("user");
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        /// <summary>
        /// 获取用户电话号码
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<string> GetPhoneNumberAsync(TUser user)
        {
            user.CheckNotNull("user");
            return Task.FromResult(user.PhoneNumber);
        }

        /// <summary>
        /// 判断用户电话号码是否已确认
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            user.CheckNotNull("user");
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        /// <summary>
        /// 设置用户电话号码是否确认
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="confirmed">是否确认</param>
        /// <returns></returns>
        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            user.CheckNotNull("user");
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        #endregion

        #region Implementation of IUserTwoFactorStore<TUser,in TUserKey>

        /// <summary>
        /// 设置用户是否启用二元认证
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="enabled">是否启用二元认证</param>
        /// <returns></returns>
        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            user.CheckNotNull("user");
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        /// <summary>
        /// 获取用户是否启用二元认证
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            user.CheckNotNull("user");
            return Task.FromResult(user.TwoFactorEnabled);
        }

        #endregion

        #region Implementation of IUserLockoutStore<TUser,in TUserKey>

        /// <summary>
        /// 获取用户锁定截止时间
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            user.CheckNotNull("user");
            return Task.FromResult(user.LockoutEndDateUtc.HasValue
                ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                : new DateTimeOffset());
        }

        /// <summary>
        /// 设置用户锁定截止时间
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="lockoutEnd">锁定截止时间</param>
        /// <returns></returns>
        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            user.CheckNotNull("user");
            user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? (DateTime?)null : lockoutEnd.UtcDateTime;
            return Task.FromResult(0);
        }

        /// <summary>
        /// 增加用户登录失败次数
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            user.CheckNotNull("user");
            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>
        /// 重置用户登录失败次数
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task ResetAccessFailedCountAsync(TUser user)
        {
            user.CheckNotNull("user");
            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        /// <summary>
        /// 获取用户登录失败次数
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            user.CheckNotNull("user");
            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>
        /// 获取用户是否被锁定
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            user.CheckNotNull("user");
            return Task.FromResult(user.LockoutEnabled);
        }

        /// <summary>
        /// 设置用户是否被锁定
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            user.CheckNotNull("user");
            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        #endregion

    }
}