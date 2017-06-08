// -----------------------------------------------------------------------
//  <copyright file="IClientIdProvider.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-05 14:56</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Security.Models;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 定义客户端编号生成器
    /// </summary>
    public interface IOAuthClientIdProvider
    {
        /// <summary>
        /// 生成类型类型的客户端编号
        /// </summary>
        /// <param name="type">客户端类型</param>
        /// <returns></returns>
        string CreateClientId(OAuthClientType type);
    }
}