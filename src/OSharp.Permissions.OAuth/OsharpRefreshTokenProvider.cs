// -----------------------------------------------------------------------
//  <copyright file="OsharpRefreshTokenProvider.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-05 18:00</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Owin.Security.Infrastructure;


namespace OSharp.Core.Security
{
    /// <summary>
    /// Osharp-RefreshToken提供者
    /// </summary>
    public class OsharpRefreshTokenProvider : AuthenticationTokenProvider
    {
        private readonly ConcurrentDictionary<string, string> _refreshTokens = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 创建RefreshToken
        /// </summary>
        /// <param name="context"></param>
        public override void Create(AuthenticationTokenCreateContext context)
        {
            string token = Guid.NewGuid().ToString("N");
            DateTime now = DateTime.UtcNow;
            context.Ticket.Properties.IssuedUtc = now;
            context.Ticket.Properties.ExpiresUtc = now.AddDays(60);
            _refreshTokens[token] = context.SerializeTicket();
            context.SetToken(token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            string token;
            if (_refreshTokens.TryRemove(context.Token, out token))
            {
                context.DeserializeTicket(token);
            }
        }
    }
}