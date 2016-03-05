// -----------------------------------------------------------------------
//  <copyright file="IEntityInfo.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-13 9:30</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;

using OSharp.Core.Data;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 实体数据接口
    /// </summary>
    public interface IEntityInfo: IRecyclable
    {
        /// <summary>
        /// 获取 实体数据类型名称
        /// </summary>
        string ClassName { get; }

        /// <summary>
        /// 获取 实体数据显示名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 获取 是否启用数据日志
        /// </summary>
        bool DataLogEnabled { get; }

        /// <summary>
        /// 获取 实体属性信息字典
        /// </summary>
        IDictionary<string, string> PropertyNames { get; } 
    }
}