// -----------------------------------------------------------------------
//  <copyright file="CacheBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-22 21:28</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;


namespace OSharp.Core.Caching
{
    /// <summary>
    /// 缓存基类
    /// </summary>
    public abstract class CacheBase : ICache
    {
        /// <summary>
        /// 获取 缓存区域名称，可作为缓存键标识，给缓存管理带来便利
        /// </summary>
        public abstract string Region { get; }

        #region Implementation of ICache

        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>获取的数据</returns>
        public abstract object Get(string key);

        /// <summary>
        /// 从缓存中获取强类型数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns>获取的强类型数据</returns>
        public abstract T Get<T>(string key);

        /// <summary>
        /// 获取当前缓存对象中的所有数据
        /// </summary>
        /// <returns>所有数据的集合</returns>
        public abstract IEnumerable<object> GetAll();

        /// <summary>
        /// 获取当前缓存中的所有数据
        /// </summary>
        /// <typeparam name="T">项数据类型</typeparam>
        /// <returns>所有数据的集合</returns>
        public abstract IEnumerable<T> GetAll<T>();

        /// <summary>
        /// 使用默认配置添加或替换缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        public abstract void Set(string key, object value);

        /// <summary>
        /// 添加或替换缓存项并设置绝对过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        /// <param name="absoluteExpiration">绝对过期时间，过了这个时间点，缓存即过期</param>
        public abstract void Set(string key, object value, DateTime absoluteExpiration);

        /// <summary>
        /// 添加或替换缓存项并设置相对过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        /// <param name="slidingExpiration">滑动过期时间，在此时间内访问缓存，缓存将继续有效</param>
        public abstract void Set(string key, object value, TimeSpan slidingExpiration);

        /// <summary>
        /// 移除指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        public abstract void Remove(string key);

        /// <summary>
        /// 清空缓存
        /// </summary>
        public abstract void Clear();

        #endregion
    }
}