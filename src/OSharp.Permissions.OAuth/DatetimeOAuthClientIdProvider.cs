// -----------------------------------------------------------------------
//  <copyright file="DatetimeClientIdProvider.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-08 17:24</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Security.Models;
using OSharp.Utility.Extensions;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 使用时间的客户端编号生成器，精度取到毫秒
    /// </summary>
    public class DatetimeOAuthClientIdProvider : IOAuthClientIdProvider
    {
        #region Implementation of IClientIdProvider

        /// <summary>
        /// 生成类型类型的客户端编号
        /// </summary>
        /// <param name="type">客户端类型</param>
        /// <returns></returns>
        public virtual string CreateClientId(OAuthClientType type)
        {
            string token = type == OAuthClientType.WebSite ? "site" : type == OAuthClientType.Application ? "app" : "no";
            DateTime now = DateTime.Now;
            return string.Format("{0}-{1}", token, now.ToUniqueString(true));
        }

        #endregion
    }
}