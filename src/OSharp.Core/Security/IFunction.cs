// -----------------------------------------------------------------------
//  <copyright file="IFunction.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-16 22:10</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Core.Security
{
    /// <summary>
    /// 功能接口，最小功能信息
    /// </summary>
    public interface IFunction
    {
        /// <summary>
        /// 获取或设置 功能名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 获取或设置 区域名称
        /// </summary>
        string Area { get; set; }

        /// <summary>
        /// 获取或设置 控制器名称
        /// </summary>
        string Controller { get; set; }

        /// <summary>
        /// 获取或设置 功能名称
        /// </summary>
        string Action { get; set; }

        /// <summary>
        /// 获取或设置 功能类型
        /// </summary>
        FunctionType FunctionType { get; set; }

        /// <summary>
        /// 获取或设置 是否启用操作日志
        /// </summary>
        bool OperateLogEnabled { get; set; }

        /// <summary>
        /// 获取或设置 是否启用数据日志
        /// </summary>
        bool DataLogEnabled { get; set; }

        /// <summary>
        /// 获取或设置 数据缓存时间（秒）
        /// </summary>
        int CacheExpirationSeconds { get; set; }

        /// <summary>
        /// 获取或设置 是否相对过期时间，否则为绝对过期
        /// </summary>
        bool IsCacheSliding { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 功能地址
        /// </summary>
        string Url { get; set; }
    }
}