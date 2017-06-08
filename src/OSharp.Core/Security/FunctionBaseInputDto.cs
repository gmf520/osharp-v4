// -----------------------------------------------------------------------
//  <copyright file="FunctionBaseInputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-20 10:46</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

using OSharp.Core.Data;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 功能信息输入DTO基类
    /// </summary>
    /// <typeparam name="TKey">功能编号类型</typeparam>
    public abstract class FunctionBaseInputDto<TKey> : IInputDto<TKey>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// 获取 功能名称
        /// </summary>
        [Required, StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 获取 功能地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 获取 功能类型
        /// </summary>
        public FunctionType FunctionType { get; set; }

        /// <summary>
        /// 获取或设置 区域名称
        /// </summary>
        [StringLength(100)]
        public string Area { get; set; }

        /// <summary>
        /// 获取或设置 控制器名称
        /// </summary>
        [StringLength(100)]
        public string Controller { get; set; }

        /// <summary>
        /// 获取或设置 功能名称
        /// </summary>
        [StringLength(100)]
        public string Action { get; set; }

        /// <summary>
        /// 获取或设置 是否控制器，如果为false，则此记录为action的记录
        /// </summary>
        public bool IsController { get; set; }

        /// <summary>
        /// 获取 是否启用操作日志
        /// </summary>
        public bool OperateLogEnabled { get; set; }

        /// <summary>
        /// 获取 是否启用数据日志
        /// </summary>
        public bool DataLogEnabled { get; set; }

        /// <summary>
        /// 获取或设置 数据缓存时间（秒）
        /// </summary>
        public int CacheExpirationSeconds { get; set; }

        /// <summary>
        /// 获取或设置 是否相对过期时间，否则为绝对过期
        /// </summary>
        public bool IsCacheSliding { get; set; }

        /// <summary>
        /// 获取或设置 是否Ajax记录
        /// </summary>
        public bool IsAjax { get; set; }

        /// <summary>
        /// 获取或设置 是否子功能
        /// </summary>
        public bool IsChild { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

    }
}