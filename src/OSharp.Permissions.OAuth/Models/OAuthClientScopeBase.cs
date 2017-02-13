// -----------------------------------------------------------------------
//  <copyright file="ClientScopeBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-31 17:12</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Data;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 客户端作用域信息
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TClient">客户端信息类型</typeparam>
    /// <typeparam name="TClientKey">客户端主键类型</typeparam>
    public abstract class OAuthClientScopeBase<TKey, TClient, TClientKey> : EntityBase<TKey>, IOAuthClientScope<TKey>
        where TClient : IOAuthClient<TClientKey>
    {
        /// <summary>
        /// 获取或设置 作用域
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// 获取或设置 所属客户端信息
        /// </summary>
        public virtual TClient Client { get; set; }
    }
}