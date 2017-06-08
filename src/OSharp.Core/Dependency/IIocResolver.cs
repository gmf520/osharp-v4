// -----------------------------------------------------------------------
//  <copyright file="IIocResolver.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-01 23:36</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;


namespace OSharp.Core.Dependency
{
    /// <summary>
    /// 依赖注入对象解析获取器
    /// </summary>
    public interface IIocResolver
    {
        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        T Resolve<T>();

        ///// <summary>
        ///// 使用参数获取指定类型的实例
        ///// </summary>
        ///// <typeparam name="T">类型</typeparam>
        ///// <param name="args">参数</param>
        ///// <returns></returns>
        //T Resolve<T>(params KeyValuePair<string, object>[] args);

        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        object Resolve(Type type);

        ///// <summary>
        ///// 使用参数获取指定类型的实例
        ///// </summary>
        ///// <param name="type">类型</param>
        ///// <param name="args">参数</param>
        //object Resolve(Type type, params KeyValuePair<string, object>[] args);

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        IEnumerable<T> Resolves<T>();

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        IEnumerable<object> Resolves(Type type);
    }
}