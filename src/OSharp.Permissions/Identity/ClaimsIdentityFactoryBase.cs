// -----------------------------------------------------------------------
//  <copyright file="ClaimsIdentityFactoryBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-08 9:16</last-date>
// -----------------------------------------------------------------------

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using OSharp.Core.Identity.Models;
using OSharp.Utility.Extensions;


namespace OSharp.Core.Identity
{
    /// <summary>
    /// ClaimsIdentity创建工厂基类
    /// </summary>
    public abstract class ClaimsIdentityFactoryBase<TUser, TUserKey> : ClaimsIdentityFactory<TUser, TUserKey>
        where TUser : UserBase<TUserKey>
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// 由用户信息创建<see cref="ClaimsIdentity"/>对象，重写添加昵称和邮箱信息
        /// </summary>
        /// <param name="manager">用户管理器</param>
        /// <param name="user">用户信息</param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        public override async Task<ClaimsIdentity> CreateAsync(UserManager<TUser, TUserKey> manager, TUser user, string authenticationType)
        {
            ClaimsIdentity identity = await base.CreateAsync(manager, user, authenticationType);
            if (!user.NickName.IsMissing())
            {
                identity.AddClaim(new Claim(ClaimTypes.GivenName, user.NickName));
            }
            if (!user.Email.IsMissing() && user.EmailConfirmed)
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            }
            return identity;
        }
    }
}
