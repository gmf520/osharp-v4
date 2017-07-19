// -----------------------------------------------------------------------
//  <copyright file="InternalCacher.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-22 23:46</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using OSharp.Core.Properties;
using OSharp.Utility.Logging;


namespace OSharp.Core.Caching
{
    /// <summary>
    /// 缓存执行者
    /// </summary>
    internal sealed class InternalCacher : ICache
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(InternalCacher));
        private readonly ICollection<ICache> _caches;

        /// <summary>
        /// 初始化一个<see cref="InternalCacher"/>类型的新实例
        /// </summary>
        public InternalCacher(string region)
        {
            _caches = CacheManager.Providers.Where(m => m != null).Select(m => m.GetCache(region)).ToList();
            if (_caches.Count == 0)
            {
                Logger.Warn(Resources.Caching_CacheNotInitialized);
            }
        }

        #region Implementation of ICache

        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>获取的数据</returns>
        public object Get(string key)
        {
            object value = null;
            foreach (ICache cache in _caches)
            {
                value = cache.Get(key);
                if (value != null)
                {
                    break;
                }
            }
            return value;
        }

        /// <summary>
        /// 从缓存中获取强类型数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns>获取的强类型数据</returns>
        public T Get<T>(string key)
        {
            object value = Get(key);
            if (value == null)
            {
                return default(T);
            }
            if (value is T)
            {
                return (T)value;
            }
            return default(T);
        }

        /// <summary>
        /// 获取当前缓存对象中的所有数据
        /// </summary>
        /// <returns>所有数据的集合</returns>
        public IEnumerable<object> GetAll()
        {
            List<object> values = new List<object>();
            foreach (ICache cache in _caches)
            {
                values = cache.GetAll().ToList();
                if (values.Count != 0)
                {
                    break;
                }
            }
            return values;
        }

        /// <summary>
        /// 获取当前缓存中的所有数据
        /// </summary>
        /// <typeparam name="T">项数据类型</typeparam>
        /// <returns>所有数据的集合</returns>
        public IEnumerable<T> GetAll<T>()
        {
            List<T> values = new List<T>();
            foreach (ICache cache in _caches)
            {
                values = cache.GetAll<T>().ToList();
                if (values.Count != 0)
                {
                    break;
                }
            }
            return values;
        }

        /// <summary>
        /// 使用默认配置添加或替换缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        public void Set(string key, object value)
        {
            foreach (ICache cache in _caches)
            {
                cache.Set(key, value);
            }
        }

        /// <summary>
        /// 添加或替换缓存项并设置绝对过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        /// <param name="absoluteExpiration">绝对过期时间，过了这个时间点，缓存即过期</param>
        public void Set(string key, object value, DateTime absoluteExpiration)
        {
            foreach (ICache cach in _caches)
            {
                cach.Set(key, value, absoluteExpiration);
            }
        }

        /// <summary>
        /// 添加或替换缓存项并设置相对过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        /// <param name="slidingExpiration">滑动过期时间，在此时间内访问缓存，缓存将继续有效</param>
        public void Set(string key, object value, TimeSpan slidingExpiration)
        {
            foreach (ICache cach in _caches)
            {
                cach.Set(key, value, slidingExpiration);
            }
        }

        /// <summary>
        /// 移除指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        public void Remove(string key)
        {
            foreach (ICache cach in _caches)
            {
                cach.Remove(key);
            }
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Clear()
        {
            foreach (ICache cach in _caches)
            {
                cach.Clear();
            }
        }

        #endregion
    }
}