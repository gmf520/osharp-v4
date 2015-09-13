// -----------------------------------------------------------------------
//  <copyright file="OnlineType.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-15 10:54</last-date>
// -----------------------------------------------------------------------

using System;


namespace OSharp.Web.Security
{
    /// <summary>
    /// 表示在线类型的枚举
    /// </summary>
    [Serializable]
    public enum OnlineType
    {
        /// <summary>
        /// 网站在线类型
        /// </summary>
        Site,

        /// <summary>
        /// 客户端在线类型
        /// </summary>
        Client,

        ///// <summary>
        ///// 移动在线类型
        ///// </summary>
        //App
    }
}