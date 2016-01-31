// -----------------------------------------------------------------------
//  <copyright file="Member.cs" company="OSharp开源团队">
//      Copyright (c) 2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-01-07 23:33</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;

using OSharp.Core.Identity.Models;


namespace OSharp.Demo.Models.Identity
{
    /// <summary>
    /// 实体类——用户信息
    /// </summary>
    [Description("认证-用户信息")]
    public class User : UserBase<int>
    {
        /// <summary>
        /// 获取或设置 是否冻结
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 用户扩展信息
        /// </summary>
        public virtual UserExtend Extend { get; set; }
    }
}