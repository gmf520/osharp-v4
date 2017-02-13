// -----------------------------------------------------------------------
//  <copyright file="IFunction.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-16 22:10</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Data;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 功能接口，最小功能信息
    /// </summary>
    public interface IFunction : ILockable, IRecyclable
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
        /// 获取或设置 功能类型是否更改过，如为true，刷新功能时将忽略功能类型
        /// </summary>
        bool IsTypeChanged { get; set; }

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
        /// 获取或设置 是否自定义功能
        /// </summary>
        bool IsCustom { get; set; }

        /// <summary>
        /// 获取或设置 功能提供者，如Mvc，WebApi，SignalR等，用于功能的技术分组
        /// </summary>
        PlatformToken PlatformToken { get; set; }

        /// <summary>
        /// 获取或设置 功能地址
        /// </summary>
        string Url { get; set; }
    }
}