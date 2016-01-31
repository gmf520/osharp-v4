// -----------------------------------------------------------------------
//  <copyright file="RuntimeMemoryCache.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-22 21:17</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

using OSharp.Utility;


namespace OSharp.Core.Caching
{
    /// <summary>
    /// 运行时内存缓存
    /// </summary>
    public class RuntimeMemoryCache : CacheBase
    {
        private readonly string _region;
        private readonly ObjectCache _cache;

        /// <summary>
        /// 初始化一个<see cref="RuntimeMemoryCache"/>类型的新实例
        /// </summary>
        public RuntimeMemoryCache(string region)
        {
            _region = region;
            _cache = MemoryCache.Default;
        }

        /// <summary>
        /// 获取 缓存区域名称，可作为缓存键标识，给缓存管理带来便利
        /// </summary>
        public override string Region
        {
            get { return _region; }
        }

        #region Implementation of ICache

        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>获取的数据</returns>
        public override object Get(string key)
        {
            key.CheckNotNull("key" );
            string cacheKey = GetCacheKey(key);
            object value = _cache.Get(cacheKey);
            if (value == null)
            {
                return null;
            }
            DictionaryEntry entry = (DictionaryEntry)value;
            if (!key.Equals(entry.Key))
            {
                return null;
            }
            return entry.Value;
        }

        /// <summary>
        /// 从缓存中获取强类型数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns>获取的强类型数据</returns>
        public override T Get<T>(string key)
        {
            return (T)Get(key);
        }

        /// <summary>
        /// 获取当前缓存对象中的所有数据
        /// </summary>
        /// <returns>所有数据的集合</returns>
        public override IEnumerable<object> GetAll()
        {
            string token = string.Concat(_region, ":");
            return _cache.Where(m => m.Key.StartsWith(token)).Select(m => m.Value).Cast<DictionaryEntry>().Select(m => m.Value);
        }

        /// <summary>
        /// 获取当前缓存中的所有数据
        /// </summary>
        /// <typeparam name="T">项数据类型</typeparam>
        /// <returns>所有数据的集合</returns>
        public override IEnumerable<T> GetAll<T>()
        {
            return GetAll().Cast<T>();
        }

        /// <summary>
        /// 使用默认配置添加或替换缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        public override void Set(string key, object value)
        {
            key.CheckNotNull("key");
            value.CheckNotNull("value");
            string cacheKey = GetCacheKey(key);
            DictionaryEntry entry = new DictionaryEntry(key, value);
            CacheItemPolicy policy = new CacheItemPolicy();
            _cache.Set(cacheKey, entry, policy);
        }

        /// <summary>
        /// 添加或替换缓存项并设置绝对过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        /// <param name="absoluteExpiration">绝对过期时间，过了这个时间点，缓存即过期</param>
        public override void Set(string key, object value, DateTime absoluteExpiration)
        {
            key.CheckNotNull("key");
            value.CheckNotNull("value");
            string cacheKey = GetCacheKey(key);
            DictionaryEntry entry = new DictionaryEntry(key, value);
            CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = absoluteExpiration };
            _cache.Set(cacheKey, entry, policy);
        }

        /// <summary>
        /// 添加或替换缓存项并设置相对过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        /// <param name="slidingExpiration">滑动过期时间，在此时间内访问缓存，缓存将继续有效</param>
        public override void Set(string key, object value, TimeSpan slidingExpiration)
        {
            key.CheckNotNull("key");
            value.CheckNotNull("value");
            string cacheKey = GetCacheKey(key);
            DictionaryEntry entry = new DictionaryEntry(key, value);
            CacheItemPolicy policy = new CacheItemPolicy() { SlidingExpiration = slidingExpiration };
            _cache.Set(cacheKey, entry, policy);
        }

        /// <summary>
        /// 移除指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        public override void Remove(string key)
        {
            key.CheckNotNull("key");
            string cacheKey = GetCacheKey(key);
            _cache.Remove(cacheKey);
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public override void Clear()
        {
            string token = _region + ":";
            List<string> cacheKeys = _cache.Where(m => m.Key.StartsWith(token)).Select(m => m.Key).ToList();
            foreach (string cacheKey in cacheKeys)
            {
                _cache.Remove(cacheKey);
            }
        }

        #endregion

        #region 私有方法

        private string GetCacheKey(string key)
        {
            return string.Concat(_region, ":", key, "@", key.GetHashCode());
        }

        #endregion
    }
}