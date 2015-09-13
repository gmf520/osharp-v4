// -----------------------------------------------------------------------
//  <copyright file="DataLogItem.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-30 4:50</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;

using OSharp.Core.Data;
using OSharp.Utility.Data;


namespace OSharp.Core.Logging
{
    /// <summary>
    /// 实体操作日志明细
    /// </summary>
    [Description("系统-操作日志明细信息")]
    public class DataLogItem : EntityBase<Guid>
    {
        /// <summary>
        /// 初始化一个<see cref="DataLogItem"/>类型的新实例
        /// </summary>
        public DataLogItem()
            : this(null, null)
        { }

       /// <summary>
        ///初始化一个<see cref="DataLogItem"/>类型的新实例
       /// </summary>
       /// <param name="originalValue">旧值</param>
       /// <param name="newValue">新值</param>
        public DataLogItem(string originalValue, string newValue)
        {
            Id = CombHelper.NewComb();
            OriginalValue = originalValue;
            NewValue = newValue;
        }

        /// <summary>
        /// 获取或设置 字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 获取或设置 字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 获取或设置 旧值
        /// </summary>
        public string OriginalValue { get; set; }

        /// <summary>
        /// 获取或设置 新值
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// 获取或设置 数据类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 获取或设置 所属数据日志
        /// </summary>
        public virtual DataLog DataLog { get; set; }
    }
}