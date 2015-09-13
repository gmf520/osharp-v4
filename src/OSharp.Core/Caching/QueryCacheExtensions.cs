// -----------------------------------------------------------------------
//  <copyright file="QueryExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-05 1:10</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using OSharp.Core.Data;
using OSharp.Core.Data.Extensions;
using OSharp.Core.Exceptions;
using OSharp.Core.Security;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;
using OSharp.Utility.Filter;


namespace OSharp.Core.Caching
{
    /// <summary>
    /// 查询缓存扩展辅助操作
    /// </summary>
    public static class QueryCacheExtensions
    {
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
        /// <returns>查询的分页结果</returns>
        public static PageResult<TResult> ToPageCache<TEntity, TResult>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            PageCondition pageCondition,
            Expression<Func<TEntity, TResult>> selector,
            int cacheSeconds = 60)
        {
            ICache cache = CacheManager.GetCacher(typeof(PageResult<TResult>));
            string key = GetKey(source, predicate, pageCondition, selector);
            PageResult<TResult> result = cache.Get<PageResult<TResult>>(key);
            if (result != null)
            {
                return result;
            }
            result = source.ToPage(predicate, pageCondition, selector);
            cache.Set(key, result, DateTime.Now.AddSeconds(cacheSeconds));
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
        /// <param name="function">缓存策略相关功能</param>
        /// <returns>查询的分页结果</returns>
        public static PageResult<TResult> ToPageCache<TEntity, TResult>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            PageCondition pageCondition,
            Expression<Func<TEntity, TResult>> selector,
            IFunction function)
        {
            ICache cache = CacheManager.GetCacher(typeof(PageResult<TResult>));
            string key = GetKey(source, predicate, pageCondition, selector);
            PageResult<TResult> result = cache.Get<PageResult<TResult>>(key);
            if (result != null)
            {
                return result;
            }
            result = source.ToPage(predicate, pageCondition, selector);
            cache.Set(key, result, function);
            return result;
        }

        /// <summary>
        /// 将结果转换为缓存的列表，如缓存存在，直接返回，否则从数据源查询，并存入缓存中再返回
        /// </summary>
        /// <typeparam name="TSource">源数据类型</typeparam>
        /// <param name="source">查询数据源</param>
        /// <param name="cacheSeconds">缓存的秒数</param>
        /// <returns>查询结果</returns>
        public static List<TSource> ToCacheList<TSource>(this IQueryable<TSource> source, int cacheSeconds = 60)
        {
            ICache cache = CacheManager.GetCacher<TSource>();
            string key = GetKey(source.Expression);
            List<TSource> result = cache.Get<List<TSource>>(key);
            if (result != null)
            {
                return result;
            }
            result = source.ToList();
            cache.Set(key, result, DateTime.Now.AddSeconds(cacheSeconds));
            return result;
        }

        /// <summary>
        /// 将结果转换为缓存的数组，如缓存存在，直接返回，否则从数据源查询，并存入缓存中再返回
        /// </summary>
        /// <typeparam name="TSource">源数据类型</typeparam>
        /// <param name="source">查询数据源</param>
        /// <param name="cacheSeconds">缓存的秒数</param>
        /// <returns>查询结果</returns>
        public static TSource[] ToCacheArray<TSource>(this IQueryable<TSource> source, int cacheSeconds = 60)
        {
            ICache cache = CacheManager.GetCacher<TSource>();
            string key = GetKey(source.Expression);
            TSource[] result = cache.Get<TSource[]>(key);
            if (result != null)
            {
                return result;
            }
            result = source.ToArray();
            cache.Set(key, result, DateTime.Now.AddSeconds(cacheSeconds));
            return result;
        }

        /// <summary>
        /// 将结果转换为缓存的列表，如缓存存在，直接返回，否则从数据源查询，并按指定缓存策略存入缓存中再返回
        /// </summary>
        /// <typeparam name="TSource">源数据类型</typeparam>
        /// <param name="source">查询数据源</param>
        /// <param name="function">缓存策略相关功能</param>
        /// <returns>查询结果</returns>
        public static List<TSource> ToCacheList<TSource>(this IQueryable<TSource> source, IFunction function)
        {
            if (function == null || function.CacheExpirationSeconds <= 0)
            {
                return source.ToList();
            }
            ICache cache = CacheManager.GetCacher<TSource>();
            string key = GetKey(source.Expression);
            List<TSource> result = cache.Get<List<TSource>>(key);
            if (result != null)
            {
                return result;
            }
            result = source.ToList();
            cache.Set(key, result, function);
            return result;
        }

        /// <summary>
        /// 将结果转换为缓存的列表，如缓存存在，直接返回，否则从数据源查询，并按指定缓存策略存入缓存中再返回
        /// </summary>
        /// <typeparam name="TSource">源数据类型</typeparam>
        /// <param name="source">查询数据源</param>
        /// <param name="function">缓存策略相关功能</param>
        /// <returns>查询结果</returns>
        public static TSource[] ToCacheArray<TSource>(this IQueryable<TSource> source, IFunction function)
        {
            if (function == null || function.CacheExpirationSeconds <= 0)
            {
                return source.ToArray();
            }
            ICache cache = CacheManager.GetCacher<TSource>();
            string key = GetKey(source.Expression);
            TSource[] result = cache.Get<TSource[]>(key);
            if (result != null)
            {
                return result;
            }
            result = source.ToArray();
            cache.Set(key, result, function);
            return result;
        }

        private static string GetKey<TEntity, TResult>(IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            PageCondition pageCondition,
            Expression<Func<TEntity, TResult>> selector)
        {
            if (!typeof(TEntity).IsEntityType())
            {
                throw new InvalidOperationException("类型“{0}”不是实体类型".FormatWith(typeof(TEntity).FullName));
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
            return GetKey(query.Expression);
        }

        private static string GetKey(Expression expression)
        {
            return expression.ToString().ToMd5Hash();
        }
    }
}