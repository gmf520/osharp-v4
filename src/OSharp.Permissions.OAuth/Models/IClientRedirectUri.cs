// -----------------------------------------------------------------------
//  <copyright file="IClientRedirectUri.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-31 17:01</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 定义客户端重定向地址信息
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IClientRedirectUri<out TKey> : IEntity<TKey>
    {
        /// <summary>
        /// 获取或设置 重定向地址
        /// </summary>
        string Url { get; set; }
    }
}