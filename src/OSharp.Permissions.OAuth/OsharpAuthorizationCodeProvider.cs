// -----------------------------------------------------------------------
//  <copyright file="OsharpAuthorizationCodeProvider.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-05 17:54</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Concurrent;

using Microsoft.Owin.Security.Infrastructure;


namespace OSharp.Core.Security
{
    /// <summary>
    /// osharp-认证码验证提供者
    /// </summary>
    public class OsharpAuthorizationCodeProvider : AuthenticationTokenProvider, IAuthorizationCodeProvider
    {
        private readonly ConcurrentDictionary<string, string> _codes = new ConcurrentDictionary<string, string>();

        public override void Create(AuthenticationTokenCreateContext context)
        {
            context.SetToken(Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N"));
            _codes[context.Token] = context.SerializeTicket();
        }

        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            string value;
            if (_codes.TryRemove(context.Token, out value))
            {
                context.DeserializeTicket(value);
            }
        }

    }
}