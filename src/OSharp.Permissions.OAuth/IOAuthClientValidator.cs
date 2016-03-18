// -----------------------------------------------------------------------
//  <copyright file="IClientValidator.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-04 23:39</last-date>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using OSharp.Core.Dependency;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 定义客户端密钥验证器
    /// </summary>
    public interface IOAuthClientValidator : IScopeDependency
    {
        /// <summary>
        /// 验证客户端编号与客户端密钥有效性
        /// </summary>
        /// <param name="clientId">客户端编号</param>
        /// <param name="clientSecret">客户端密钥</param>
        /// <returns>是否验证通过</returns>
        Task<bool> Validate(string clientId, string clientSecret);

        /// <summary>
        /// 获取指定客户端的重定向地址
        /// </summary>
        /// <param name="clientId">客户端编号</param>
        /// <returns></returns>
        Task<string> GetRedirectUrl(string clientId);
    }
}