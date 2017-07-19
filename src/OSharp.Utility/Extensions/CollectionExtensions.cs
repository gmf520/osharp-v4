// -----------------------------------------------------------------------
//  <copyright file="CollectionExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-08-04 0:29</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using OSharp.Utility.Data;
using OSharp.Utility.Filter;


namespace OSharp.Utility.Extensions
{
    /// <summary>
    /// 集合扩展方法类
    /// </summary>
    public static class CollectionExtensions
    {
        #region IEnumerable的扩展

        /// <summary>
        /// 打乱一个集合的项顺序
        /// </summary>
        public static IEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return source.OrderBy(m => Guid.NewGuid());
        }

        /// <summary>
        /// 将集合展开并分别转换成字符串，再以指定的分隔符衔接，拼成一个字符串返回。默认分隔符为逗号
        /// </summary>
        /// <param name="collection"> 要处理的集合 </param>
        /// <param name="separator"> 分隔符，默认为逗号 </param>
        /// <returns> 拼接后的字符串 </returns>
        public static string ExpandAndToString<T>(this IEnumerable<T> collection, string separator = ",")
        {
            return collection.ExpandAndToString(t => t.ToString(), separator);
        }

        /// <summary>
        /// 循环集合的每一项，调用委托生成字符串，返回合并后的字符串。默认分隔符为逗号
        /// </summary>
        /// <param name="collection">待处理的集合</param>
        /// <param name="itemFormatFunc">单个集合项的转换委托</param>
        /// <param name="separetor">分隔符，默认为逗号</param>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <returns></returns>
        public static string ExpandAndToString<T>(this IEnumerable<T> collection, Func<T, string> itemFormatFunc, string separetor = ",")
        {
            collection = collection as IList<T> ?? collection.ToList();
            itemFormatFunc.CheckNotNull("itemFormatFunc");
            if (!collection.Any())
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            int i = 0;
            int count = collection.Count();
            foreach (T t in collection)
            {
                if (i == count - 1)
                {
                    sb.Append(itemFormatFunc(t));
                }
                else
                {
                    sb.Append(itemFormatFunc(t) + separetor);
                }
                i++;
            }
            return sb.ToString();
        }

