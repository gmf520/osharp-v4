// -----------------------------------------------------------------------
//  <copyright file="RuntimeMemoryCacheProvider.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-22 21:18</last-date>
// -----------------------------------------------------------------------

using System.Collections.Concurrent;


namespace OSharp.Core.Caching
{
    /// <summary>
    /// 运行时内存缓存提供程序
    /// </summary>
    public class RuntimeMemoryCacheProvider : ICacheProvider
    {
        private static readonly ConcurrentDictionary<string, ICache> Caches;

        static RuntimeMemoryCacheProvider()
        {
            Caches = new ConcurrentDictionary<string, ICache>();
        }

        /// <summary>
        /// 获取 缓存是否可用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="regionName">缓存区域名称</param>
        /// <returns></returns>
        public ICache GetCache(string regionName)
        {
            ICache cache;
            if (Caches.TryGetValue(regionName, out cache))
            {
                return cache;
            }
            cache = new RuntimeMemoryCache(regionName);
            Caches[regionName] = cache;
            return cache;
        }

    }
}