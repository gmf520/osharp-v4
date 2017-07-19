// -----------------------------------------------------------------------
//  <copyright file="ClaimsIdentityExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-21 18:08</last-date>
// -----------------------------------------------------------------------

using System.Linq;
using System.Security.Claims;


namespace OSharp.Core.Extensions
{
    /// <summary>
    /// <see cref="ClaimsIdentity"/>扩展操作类
    /// </summary>
    public static class ClaimsIdentityExtensions
    {
        /// <summary>
        /// 获取指定类型的Claim值
        /// </summary>
        public static string GetClaimValueFirstOrDefault(this ClaimsIdentity identity, string type)
        {
            Claim claim = identity.Claims.FirstOrDefault(m => m.Type == type);
            return claim == null ? null : claim.Value;
        }

        /// <summary>
        /// 获取指定类型的所有Claim值
        /// </summary>
        public static string[] GetClaimValues(this ClaimsIdentity identity, string type)
        {
            return identity.Claims.Where(m => m.Type == type).Select(m => m.Value).ToArray();
        }
    }
}