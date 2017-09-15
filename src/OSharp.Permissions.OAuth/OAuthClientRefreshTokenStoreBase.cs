// -----------------------------------------------------------------------
//  <copyright file="ClientRefreshTokenStoreBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-10 3:03</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;

using OSharp.Core.Data;
using OSharp.Core.Dependency;
using OSharp.Core.Identity.Models;
using OSharp.Core.Security.Models;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 刷新Token存储基类
    /// </summary>
    public abstract class OAuthClientRefreshTokenStoreBase<TClientRefreshToken, TClientRefreshTokenKey, TClient, TClientKey, TUser, TUserKey>
        : IOAuthClientRefreshTokenStore, IScopeDependency
        where TClientRefreshToken : OAuthClientRefreshTokenBase<TClientRefreshTokenKey, TClient, TClientKey, TUser, TUserKey>, new()
        where TClient : IOAuthClient<TClientKey>
        where TUser : UserBase<TUserKey>
        where TClientRefreshTokenKey : IEquatable<TClientRefreshTokenKey>
        where TClientKey : IEquatable<TClientKey>
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// 获取或设置 客户端刷新Token仓储对象
        /// </summary>
        public IRepository<TClientRefreshToken, TClientRefreshTokenKey> ClientRefreshTokenRepository { get; set; }

        /// <summary>
        /// 获取或设置 客户端仓储对象
        /// </summary>
        public IRepository<TClient, TClientKey> ClientRepository { get; set; }

        /// <summary>
        /// 获取或设置 用户仓储对象
        /// </summary>
        public IRepository<TUser, TUserKey> UserRepository { get; set; }

        #region Implementation of IClientRefreshTokenStore

        /// <summary>
        /// 获取刷新Token
        /// </summary>
        /// <param name="value">token值</param>
        /// <returns></returns>
        public virtual Task<RefreshTokenInfo> GetTokenInfo(string value)
        {
            var tokenInfo = ClientRefreshTokenRepository.Entities.Where(m => m.Value == value).Select(m => new RefreshTokenInfo()
            {
                Value = m.Value,
                IssuedUtc = m.IssuedUtc,
                ExpiresUtc = m.ExpiresUtc,
                ProtectedTicket = m.ProtectedTicket,
                ClientId = m.Client.ClientId,
                UserName = m.User.UserName
            }).FirstOrDefault();
            return Task.FromResult(tokenInfo);
        }

        /// <summary>
        /// 保存刷新Token信息
        /// </summary>
        /// <param name="tokenInfo">Token信息</param>
        /// <returns></returns>
        public async virtual Task<bool> SaveToken(RefreshTokenInfo tokenInfo)
        {
            TClientRefreshToken token = new TClientRefreshToken()
            {
                Value = tokenInfo.Value,
                ProtectedTicket = tokenInfo.ProtectedTicket,
                IssuedUtc = tokenInfo.IssuedUtc,
                ExpiresUtc = tokenInfo.ExpiresUtc
            };
            TClient client = ClientRepository.TrackEntities.Where(m => m.ClientId == tokenInfo.ClientId).FirstOrDefault();
            if (client == null)
            {
                return false;
            }
            token.Client = client;
            TUser user = UserRepository.TrackEntities.Where(m => m.UserName == tokenInfo.UserName).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            token.User = user;
            int result = await ClientRefreshTokenRepository.InsertAsync(token);
            return result > 0;
        }

        /// <summary>
        /// 移除刷新Token
        /// </summary>
        /// <param name="value">Token值</param>
        /// <returns></returns>
        public async virtual Task<bool> Remove(string value)
        {
            var token = ClientRefreshTokenRepository.Entities.Where(m => m.Value == value)
                .Select(m => new { UserId = m.User.Id }).FirstOrDefault();
            if (token == null)
            {
                return false;
            }
            TUserKey userId = token.UserId;
            int result = await ClientRefreshTokenRepository.DeleteDirectAsync(m => m.User.Id.Equals(userId));
            return result > 0;
        }

        #endregion
    }
}