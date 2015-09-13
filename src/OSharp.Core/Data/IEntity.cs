// -----------------------------------------------------------------------
//  <copyright file="IEntity.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-25 13:31</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OSharp.Core.Data
{
    /// <summary>
    /// 数据模型接口
    /// </summary>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 获取或设置 实体唯一标识，主键
        /// </summary>
        TKey Id { get; set; }
    }
}