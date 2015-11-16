﻿// -----------------------------------------------------------------------
//  <copyright file="ClientClaimBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-01 2:08</last-date>
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
    /// 客户端摘要标识信息基类
    /// </summary>
    public abstract class ClientClaimBase<TKey, TClient, TClientKey> : EntityBase<TKey>, IClientClaim<TKey>
        where TClient : IClient<TClientKey>
    {
        /// <summary>
        /// 获取或设置 摘要类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 获取或设置 摘要值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 获取或设置 所属客户端信息
        /// </summary>
        public virtual TClient Client { get; set; }
    }
}