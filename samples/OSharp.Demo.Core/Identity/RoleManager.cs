// -----------------------------------------------------------------------
//  <copyright file="RoleManager.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-05 18:45</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNet.Identity;

using OSharp.Core.Dependency;
using OSharp.Demo.Models.Identity;


namespace OSharp.Demo.Identity
{
    /// <summary>
    /// 角色信息管理器
    /// </summary>
    public class RoleManager : RoleManager<Role, int>, IScopeDependency
    {
        /// <summary>
        /// 初始化一个<see cref="RoleManager"/>类型的新实例
        /// </summary>
        public RoleManager(IRoleStore<Role, int> store)
            : base(store)
        {
            RoleValidator = new RoleValidator<Role, int>(this);
        }
    }
}