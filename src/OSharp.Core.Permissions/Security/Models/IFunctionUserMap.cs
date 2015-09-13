// -----------------------------------------------------------------------
//  <copyright file="IFunctionUserMap.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-03 19:15</last-date>
// -----------------------------------------------------------------------

using System;

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 定义功能用户映射信息
    /// </summary>
    /// <typeparam name="TKey">映射编号类型</typeparam>
    /// <typeparam name="TFunction">功能类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    public interface IFunctionUserMap<TKey, TFunction, TFunctionKey, TUser, TUserKey> : IEntity<TKey>
        where TFunction : IFunction, IEntity<TFunctionKey>
        where TUser : IUser<TUserKey>, IEntity<TUserKey>
    {
        /// <summary>
        /// 获取或设置 功能信息
        /// </summary>
        TFunction Function { get; set; }

        /// <summary>
        /// 获取或设置 用户信息
        /// </summary>
        TUser User { get; set; }

        /// <summary>
        /// 获取或设置 限制类型
        /// </summary>
        FilterType FilterType { get; set; }

        /// <summary>
        /// 获取或设置 生效时间
        /// </summary>
        DateTime BeginTime { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        DateTime? EndTime { get; set; }
    }
}