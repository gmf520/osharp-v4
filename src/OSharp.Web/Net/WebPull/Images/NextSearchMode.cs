// -----------------------------------------------------------------------
//  <copyright file="NextSearchMode.cs" company="OSharp开源团队">
//      Copyright (c) 2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-01-02 12:29</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OSharp.Web.Net.WebPull.Images
{
    /// <summary>
    /// 下一个数据搜索模式
    /// </summary>
    public enum NextSearchMode
    {
        /// <summary>
        /// 循环模式，知道总数，使用循环遍历
        /// </summary>
        Cycle,

        /// <summary>
        /// 逐个查询，从当前数据中获取下一数据的标识，一个一个获取
        /// </summary>
        OneByOne
    }
}