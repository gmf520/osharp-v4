// -----------------------------------------------------------------------
//  <copyright file="EntityInfoConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-14 3:29</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data.Entity;
using OSharp.Core.Security;
using OSharp.SiteBase;
using OSharp.SiteBase.Security;


namespace OSharp.Demo.ModelConfigurations.Security
{
    /// <summary>
    /// 实体映射类——实体信息
    /// </summary>
    public class EntityInfoConfiguration : EntityConfigurationBase<EntityInfo, Guid>
    { }
}