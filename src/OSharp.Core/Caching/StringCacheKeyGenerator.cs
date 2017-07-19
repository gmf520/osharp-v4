﻿// -----------------------------------------------------------------------
//  <copyright file="StringCacheKeyGenerator.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-11-16 23:41</last-date>
// -----------------------------------------------------------------------

using OSharp.Utility;
using OSharp.Utility.Extensions;


namespace OSharp.Core.Caching
{
    /// <summary>
    /// 字符串缓存键生成器
    /// </summary>
    public class StringCacheKeyGenerator : ICacheKeyGenerator
    {
        #region Implementation of ICacheKeyGenerator

        /// <summary>
        /// 生成缓存键
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public string GetKey(params object[] args)
        {
            args.CheckNotNullOrEmpty("args");
            return args.ExpandAndToString("-");
        }

        #endregion
    }
}