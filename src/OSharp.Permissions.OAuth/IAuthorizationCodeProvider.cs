// -----------------------------------------------------------------------
//  <copyright file="IAuthorizationCodeProvider.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-06 6:12</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Owin.Security.Infrastructure;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthorizationCodeProvider : IAuthenticationTokenProvider
    { }
}