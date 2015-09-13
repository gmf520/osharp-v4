// -----------------------------------------------------------------------
//  <copyright file="AutoMapperExtension.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-24 15:02</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

using AutoMapper;


namespace OSharp.Core.Data.Entity
{
    /// <summary>
    /// AutoMapper 辅助扩展方法
    /// </summary>
    public static class AutoMapperExtension
    {
        /// <summary>
        /// 创建映射，使用源对象创建目标对象
        /// </summary>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns>创建的目标对象</returns>
        public static TTarget MapTo<TTarget>(this object source)
        {
            return Mapper.Map<TTarget>(source);
        }

        /// <summary>
        /// 更新映射，使用源对象更新目标对象
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="target">要更新的目标对象</param>
        /// <returns>更新后的目标对象</returns>
        public static TTarget MapTo<TSource, TTarget>(this TSource source, TTarget target )
        {
            return Mapper.Map<TSource, TTarget>(source, target);
        }
    }
}