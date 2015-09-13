// -----------------------------------------------------------------------
//  <copyright file="UserClaimConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-25 14:42</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data.Entity;
using OSharp.Demo.Models.Identity;


namespace OSharp.Demo.ModelConfigurations.Identity
{
    public class UserClaimConfiguration : EntityConfigurationBase<UserClaim, int>
    {
        public UserClaimConfiguration()
        {
            HasRequired(m => m.User).WithMany();
        }
    }
}