// -----------------------------------------------------------------------
//  <copyright file="IIocInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-29 17:37</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Configs;
using OSharp.Core.Dependency;


namespace OSharp.Core.Initialize
{
    /// <summary>
    /// 定义依赖注入初始化器，从程序集中反射进行依赖注入接口与实现的注册
    /// </summary>
    public interface IIocInitializer
    {
        /// <summary>
        /// 初始化依赖注入
        /// </summary>
        /// <param name="config">框架配置信息</param>
        void Initialize(OSharpConfig config);
    }
}