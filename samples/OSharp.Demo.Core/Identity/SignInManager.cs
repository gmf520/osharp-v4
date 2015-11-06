// -----------------------------------------------------------------------
//  <copyright file="SignInManager.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-05 18:47</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

using OSharp.Core.Dependency;
using OSharp.Demo.Models.Identity;


namespace OSharp.Demo.Identity
{
    /// <summary>
    /// 用户登录管理器
    /// </summary>
    public class SignInManager : SignInManager<User, int>, IScopeDependency
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager"/><param name="authenticationManager"/>
        public SignInManager(UserManager<User, int> userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        { }
    }
}