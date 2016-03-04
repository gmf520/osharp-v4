// -----------------------------------------------------------------------
//  <copyright file="UserManager.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-03 11:47</last-date>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

using OSharp.Core.Identity;
using OSharp.Demo.Models.Identity;
using OSharp.Demo.Dtos.Identity;

namespace OSharp.Demo.Identity
{
    /// <summary>
    /// 用户管理器
    /// </summary>
    public class UserManager : UserManagerBase<User, int, Role, int, UserRoleMap, UserRoleMapInputDto, int>
    {
        /// <summary>
        /// 初始化一个<see cref="UserManager"/>类型的新实例
        /// </summary>
        public UserManager(IUserStore<User, int> store, IDataProtectionProvider dataProtectionProvider)
            : base(store)
        {
            //配置用户名的验证逻辑
            UserValidator = new UserValidator<User, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            //配置密码的验证逻辑
            PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true
            };

            //配置用户锁定默认值
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            // 注册双重身份验证提供程序。此应用程序使用手机和电子邮件作为接收用于验证用户的代码的一个步骤
            // 你可以编写自己的提供程序并将其插入到此处。
            RegisterTwoFactorProvider("电话代码", new PhoneNumberTokenProvider<User, int>() { MessageFormat = "你的安全代码是 {0}" });
            RegisterTwoFactorProvider("电子邮件代码", new EmailTokenProvider<User, int>() { Subject = "安全代码", BodyFormat = "你的安全代码是 {0}" });
            SmsService = new EmailService();
            EmailService = new EmailService();
            IDataProtector dataProtector = dataProtectionProvider.Create("ASP.NET Identity");
            UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtector);
        }

    }


    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return Task.FromResult(0);
        }
    }


    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return Task.FromResult(0);
        }
    }
}