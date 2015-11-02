// -----------------------------------------------------------------------
//  <copyright file="CacheExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-21 18:08</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Caching;
using OSharp.Core.Security;
using OSharp.Utility;


namespace OSharp.Core.Extensions
{
    /// <summary>
    /// 缓存扩展辅助操作类
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// 根据功能配置添加缓存数据
        /// </summary>
        public static void Set(this ICache cache, string key, object value, IFunction function)
        {
            key.CheckNotNull("key");
            value.CheckNotNull("value");
            if (function == null || function.CacheExpirationSeconds <= 0)
            {
                return;
            }
            if (function.IsCacheSliding)
            {
                cache.Set(key, value, TimeSpan.FromSeconds(function.CacheExpirationSeconds));
            }
            else
            {
                cache.Set(key, value, DateTime.Now.AddSeconds(function.CacheExpirationSeconds));
            }
        }
    }
}