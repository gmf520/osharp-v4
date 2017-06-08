// -----------------------------------------------------------------------
//  <copyright file="ClientRefreshToken.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-10 5:00</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Security.Models;
using OSharp.Demo.Models.Identity;


namespace OSharp.Demo.Models.OAuth
{
    /// <summary>
    /// 实体类——客户端刷新Token
    /// </summary>
    public class OAuthClientRefreshToken : OAuthClientRefreshTokenBase<Guid, OAuthClient, int, User, int>
    { }
}