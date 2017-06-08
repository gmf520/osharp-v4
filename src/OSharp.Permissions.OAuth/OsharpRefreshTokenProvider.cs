// -----------------------------------------------------------------------
//  <copyright file="OsharpRefreshTokenProvider.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-05 18:00</last-date>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using Microsoft.Owin.Security.Infrastructure;


namespace OSharp.Core.Security
{
    /// <summary>
    /// Osharp-RefreshToken提供者
    /// </summary>
    public class OsharpRefreshTokenProvider : AuthenticationTokenProvider, IRefreshTokenProvider
    {
        private readonly IOAuthClientRefreshTokenStore _clientRefreshTokenStore;

        /// <summary>
        /// 初始化一个<see cref="OsharpRefreshTokenProvider"/>类型的新实例
        /// </summary>
        public OsharpRefreshTokenProvider(IOAuthClientRefreshTokenStore clientRefreshTokenStore)
        {
            _clientRefreshTokenStore = clientRefreshTokenStore;
        }

        /// <summary>
        /// 创建RefreshToken，在客户端请求AccessToken的时候自动调用
        /// </summary>
        /// <param name="context"></param>
        public async override Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            string clientId = context.Ticket.Properties.Dictionary["as:client_id"];
            if (string.IsNullOrEmpty(clientId))
            {
                return;
            }
            
            DateTime now = DateTime.UtcNow;
            string userName = context.Ticket.Identity.Name;
            if (clientId == userName)
            {
                return;
            }
            RefreshTokenInfo tokenInfo = new RefreshTokenInfo()
            {
                Value = Guid.NewGuid().ToString("N"),
                IssuedUtc = now,
                ExpiresUtc = now.AddDays(30),
                UserName = userName,
                ClientId = clientId
            };
            context.Ticket.Properties.IssuedUtc = tokenInfo.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = tokenInfo.ExpiresUtc;
            tokenInfo.ProtectedTicket = context.SerializeTicket();
            if (await _clientRefreshTokenStore.SaveToken(tokenInfo))
            {
                context.SetToken(tokenInfo.Value);
            }
        }

        /// <summary>
        /// 移除RefreshToken，在客户端使用RefreshToken请求新的AccessToken的时候自动调用
        /// </summary>
        /// <param name="context"></param>
        public async override Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            RefreshTokenInfo token = await _clientRefreshTokenStore.GetTokenInfo(context.Token);
            if (token == null)
            {
                return;
            }
            context.DeserializeTicket(token.ProtectedTicket);
            await _clientRefreshTokenStore.Remove(context.Token);
        }
    }
}