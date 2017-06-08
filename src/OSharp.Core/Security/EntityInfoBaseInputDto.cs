// -----------------------------------------------------------------------
//  <copyright file="EntityInfoBaseInputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-20 10:48</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Data;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 实体信息输入DTO基类
    /// </summary>
    /// <typeparam name="TKey">实体数据编号类型</typeparam>
    public abstract class EntityInfoBaseInputDto<TKey> : IInputDto<TKey>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// 获取 实体数据显示名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 获取 实体数据类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取 是否启用数据日志
        /// </summary>
        public bool DataLogEnabled { get; set; }
    }
}