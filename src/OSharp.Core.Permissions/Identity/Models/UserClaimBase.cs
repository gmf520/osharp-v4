// -----------------------------------------------------------------------
//  <copyright file="UserClaimBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-25 13:17</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;


namespace OSharp.Core.Identity.Models
{
    /// <summary>
    /// 用户摘要标识信息基类
    /// </summary>
    /// <typeparam name="TKey">编号类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    public abstract class UserClaimBase<TKey, TUser, TUserKey> : EntityBase<TKey>, IUserClaim<TKey, TUser, TUserKey>
        where TUser : IUser<TUserKey>
    {
        /// <summary>
        /// 获取或设置 摘要类型
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// 获取或设置 摘要值
        /// </summary>
        public string ClaimValue { get; set; }

        /// <summary>
        /// 获取或设置 相关用户
        /// </summary>
        public virtual TUser User { get; set; }
    }
}