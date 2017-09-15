// -----------------------------------------------------------------------
//  <copyright file="ModuleConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-05-06 14:16</last-date>
// -----------------------------------------------------------------------

using OSharp.Data.Entity;
using OSharp.Demo.Models.Security;


namespace OSharp.Demo.ModelConfigurations.Security
{
    public class ModuleConfiguration : EntityConfigurationBase<Module, int>
    {
        public ModuleConfiguration()
        {
            HasMany(m => m.SubModules).WithOptional(n => n.Parent);
            HasMany(m => m.Functions).WithMany();
            HasMany(m => m.Roles).WithMany();
            HasMany(m => m.Users).WithMany();
        }
    }
}