﻿// -----------------------------------------------------------------------
//  <copyright file="MapperExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-14 0:44</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;

using OSharp.Core.Data;
using OSharp.Core.Properties;
using OSharp.Utility;


namespace OSharp.Core.Mapping
{
    /// <summary>
    /// 对象映射扩展操作
    /// </summary>
    public static class MapperExtensions
    {
        private static IMapper _mapper;

        /// <summary>
        /// 设置对象映射执行者
        /// </summary>
        /// <param name="mapper">映射执行者</param>
        public static void SetMaper(IMapper mapper)
        {
            mapper.CheckNotNull("mapper");
            _mapper = mapper;
        }

        /// <summary>
        /// 将对象映射为指定类型
        /// </summary>
        /// <typeparam name="TTarget">要映射的目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns>目标类型的对象</returns>
        public static TTarget MapTo<TTarget>(this object source)
        {
            CheckMapper();
            return _mapper.MapTo<TTarget>(source);
        }

        /// <summary>
        /// 使用源类型的对象更新目标类型的对象
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="target">待更新的目标对象</param>
        /// <returns>更新后的目标类型对象</returns>
        public static TTarget MapTo<TSource, TTarget>(this TSource source, TTarget target)
        {
            CheckMapper();
            return _mapper.MapTo(source, target);
        }

        /// <summary>
        /// 将数据源映射为指定<see cref="IOutputDto"/>的集合
        /// </summary>
        public static IQueryable<TOutputDto> ToOutput<TOutputDto>(this IQueryable source, params Expression<Func<TOutputDto, object>>[] membersToExpand)
        {
            return _mapper.ToOutput(source, membersToExpand);
        }

        /// <summary>
        /// 验证映射执行者是否为空
        /// </summary>
        private static void CheckMapper()
        {
            if (_mapper == null)
            {
                throw new NullReferenceException(Resources.Map_MapperIsNull);
            }
        }
    }
}