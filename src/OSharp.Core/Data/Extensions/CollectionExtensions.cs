// -----------------------------------------------------------------------
//  <copyright file="CollectionExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-08-04 0:06</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;
using OSharp.Utility.Filter;


namespace OSharp.Core.Data.Extensions
{
    /// <summary>
    /// 集合扩展辅助操作类
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合中查询指定数据筛选的分页信息
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TResult">分页数据类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageCondition">分页查询条件</param>
        /// <param name="selector">数据筛选表达式</param>
        /// <returns>分页结果信息</returns>
        public static PageResult<TResult> ToPage<TEntity, TResult>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            PageCondition pageCondition,
            Expression<Func<TEntity, TResult>> selector)
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");
            pageCondition.CheckNotNull("pageCondition");
            selector.CheckNotNull("selector");
            return source.ToPage(predicate,
                pageCondition.PageIndex,
                pageCondition.PageSize,
                pageCondition.SortConditions,
                selector);
        }

        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合中查询指定数据筛选的分页信息
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TResult">分页数据类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="sortConditions">排序条件集合</param>
        /// <param name="selector">数据筛选表达式</param>
        /// <returns>分页结果信息</returns>
        public static PageResult<TResult> ToPage<TEntity, TResult>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            int pageIndex,
            int pageSize,
            SortCondition[] sortConditions,
            Expression<Func<TEntity, TResult>> selector)
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");
            pageIndex.CheckGreaterThan("pageIndex", 0);
            pageSize.CheckGreaterThan("pageSize", 0);
            selector.CheckNotNull("selector");

            int total;
            TResult[] data = source.Where(predicate, pageIndex, pageSize, out total, sortConditions).Select(selector).ToArray();
            return new PageResult<TResult>() { Total = total, Data = data };
        }

        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合 中查询指定分页条件的子数据集
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageCondition">分页查询条件</param>
        /// <param name="total">输出符合条件的总记录数</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            PageCondition pageCondition,
            out int total)
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");
            pageCondition.CheckNotNull("pageCondition");

            return source.Where(predicate, pageCondition.PageIndex, pageCondition.PageSize, out total, pageCondition.SortConditions);
        }

        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合 中查询指定分页条件的子数据集
        /// </summary>
        /// <typeparam name="TEntity">动态实体类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="total">输出符合条件的总记录数</param>
        /// <param name="sortConditions">排序条件集合</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            int pageIndex,
            int pageSize,
            out int total,
            SortCondition[] sortConditions = null)
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");
            pageIndex.CheckGreaterThan("pageIndex", 0);
            pageSize.CheckGreaterThan("pageSize", 0);

            if (!typeof(TEntity).IsEntityType())
            {
                throw new InvalidOperationException("类型“{0}”不是实体类型".FormatWith(typeof(TEntity).FullName));
            }

            total = source.Count(predicate);
            source = source.Where(predicate);
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
            return source != null
                ? source.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                : Enumerable.Empty<TEntity>().AsQueryable();
        }

        
    }
}