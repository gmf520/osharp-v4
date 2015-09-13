// -----------------------------------------------------------------------
//  <copyright file="EntityRoleMapConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-07 3:55</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data.Entity;
using OSharp.Demo.Models.Security;


namespace OSharp.Demo.ModelConfigurations.Security
{
    /// <summary>
    /// 实体映射类——数据角色映射信息
    /// </summary>
    public class EntityRoleMapConfiguration : EntityConfigurationBase<EntityRoleMap, int>
    { }
}