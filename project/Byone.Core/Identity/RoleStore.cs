﻿// -----------------------------------------------------------------------
//  <copyright file="RoleStore.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-03 11:48</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data;
using OSharp.Core.Identity;
using Byone.Core.Models.Identity;


namespace Byone.Core.Identity
{
    /// <summary>
    /// 角色存储实现
    /// </summary>
    public class RoleStore : RoleStoreBase<Role, int>
    { }
}