// -----------------------------------------------------------------------
//  <copyright file="CacheManager.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-22 14:59</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Concurrent;

using OSharp.Utility;


namespace OSharp.Core.Caching
{
    /// <summary>
    /// 缓存操作管理器
    /// </summary>
    public static class CacheManager
    {
        private static readonly ConcurrentDictionary<string, ICache> Cachers;
        //两级缓存
        internal static readonly ICacheProvider[] Providers = new ICacheProvider[2];

        static CacheManager()
        {
            Cachers = new ConcurrentDictionary<string, ICache>();
        }

        /// <summary>
        /// 设置缓存提供者
        /// </summary>
        /// <param name="provider">缓存提供者</param>
        /// <param name="level">缓存级别</param>
        public static void SetProvider(ICacheProvider provider, CacheLevel level)
        {
            provider.CheckNotNull("provider");
            switch (level)
            {
                case CacheLevel.First:
                    Providers[0] = provider;
                    break;
                case CacheLevel.Second:
                    Providers[1] = provider;
                    break;
            }
        }

        /// <summary>
        /// 移除指定级别的缓存提供者
        /// </summary>
        /// <param name="level">缓存级别</param>
        public static void RemoveProvider(CacheLevel level)
        {
            switch (level)
            {
                case CacheLevel.First:
                    Providers[0] = null;
                    break;
                case CacheLevel.Second:
                    Providers[1] = null;
                    break;
            }
        }

        /// <summary>
        /// 获取指定区域的缓存执行者实例
        /// </summary>
        public static ICache GetCacher(string region)
        {
            region.CheckNotNullOrEmpty("region");
            ICache cache;
            if (Cachers.TryGetValue(region, out cache))
            {
                return cache;
            }
            cache = new InternalCacher(region);
            Cachers[region] = cache;
            return cache;
        }

        /// <summary>
        /// 获取指定类型的缓存执行者实例
        /// </summary>
        /// <param name="type">类型实例</param>
        public static ICache GetCacher(Type type)
        {
            type.CheckNotNull("type");
            return GetCacher(type.FullName);
        }

        /// <summary>
        /// 获取指定类型的缓存执行者实例
        /// </summary>
        public static ICache GetCacher<T>()
        {
            return GetCacher(typeof(T));
        }
    }
}