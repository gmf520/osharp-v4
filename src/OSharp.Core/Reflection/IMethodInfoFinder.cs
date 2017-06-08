// -----------------------------------------------------------------------
//  <copyright file="IMethodInfoFinder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-08 2:34</last-date>
// -----------------------------------------------------------------------

using System;
using System.Reflection;


namespace OSharp.Core.Reflection
{
    /// <summary>
    /// 定义方法信息查找器
    /// </summary>
    public interface IMethodInfoFinder
    {
        /// <summary>
        /// 查找指定条件的方法信息
        /// </summary>
        /// <param name="type">控制器类型</param>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        MethodInfo[] Find(Type type, Func<MethodInfo, bool> predicate);

        /// <summary>
        /// 从指定类型查找方法信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        MethodInfo[] FindAll(Type type);
    }
}