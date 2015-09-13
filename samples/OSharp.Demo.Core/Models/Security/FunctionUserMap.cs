// -----------------------------------------------------------------------
//  <copyright file="FunctionUserMap.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-04 14:00</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Security;
using OSharp.Core.Security.Models;
using OSharp.Demo.Models.Identity;


namespace OSharp.Demo.Models.Security
{
    /// <summary>
    /// 实体类——功能用户映射信息
    /// </summary>
    [Description("权限-功能用户映射信息")]
    public class FunctionUserMap : FunctionUserMapBase<int, Function, Guid, User, int>
    { }
}