// -----------------------------------------------------------------------
//  <copyright file="UserManager.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-03 11:47</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using OSharp.Core;
using OSharp.Core.Dependency;
using OSharp.Demo.Models.Identity;


namespace OSharp.Demo.Identity
{
    /// <summary>
    /// 用户管理器
    /// </summary>
    public class UserManager : UserManager<User, int>, ILifetimeScopeDependency
    {
        /// <summary>
        /// 初始化一个<see cref="UserManager"/>类型的新实例
        /// </summary>
        public UserManager(IUserStore<User, int> store)
            : base(store)
        { }
    }
}