// -----------------------------------------------------------------------
//  <copyright file="UserLoginConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-25 14:42</last-date>
// -----------------------------------------------------------------------

using OSharp.Data.Entity;
using OSharp.Demo.Models.Identity;


namespace OSharp.Demo.ModelConfigurations.Identity
{
    public class UserLoginConfiguration : EntityConfigurationBase<UserLogin, int>
    {
        public UserLoginConfiguration()
        {
            HasRequired(m => m.User).WithMany();
        }
    }
}