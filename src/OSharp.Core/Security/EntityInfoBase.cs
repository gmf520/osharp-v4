// -----------------------------------------------------------------------
//  <copyright file="EntityInfoBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-11 1:18</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

using OSharp.Core.Data;
using OSharp.Utility.Extensions;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 实体信息基类
    /// </summary>
    public abstract class EntityInfoBase<TKey> : EntityBase<TKey>, IEntityInfo
    {
        /// <summary>
        /// 获取或设置 实体类型全名
        /// </summary>
        [DisplayName("类型全名")]
        public string ClassName { get; set; }

        /// <summary>
        /// 获取或设置 实体名称
        /// </summary>
        [DisplayName("实体名称")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 是否启用数据日志
        /// </summary>
        [DisplayName("是否启用数据日志")]
        public bool DataLogEnabled { get; set; }

        /// <summary>
        /// 获取或设置 实体属性信息Json字符串
        /// </summary>
        public string PropertyNamesJson { get; set; }

        /// <summary>
        /// 获取 实体属性信息字典
        /// </summary>
        [NotMapped]
        public IDictionary<string, string> PropertyNames
        {
            get
            {
                if (PropertyNamesJson.IsNullOrEmpty())
                {
                    return new Dictionary<string, string>();
                }
                return PropertyNamesJson.FromJsonString<Dictionary<string, string>>();
            }
        }

        /// <summary>
        /// 获取或设置 是否已逻辑删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}