// -----------------------------------------------------------------------
//  <copyright file="UserLogin.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-25 14:39</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;

using OSharp.Core.Identity.Models;


namespace OSharp.Demo.Models.Identity
{
    /// <summary>
    /// 实体类——用户第三方登录（OAuth，如facebook,google）信息
    /// </summary>
    [Description("认证-第三方登录信息")]
    public class UserLogin : UserLoginBase<int, User, int>
    { }
}