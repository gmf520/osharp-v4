// -----------------------------------------------------------------------
//  <copyright file="IBasicLoggingInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-29 13:41</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Configs;


namespace OSharp.Core.Initialize
{
    /// <summary>
    /// 定义基础日志初始化器，用于初始化基础日志功能
    /// </summary>
    public interface IBasicLoggingInitializer
    {
        /// <summary>
        /// 开始初始化基础日志
        /// </summary>
        /// <param name="config">日志配置信息</param>
        void Initialize(LoggingConfig config);
    }
}