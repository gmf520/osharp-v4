// -----------------------------------------------------------------------
//  <copyright file="IServicesBuilder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-07 18:23</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Core.Dependency
{
    /// <summary>
    /// 定义服务器映射集合创建功能
    /// </summary>
    public interface IServicesBuilder
    {
        /// <summary>
        /// 将当前服务创建为
        /// </summary>
        /// <returns>服务映射集合</returns>
        IServiceCollection Build();
    }
}