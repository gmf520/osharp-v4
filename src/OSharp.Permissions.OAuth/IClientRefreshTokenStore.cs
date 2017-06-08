// -----------------------------------------------------------------------
//  <copyright file="IClientRefreshTokenStore.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-10 2:52</last-date>
// -----------------------------------------------------------------------

using System.Threading.Tasks;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 定义客户端刷新Token存储功能
    /// </summary>
    public interface IOAuthClientRefreshTokenStore
    {
        /// <summary>
        /// 获取刷新Token
        /// </summary>
        /// <param name="value">token值</param>
        /// <returns></returns>
        Task<RefreshTokenInfo> GetTokenInfo(string value);

        /// <summary>
        /// 保存刷新Token信息
        /// </summary>
        /// <param name="tokenInfo">Token信息</param>
        /// <returns></returns>
        Task<bool> SaveToken(RefreshTokenInfo tokenInfo);

        /// <summary>
        /// 移除刷新Token
        /// </summary>
        /// <param name="value">Token值</param>
        /// <returns></returns>
        Task<bool> Remove(string value);
    }
}