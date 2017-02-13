// -----------------------------------------------------------------------
//  <copyright file="IClientClaim.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-01 2:05</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Data;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 定义客户端摘要信息
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IOAuthClientClaim<out TKey> : IEntity<TKey>
    {
        /// <summary>
        /// 获取或设置 摘要类型
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// 获取或设置 摘要值
        /// </summary>
        string Value { get; set; }
    }
}