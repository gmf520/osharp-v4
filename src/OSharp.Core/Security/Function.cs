// -----------------------------------------------------------------------
//  <copyright file="Function.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-14 0:14</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;

using OSharp.Utility.Data;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 实体类——功能信息
    /// </summary>
    [Description("权限-功能信息")]
    public class Function : FunctionBase<Guid>
    {
        /// <summary>
        /// 初始化一个<see cref="Function"/>类型的新实例
        /// </summary>
        public Function()
        {
            Id = CombHelper.NewComb();
        }

    }
}