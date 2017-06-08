// -----------------------------------------------------------------------
//  <copyright file="UserRoleMapConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-17 23:02</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Data.Entity;
using OSharp.Demo.Models.Identity;


namespace OSharp.Demo.ModelConfigurations.Identity
{
    public class UserRoleMapConfiguration : EntityConfigurationBase<UserRoleMap, Int32>
    {
        /// <summary>
        /// 初始化一个<see cref="UserRoleMapConfiguration"/>类型的新实例
        /// </summary>
        public UserRoleMapConfiguration()
        {
            UserExtendConfigurationAppend();
        }

        private void UserExtendConfigurationAppend()
        {
            HasRequired(m => m.User).WithMany();
            HasRequired(m => m.Role).WithMany();
        }
    }
}