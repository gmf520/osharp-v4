// -----------------------------------------------------------------------
//  <copyright file="UserValidatorBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-08 9:16</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using OSharp.Core.Identity.Models;
using OSharp.Utility.Extensions;


namespace OSharp.Core.Identity
{
    /// <summary>
    /// 用户验证基类，添加自定义用户验证规则
    /// </summary>
    public partial class UserValidatorBase<TUser, TUserKey> : UserValidator<TUser, TUserKey>
        where TUser : UserBase<TUserKey>
        where TUserKey : IEquatable<TUserKey>
    {
        private readonly UserManager<TUser, TUserKey> _manager;

        /// <summary>
        /// 初始化一个<see cref="UserValidator{TUser,TUserKey}"/>类型的新实例
        /// </summary>
        public UserValidatorBase(UserManager<TUser, TUserKey> manager)
            : base(manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// 获取或设置 是否限制用户昵称唯一
        /// </summary>
        public bool RequireUniqueNickName { get; set; }

        #region Overrides of UserValidator<TUser,TUserKey>

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="user">待验证的用户信息</param>
        /// <returns></returns>
        public async override Task<IdentityResult> ValidateAsync(TUser user)
        {
            IdentityResult result = await base.ValidateAsync(user);
            if (!result.Succeeded)
            {
                return result;
            }
            List<string> errors = result.Errors.ToList();
            ValidateNickName(user, errors);
            return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }

        #endregion

        private void ValidateNickName(TUser user, List<string> errors)
        {
            string nickName = user.NickName;
            if (user.NickName.IsMissing())
            {
                errors.Add("用户昵称不存在");
                return;
            }
            TUser existUser = _manager.Users.FirstOrDefault(m => m.NickName == nickName);
            if (existUser == null || user.Id.Equals(existUser.Id))
            {
                return;
            }
            errors.Add("用户昵称“{0}”已存在，请更换");
        }
    }
}
