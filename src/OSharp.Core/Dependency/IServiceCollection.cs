// -----------------------------------------------------------------------
//  <copyright file="IServiceCollection.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-10 14:56</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;


namespace OSharp.Core.Dependency
{
    /// <summary>
    /// 定义服务映射信息集合，用于装载注册类型映射的描述信息
    /// </summary>
    public interface IServiceCollection : IList<ServiceDescriptor>
    {
        /// <summary>
        /// 克隆创建当前集合的副本
        /// </summary>
        /// <returns></returns>
        IServiceCollection Clone();
    }
}