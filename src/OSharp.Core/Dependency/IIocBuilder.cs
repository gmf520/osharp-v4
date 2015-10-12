// -----------------------------------------------------------------------
//  <copyright file="IIocBuilder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-10 15:26</last-date>
// -----------------------------------------------------------------------

using System;


namespace OSharp.Core.Dependency
{
    /// <summary>
    /// 定义依赖注入构建器，解析依赖注入服务映射信息进行构建
    /// </summary>
    public interface IIocBuilder
    {
        /// <summary>
        /// 获取 服务提供者
        /// </summary>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 开始构建依赖注入映射
        /// </summary>
        /// <returns>服务提供者</returns>
        IServiceProvider Build();
    }
}