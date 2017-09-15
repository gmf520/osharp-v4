// -----------------------------------------------------------------------
//  <copyright file="GuidClientSecretProvider.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-05 16:30</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Utility.Extensions;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 采用Guid的客户端字符串生成器
    /// </summary>
    public class GuidOAuthClientSecretProvider : IOAuthClientSecretProvider
    {
        #region Implementation of IClientSecretProvider

        /// <summary>
        /// 创建客户端密钥字符串
        /// </summary>
        /// <returns></returns>
        public string CreateSecret()
        {
            string id = Guid.NewGuid().ToString("N");
            return id.ToBase64String().TrimEnd('=').Replace("+", "").Replace("/", "");
        }

        #endregion
    }
}