// -----------------------------------------------------------------------
//  <copyright file="FunctionMapStore.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-07 3:53</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Security;
using OSharp.Demo.Dtos.Security;
using OSharp.Demo.Models.Identity;
using OSharp.Demo.Models.Security;
using OSharp.Utility.Data;


namespace OSharp.Demo.Security
{
    /// <summary>
    /// 功能（角色、用户）映射存储
    /// </summary>
    public class FunctionMapStore
        : FunctionMapStoreBase<FunctionRoleMap, int, FunctionUserMap, int, FunctionRoleMapDto, FunctionUserMapDto, Function, Guid, Role, int, User, int>
    { }
}