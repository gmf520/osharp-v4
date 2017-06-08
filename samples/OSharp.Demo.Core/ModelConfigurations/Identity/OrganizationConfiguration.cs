// -----------------------------------------------------------------------
//  <copyright file="OrganizationConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-24 17:01</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Data.Entity;


namespace OSharp.Demo.ModelConfigurations.Identity
{
    public partial class OrganizationConfiguration
    {
        /// <summary>
        /// 获取 相关上下文类型
        /// </summary>
        public override Type DbContextType
        {
            get { return typeof(DefaultDbContext); }
        }

        partial void OrganizationConfigurationAppend()
        {
            HasOptional(m => m.Parent).WithMany(n => n.Children);
        }
    }
}