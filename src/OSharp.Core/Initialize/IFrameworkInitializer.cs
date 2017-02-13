// -----------------------------------------------------------------------
//  <copyright file="IFrameworkInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-10 14:44</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Dependency;


namespace OSharp.Core
{
    /// <summary>
    /// 框架初始化接口
    /// </summary>
    public interface IFrameworkInitializer
    {
        /// <summary>
        /// 开始执行框架初始化
        /// </summary>
        /// <param name="iocBuilder">依赖注入构建器</param>
        void Initialize(IIocBuilder iocBuilder);
    }
}