﻿// -----------------------------------------------------------------------
//  <copyright file="IFinder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-28 2:45</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Dependency;


namespace OSharp.Core.Reflection
{
    /// <summary>
    /// 定义一个查找器
    /// </summary>
    /// <typeparam name="TItem">要查找的项类型</typeparam>
    public interface IFinder<out TItem>
    {
        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        TItem[] Find(Func<TItem, bool> predicate);

        /// <summary>
        /// 查找所有项
        /// </summary>
        /// <returns></returns>
        TItem[] FindAll();
    }
}