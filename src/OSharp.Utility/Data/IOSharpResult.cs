﻿// -----------------------------------------------------------------------
//  <copyright file="IOSharpResult.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-03 18:20</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Utility.Data
{
    /// <summary>
    /// OSharp操作结果
    /// </summary>
    /// <typeparam name="TResultType"></typeparam>
    public interface IOSharpResult<TResultType> : IOSharpResult<TResultType, object>
    { }


    /// <summary>
    /// OSharp操作结果
    /// </summary>
    public interface IOSharpResult<TResultType, TData>
    {
        /// <summary>
        /// 获取或设置 结果类型
        /// </summary>
        TResultType ResultType { get; set; }

        /// <summary>
        /// 获取或设置 返回消息
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// 获取或设置 结果数据
        /// </summary>
        TData Data { get; set; }
    }
}