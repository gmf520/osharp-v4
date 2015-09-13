// -----------------------------------------------------------------------
//  <copyright file="IFrameworkInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-29 12:13</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OSharp.Core
{
    /// <summary>
    /// 框架初始化接口
    /// </summary>
    public interface IFrameworkInitializer
    {
        /// <summary>
        /// 开始初始化
        /// </summary>
        void Initialize();
    }
}