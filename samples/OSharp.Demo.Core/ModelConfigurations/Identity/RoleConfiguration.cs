// -----------------------------------------------------------------------
//  <copyright file="RoleConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-24 17:02</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Demo.ModelConfigurations.Identity
{
    public partial class RoleConfiguration
    {
        partial void RoleConfigurationAppend()
        {
            //HasRequired(m => m.Organization).WithMany(n => n.Roles);
            HasOptional(m => m.Organization).WithMany(n => n.Roles);
        }
    }
}