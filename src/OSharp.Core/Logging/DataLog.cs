// -----------------------------------------------------------------------
//  <copyright file="DataLog.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-30 4:47</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using OSharp.Core.Data;


namespace OSharp.Core.Logging
{
    /// <summary>
    /// 数据日志信息类
    /// </summary>
    [Description("系统-数据日志信息")]
    public class DataLog : EntityBase<int>
    {
        /// <summary>
        /// 初始化一个<see cref="DataLog"/>类型的新实例
        /// </summary>
        public DataLog()
            : this(null, null, OperatingType.Query)
        { }

        /// <summary>
        /// 初始化一个<see cref="DataLog"/>类型的新实例
        /// </summary>
        public DataLog(string entityName, string name, OperatingType operatingType)
        {
            EntityName = entityName;
            Name = name;
            OperateType = operatingType;
            LogItems = new List<DataLogItem>();
        }

        /// <summary>
        /// 获取或设置 类型名称
        /// </summary>
        [StringLength(500)]
        [Display(Name = "类型名称")]
        public string EntityName { get; set; }

        /// <summary>
        /// 获取或设置 实体名称
        /// </summary>
        [Display(Name = "实体名称")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 数据编号
        /// </summary>
        [StringLength(150)]
        [DisplayName("主键值")]
        public string EntityKey { get; set; }

        /// <summary>
        /// 获取或设置 操作类型
        /// </summary>
        [Description("操作类型")]
        public OperatingType OperateType { get; set; }

        /// <summary>
        /// 获取或设置 操作日志信息
        /// </summary>
        public virtual OperateLog OperateLog { get; set; }

        /// <summary>
        /// 获取或设置 操作明细
        /// </summary>
        public virtual ICollection<DataLogItem> LogItems { get; set; }
    }


    /// <summary>
    /// 实体数据日志操作类型
    /// </summary>
    public enum OperatingType
    {
        /// <summary>
        /// 查询
        /// </summary>
        Query = 0,

        /// <summary>
        /// 新建
        /// </summary>
        Insert = 10,

        /// <summary>
        /// 更新
        /// </summary>
        Update = 20,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 30
    }
}