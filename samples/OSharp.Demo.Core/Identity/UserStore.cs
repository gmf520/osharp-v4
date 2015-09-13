// -----------------------------------------------------------------------
//  <copyright file="UserStore.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-03 20:54</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data;
using OSharp.Core.Identity;
using OSharp.Demo.Models.Identity;


namespace OSharp.Demo.Identity
{
    /// <summary>
    /// 用户存储实现
    /// </summary>
    public class UserStore : UserStoreBase<User, int, Role, int, UserRoleMap, int, UserLogin, int, UserClaim, int>
    { }
}