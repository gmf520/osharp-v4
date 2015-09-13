// -----------------------------------------------------------------------
//  <copyright file="ICacheProvider.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-22 15:36</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OSharp.Core.Caching
{
    /// <summary>
    /// 缓存提供者约定，用于创建并管理缓存对象
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="regionName">缓存区域名称</param>
        /// <returns></returns>
        ICache GetCache(string regionName);

    }
}