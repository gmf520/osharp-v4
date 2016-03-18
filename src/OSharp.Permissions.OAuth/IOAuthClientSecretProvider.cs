// -----------------------------------------------------------------------
//  <copyright file="IClientSecretProvider.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-05 15:55</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Core.Security
{
    /// <summary>
    /// 定义客户端密钥生成器
    /// </summary>
    public interface IOAuthClientSecretProvider
    {
        /// <summary>
        /// 创建客户端密钥字符串
        /// </summary>
        /// <returns></returns>
        string CreateSecret();
    }
}