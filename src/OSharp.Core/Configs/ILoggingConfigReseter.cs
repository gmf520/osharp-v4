// -----------------------------------------------------------------------
//  <copyright file="ILoggingConfigReseter.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-10 10:45</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Core.Configs
{
    /// <summary>
    /// 定义日志配置信息重置功能
    /// </summary>
    public interface ILoggingConfigReseter
    {
        /// <summary>
        /// 日志配置信息重置
        /// </summary>
        /// <param name="config">待重置的日志配置信息</param>
        /// <returns>重置后的日志配置信息</returns>
        LoggingConfig Reset(LoggingConfig config);
    }
}