// -----------------------------------------------------------------------
//  <copyright file="IServicesBuilder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-07 18:23</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Configs;


namespace OSharp.Core.Dependency
{
    /// <summary>
    /// 定义服务器映射集合创建功能
    /// </summary>
    public interface IServicesBuilder
    {
        /// <summary>
        /// 创建依赖注入服务映射信息集合
        /// </summary>
        /// <param name="config">OSharp配置信息</param>
        /// <returns></returns>
        IServiceCollection Build(OSharpConfig config);
    }
}