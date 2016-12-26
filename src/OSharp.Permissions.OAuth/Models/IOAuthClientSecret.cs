// -----------------------------------------------------------------------
//  <copyright file="IClientSecret.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-01 1:55</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Data;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 定义客户端密钥信息
    /// </summary>
    public interface IOAuthClientSecret<out TKey> : IEntity<TKey>, ILockable, IExpirable
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 获取或设置 密钥值
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// 获取或设置 密钥类型
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// 获取或设置 描述
        /// </summary>
        string Remark { get; set; }
    }
}