// -----------------------------------------------------------------------
//  <copyright file="SecurityManager.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-05-06 14:07</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Security;
using OSharp.Demo.Dtos.Security;
using OSharp.Demo.Models.Identity;
using OSharp.Demo.Models.Security;


namespace OSharp.Demo.Security
{
    /// <summary>
    /// 权限安全管理器
    /// </summary>
    public class SecurityManager
        : SecurityManagerBase<Role, int, User, int, Module, ModuleInputDto, int, Function, FunctionInputDto, Guid,
              EntityInfo, EntityInfoInputDto, Guid, EntityRoleMap, EntityRoleMapInputDto,
              int, EntityUserMap, EntityUserMapInputDto, int>
    { }
}