// -----------------------------------------------------------------------
//  <copyright file="ClientRefreshTokenStore.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-10 5:02</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Security;
using OSharp.Demo.Models.Identity;
using OSharp.Demo.Models.OAuth;


namespace OSharp.Demo.OAuth
{
    /// <summary>
    /// 客户端刷新Token存储
    /// </summary>
    public class ClientRefreshTokenStore : ClientRefreshTokenStoreBase<ClientRefreshToken, Guid, Client, int, User, int>
    { }
}