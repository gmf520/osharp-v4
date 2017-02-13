// -----------------------------------------------------------------------
//  <copyright file="EntityUserMap.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-07 3:47</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;

using OSharp.Core.Security;
using OSharp.Core.Security.Models;
using OSharp.Demo.Models.Identity;


namespace OSharp.Demo.Models.Security
{
    /// <summary>
    /// 实体类——数据用户映射信息
    /// </summary>
    [Description("权限-数据用户映射信息")]
    public class EntityUserMap : EntityUserMapBase<int, EntityInfo, Guid, User, int>
    { }
}