// -----------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-10 13:00</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Configs;
using OSharp.Core.Dependency;
using OSharp.Core.Initialize;
using OSharp.SiteBase.Initialize;


namespace OSharp.Logging.Log4Net
{
    /// <summary>
    /// 服务映射信息集合扩展操作
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Log4Net日志功能相关映射信息
        /// </summary>
        public static void AddLog4NetServices(this IServiceCollection services)
        {
            if (OSharpConfig.LoggingConfigReseter == null)
            {
                OSharpConfig.LoggingConfigReseter = new Log4NetLoggingConfigReseter();
            }
            services.AddSingleton<IBasicLoggingInitializer, Log4NetLoggingInitializer>();
            services.AddSingleton<Log4NetLoggerAdapter>();
        }
    }
}