// -----------------------------------------------------------------------
//  <copyright file="UserRoleMap.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-17 22:09</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;

using OSharp.Core.Identity.Models;


namespace OSharp.Demo.Models.Identity
{
    /// <summary>
    /// 实体类——用户角色映射
    /// </summary>
    [Description("认证-用户角色映射信息")]
    public class UserRoleMap : UserRoleMapBase<int, User, int, Role, int>
    { }
}