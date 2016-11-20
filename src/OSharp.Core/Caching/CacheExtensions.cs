// -----------------------------------------------------------------------
//  <copyright file="CacheExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-06 12:25</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using OSharp.Core.Data.Extensions;
using OSharp.Core.Properties;
using OSharp.Core.Security;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;
using OSharp.Utility.Filter;


namespace OSharp.Core.Caching
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

        /// <summary>
        /// 从缓存中获取数据，如不存在则从指定委托中获取，并回存到缓存中再返回
        /// </summary>
        /// <typeparam name="TResult">返回结果类型</typeparam>
        /// <param name="cache">缓存对象</param>
        /// <param name="key">缓存键</param>
        /// <param name="getFunc">外部获取数据委托</param>
        /// <param name="cacheSeconds">缓存时间，秒</param>
        /// <returns>结果数据</returns>
        public static TResult Get<TResult>(this ICache cache, string key, Func<TResult> getFunc, int cacheSeconds)
        {
            TResult result = cache.Get<TResult>(key);
            if (result != null)
            {
                return result;
            }
            result = getFunc();
            cache.Set(key, result, DateTime.Now.AddSeconds(cacheSeconds));
            return result;
        }

        /// <summary>
        /// 从缓存中获取数据，如不存在则从指定委托中获取，并回存到缓存中再返回
        /// </summary>
        /// <typeparam name="TResult">返回结果类型</typeparam>
        /// <param name="cache">缓存对象</param>
        /// <param name="key">缓存键</param>
        /// <param name="getFunc">外部获取数据委托</param>
        /// <param name="function">缓存策略相关功能</param>
        /// <returns>结果数据</returns>
        public static TResult Get<TResult>(this ICache cache, string key, Func<TResult> getFunc, IFunction function)
        {
            TResult result = cache.Get<TResult>(key);
            if (result != null)
            {
                return result;
            }
            result = getFunc();
            cache.Set(key, result, function);
            return result;
        }

        /// <summary>
        /// 查询分页数据结果，如缓存存在，直接返回，否则从数据源查找分页结果，并存入缓存中再返回
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TResult">分页数据类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageCondition">分页查询条件</param>
        /// <param name="selector">数据筛选表达式</param>
        /// <param name="cacheSeconds">缓存的秒数</param>
        /// <param name="keyParams">缓存键参数</param>
        /// <returns>查询的分页结果</returns>
        public static PageResult<TResult> ToPageCache<TEntity, TResult>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            PageCondition pageCondition,
            Expression<Func<TEntity, TResult>> selector,
            int cacheSeconds = 60, params object[] keyParams)
        {
            ICache cache = CacheManager.GetCacher<TEntity>();
            string key = GetKey(source, predicate, pageCondition, selector, keyParams);
            return cache.Get(key, () => source.ToPage(predicate, pageCondition, selector), cacheSeconds);
        }

        /// <summary>
        /// 查询分页数据结果，如缓存存在，直接返回，否则从数据源查找分页结果，并存入缓存中再返回
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TResult">分页数据类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageCondition">分页查询条件</param>
        /// <param name="selector">数据筛选表达式</param>
        /// <param name="function">缓存策略相关功能</param>
        /// <param name="keyParams">缓存键参数</param>
        /// <returns>查询的分页结果</returns>
        public static PageResult<TResult> ToPageCache<TEntity, TResult>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            PageCondition pageCondition,
            Expression<Func<TEntity, TResult>> selector,
            IFunction function, params object[] keyParams)
        {
            ICache cache = CacheManager.GetCacher<TEntity>();
            string key = GetKey(source, predicate, pageCondition, selector, keyParams);
            return cache.Get(key, () => source.ToPage(predicate, pageCondition, selector), function);
        }

        /// <summary>
        /// 将结果转换为缓存的列表，如缓存存在，直接返回，否则从数据源查询，并存入缓存中再返回
        /// </summary>
        /// <typeparam name="TSource">源数据类型</typeparam>
        /// <param name="source">查询数据源</param>
        /// <param name="cacheSeconds">缓存的秒数</param>
        /// <param name="keyParams">缓存键参数</param>
        /// <returns>查询结果</returns>
        public static List<TSource> ToCacheList<TSource>(this IQueryable<TSource> source, int cacheSeconds = 60, params object[] keyParams)
        {
            ICache cache = CacheManager.GetCacher<TSource>();
            string key = GetKey(source.Expression, keyParams);
            return cache.Get(key, source.ToList, cacheSeconds);
        }

        /// <summary>
        /// 将结果转换为缓存的数组，如缓存存在，直接返回，否则从数据源查询，并存入缓存中再返回
        /// </summary>
        /// <typeparam name="TSource">源数据类型</typeparam>
        /// <param name="source">查询数据源</param>
        /// <param name="cacheSeconds">缓存的秒数</param>
        /// <param name="keyParams">缓存键参数</param>
        /// <returns>查询结果</returns>
        public static TSource[] ToCacheArray<TSource>(this IQueryable<TSource> source, int cacheSeconds = 60, params object[] keyParams)
        {
            ICache cache = CacheManager.GetCacher<TSource>();
            string key = GetKey(source.Expression, keyParams);
            return cache.Get(key, source.ToArray, cacheSeconds);
        }

        /// <summary>
        /// 将结果转换为缓存的列表，如缓存存在，直接返回，否则从数据源查询，并按指定缓存策略存入缓存中再返回
        /// </summary>
        /// <typeparam name="TSource">源数据类型</typeparam>
        /// <param name="source">查询数据源</param>
        /// <param name="function">缓存策略相关功能</param>
        /// <param name="keyParams">缓存键参数</param>
        /// <returns>查询结果</returns>
        public static List<TSource> ToCacheList<TSource>(this IQueryable<TSource> source, IFunction function, params object[] keyParams)
        {
            if (function == null || function.CacheExpirationSeconds <= 0)
            {
                return source.ToList();
            }
            ICache cache = CacheManager.GetCacher<TSource>();
            string key = GetKey(source.Expression, keyParams);
            return cache.Get(key, source.ToList, function);
        }

        /// <summary>
        /// 将结果转换为缓存的列表，如缓存存在，直接返回，否则从数据源查询，并按指定缓存策略存入缓存中再返回
        /// </summary>
        /// <typeparam name="TSource">源数据类型</typeparam>
        /// <param name="source">查询数据源</param>
        /// <param name="function">缓存策略相关功能</param>
        /// <param name="keyParams">缓存键参数</param>
        /// <returns>查询结果</returns>
        public static TSource[] ToCacheArray<TSource>(this IQueryable<TSource> source, IFunction function, params object[] keyParams)
        {
            if (function == null || function.CacheExpirationSeconds <= 0)
            {
                return source.ToArray();
            }
            ICache cache = CacheManager.GetCacher<TSource>();
            string key = GetKey(source.Expression, keyParams);
            return cache.Get(key, source.ToArray, function);
        }

        private static string GetKey<TEntity, TResult>(IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            PageCondition pageCondition,
            Expression<Func<TEntity, TResult>> selector, params object[] keyParams)
        {
            if (!typeof(TEntity).IsEntityType())
            {
                throw new InvalidOperationException(Resources.QueryCacheExtensions_TypeNotEntityType.FormatWith(typeof(TEntity).FullName));
            }

            source = source.Where(predicate);
            SortCondition[] sortConditions = pageCondition.SortConditions;
            if (sortConditions == null || sortConditions.Length == 0)
            {
                source = source.OrderBy("Id");
            }
            else
            {
                int count = 0;
                IOrderedQueryable<TEntity> orderSource = null;
                foreach (SortCondition sortCondition in sortConditions)
                {
                    orderSource = count == 0
                        ? CollectionPropertySorter<TEntity>.OrderBy(source, sortCondition.SortField, sortCondition.ListSortDirection)
                        : CollectionPropertySorter<TEntity>.ThenBy(orderSource, sortCondition.SortField, sortCondition.ListSortDirection);
                    count++;
                }
                source = orderSource;
            }
            int pageIndex = pageCondition.PageIndex, pageSize = pageCondition.PageSize;
            source = source != null
                ? source.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                : Enumerable.Empty<TEntity>().AsQueryable();
            IQueryable<TResult> query = source.Select(selector);
            return GetKey(query.Expression, keyParams);
        }

        private static string GetKey(Expression expression, params object[] keyParams)
        {
            string key;
            try
            {
                key = new ExpressionCacheKeyGenerator(expression).GetKey(keyParams);
            }
            catch (TargetInvocationException)
            {
                key = new StringCacheKeyGenerator().GetKey(keyParams);
            }
            return key.ToMd5Hash();
        }
    }
}