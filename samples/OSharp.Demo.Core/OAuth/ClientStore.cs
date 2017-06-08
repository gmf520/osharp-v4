// -----------------------------------------------------------------------
//  <copyright file="ClientStore.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-05 19:04</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Security;
using OSharp.Demo.Dtos.OAuth;
using OSharp.Demo.Models.OAuth;


namespace OSharp.Demo.OAuth
{
    /// <summary>
    /// 客户端存储实现
    /// </summary>
    public class OAuthClientStore : OAuthClientStoreBase<OAuthClient, int, OAuthClientSecret, int, OAuthClientInputDto, OAuthClientSecretInputDto>
    { }
}