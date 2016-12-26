// -----------------------------------------------------------------------
//  <copyright file="IClientScope.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-31 17:04</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Data;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 定义客户端作用域信息
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IOAuthClientScope<out TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 获取或设置 作用域
        /// </summary>
        string Scope { get; set; }
    }
}