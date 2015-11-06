// -----------------------------------------------------------------------
//  <copyright file="EntityUserMapConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-07 3:58</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Data.Entity;
using Byone.Core.Models.Security;


namespace Byone.Core.ModelConfigurations.Security
{
    /// <summary>
    /// 实体映射类——数据用户映射信息
    /// </summary>
    public class EntityUserMapConfiguration : EntityConfigurationBase<EntityUserMap, int>
    { }
}