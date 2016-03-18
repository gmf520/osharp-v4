// -----------------------------------------------------------------------
//  <copyright file="ClientType.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-05 14:32</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 表示客户端类型的枚举
    /// </summary>
    public enum OAuthClientType
    {
        /// <summary>
        /// 网站
        /// </summary>
        WebSite = 1,
        /// <summary>
        /// 桌面/移动 客户端程序
        /// </summary>
        Application = 2
    }
}