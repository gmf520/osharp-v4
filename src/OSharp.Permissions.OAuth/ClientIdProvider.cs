// -----------------------------------------------------------------------
//  <copyright file="ClientIdProvider.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-05 14:56</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Security.Models;
using OSharp.Utility.Extensions;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 使用时间的客户端编号生成器，由于添加客户端是低频业务，不考虑毫秒级的重复
    /// </summary>
    public class DatetimeClientIdProvider : IClientIdProvider
    {
        #region Implementation of IClientIdProvider

        /// <summary>
        /// 生成类型类型的客户端编号
        /// </summary>
        /// <param name="type">客户端类型</param>
        /// <returns></returns>
        public virtual string CreateClientId(ClientType type)
        {
            string token = type == ClientType.WebSite ? "site" : type == ClientType.Application ? "app" : "no";
            DateTime now = DateTime.Now;
            return string.Format("{0}-{1}", token, now.ToUniqueString(true));
        }

        #endregion
    }
}