        /// <summary>
        /// 集合是否为空
        /// </summary>
        /// <param name="collection"> 要处理的集合 </param>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <returns> 为空返回True，不为空返回False </returns>
        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            collection = collection as IList<T> ?? collection.ToList();
            return !collection.Any();
        }

        /// <summary>
        /// 根据第三方条件是否为真来决定是否执行指定条件的查询
        /// </summary>
        /// <param name="source"> 要查询的源 </param>
        /// <param name="predicate"> 查询条件 </param>
        /// <param name="condition"> 第三方条件 </param>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <returns> 查询的结果 </returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition)
        {
            predicate.CheckNotNull("predicate");
            source = source as IList<T> ?? source.ToList();

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// 根据指定条件返回集合中不重复的元素
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <typeparam name="TKey">动态筛选条件类型</typeparam>
        /// <param name="source">要操作的源</param>
        /// <param name="keySelector">重复数据筛选条件</param>
        /// <returns>不重复元素的集合</returns>
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            keySelector.CheckNotNull("keySelector");
            source = source as IList<T> ?? source.ToList();

            return source.GroupBy(keySelector).Select(group => group.First());
        }

        /// <summary>
        /// 把<see cref="IEnumerable{T}"/>集合按指定字段与排序方式进行排序
        /// </summary>
        /// <typeparam name="T">集合项类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns>排序后的数据集</returns>
        public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> source,
            string propertyName,
            ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            propertyName.CheckNotNullOrEmpty("propertyName");
            return CollectionPropertySorter<T>.OrderBy(source, propertyName, sortDirection);
        }

        /// <summary>
        /// 把<see cref="IEnumerable{T}"/>集合按指定字段排序条件进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="sortCondition">列表字段排序条件</param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> source, SortCondition sortCondition)
        {
            sortCondition.CheckNotNull("sortCondition");
            return source.OrderBy(sortCondition.SortField, sortCondition.ListSortDirection);
        }

        /// <summary>
        /// 把<see cref="IEnumerable{T}"/>集合按指定字段排序条件进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="sortCondition">列表字段排序条件</param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> source, SortCondition<T> sortCondition)
        {
            sortCondition.CheckNotNull("sortCondition");
            return source.OrderBy(sortCondition.SortField, sortCondition.ListSortDirection);
        }

        /// <summary>
        /// 把<see cref="IOrderedQueryable{T}"/>集合继续按指定字段排序方式进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> ThenBy<T>(this IOrderedEnumerable<T> source,
            string propertyName,
            ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            source.CheckNotNull("source");
            propertyName.CheckNotNullOrEmpty("propertyName");

            return CollectionPropertySorter<T>.ThenBy(source, propertyName, sortDirection);
        }

        /// <summary>
        /// 把<see cref="IOrderedEnumerable{T}"/>集合继续指定字段排序方式进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="sortCondition">列表字段排序条件</param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> ThenBy<T>(this IOrderedEnumerable<T> source, SortCondition sortCondition)
        {
            source.CheckNotNull("source");
            sortCondition.CheckNotNull("sortCondition");

            return source.ThenBy(sortCondition.SortField, sortCondition.ListSortDirection);
        }

        #endregion

        #region IQueryable的扩展

        /// <summary>
        /// 根据第三方条件是否为真来决定是否执行指定条件的查询
        /// </summary>
        /// <param name="source"> 要查询的源 </param>
        /// <param name="predicate"> 查询条件 </param>
        /// <param name="condition"> 第三方条件 </param>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <returns> 查询的结果 </returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, bool condition)
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// 把<see cref="IQueryable{T}"/>集合按指定字段与排序方式进行排序
        /// </summary>
        /// <param name="source">要排序的数据集</param>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="sortDirection">排序方向</param>
        /// <typeparam name="T">动态类型</typeparam>
        /// <returns>排序后的数据集</returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source,
            string propertyName,
            ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            source.CheckNotNull("source");
            propertyName.CheckNotNullOrEmpty("propertyName");

            return CollectionPropertySorter<T>.OrderBy(source, propertyName, sortDirection);
        }

        /// <summary>
        /// 把<see cref="IQueryable{T}"/>集合按指定字段排序条件进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="sortCondition">列表字段排序条件</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, SortCondition sortCondition)
        {
            source.CheckNotNull("source");
            sortCondition.CheckNotNull("sortCondition");

            return source.OrderBy(sortCondition.SortField, sortCondition.ListSortDirection);
        }

        /// <summary>
        /// 把<see cref="IQueryable{T}"/>集合按指定字段排序条件进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="sortCondition">列表字段排序条件</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, SortCondition<T> sortCondition)
        {
            source.CheckNotNull("source");
            sortCondition.CheckNotNull("sortCondition");
            return source.OrderBy(sortCondition.SortField, sortCondition.ListSortDirection);
        }

        /// <summary>
        /// 把<see cref="IOrderedQueryable{T}"/>集合继续按指定字段排序方式进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source,
            string propertyName,
            ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            source.CheckNotNull("source");
            propertyName.CheckNotNullOrEmpty("propertyName");

            return CollectionPropertySorter<T>.ThenBy(source, propertyName, sortDirection);
        }

        /// <summary>
        /// 把<see cref="IOrderedQueryable{T}"/>集合继续指定字段排序方式进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="sortCondition">列表字段排序条件</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, SortCondition sortCondition)
        {
            source.CheckNotNull("source");
            sortCondition.CheckNotNull("sortCondition");

            return source.ThenBy(sortCondition.SortField, sortCondition.ListSortDirection);
        }

        /*/// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合中筛选指定键范围内的子数据集
        /// </summary>
        /// <typeparam name="TSource">集合元素类型</typeparam>
        /// <typeparam name="TKey">筛选键类型</typeparam>
        /// <param name="source">要筛选的数据源</param>
        /// <param name="keySelector">筛选键的范围表达式</param>
        /// <param name="start">筛选范围起始值</param>
        /// <param name="end">筛选范围结束值</param>
        /// <param name="startEqual">是否等于起始值</param>
        /// <param name="endEqual">是否等于结束集</param>
        /// <returns></returns>
        public static IQueryable<TSource> Between<TSource, TKey>(this IQueryable<TSource> source,
            Expression<Func<TSource, TKey>> keySelector,
            TKey start,
            TKey end,
            bool startEqual = false,
            bool endEqual = false) where TKey : IComparable<TKey>
        {
            Expression[] paramters = keySelector.Parameters.Cast<Expression>().ToArray();
            Expression key = Expression.Invoke(keySelector, paramters);
            Expression startBound = startEqual
                ? Expression.GreaterThanOrEqual(key, Expression.Constant(start))
                : Expression.GreaterThan(key, Expression.Constant(start));
            Expression endBound = endEqual
                ? Expression.LessThanOrEqual(key, Expression.Constant(end))
                : Expression.LessThan(key, Expression.Constant(end));
            Expression and = Expression.AndAlso(startBound, endBound);
            Expression<Func<TSource, bool>> lambda = Expression.Lambda<Func<TSource, bool>>(and, keySelector.Parameters);
            return source.Where(lambda);
        }*/

        #endregion
    }
}