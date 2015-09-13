// -----------------------------------------------------------------------
//  <copyright file="VisiteType.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-13 9:29</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 表示功能访问限制类型的枚举
    /// </summary>
    public enum FilterType
    {
        /// <summary>
        /// 继承的，如果有父级，则与父级相同，如果没有父级，则为默认状态，不允许也不拒绝
        /// </summary>
        Inherited = 0,

        /// <summary>
        /// 允许的，如果同时有多个访问类型，并且没有拒绝，只要有一个允许，则允许
        /// </summary>
        Allowed = 1,

        /// <summary>
        /// 拒绝的，如果同时有多个访问类型，只要有一个拒绝，则拒绝
        /// </summary>
        Refused = 2
    }
}