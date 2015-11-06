﻿// -----------------------------------------------------------------------
//  <copyright file="EntityRoleMap.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-07 3:46</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Security;
using OSharp.Core.Security.Models;
using Byone.Core.Models.Identity;


namespace Byone.Core.Models.Security
{
    /// <summary>
    /// 实体类——数据角色映射信息
    /// </summary>
    [Description("权限-数据角色映射信息")]
    public class EntityRoleMap : EntityRoleMapBase<int, EntityInfo, Guid, Role, int>
    { }
}