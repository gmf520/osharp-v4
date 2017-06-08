// -----------------------------------------------------------------------
//  <copyright file="PlatformToken.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-25 11:54</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Core.Security
{
    /// <summary>
    /// 技术平台标识
    /// </summary>
    public enum PlatformToken
    {
        /// <summary>
        /// 标识当前平台为ASP.NET MVC
        /// </summary>
        Mvc,

        /// <summary>
        /// 标识当前平台为ASP.NET WebAPI
        /// </summary>
        WebApi,

        /// <summary>
        /// 标识当前平台为ASP.NET SignalR
        /// </summary>
        SignalR,

        /// <summary>
        /// 标识当前平台为本地程序
        /// </summary>
        Local
    }
}