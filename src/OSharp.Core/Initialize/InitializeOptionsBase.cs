// -----------------------------------------------------------------------
//  <copyright file="InitializeOptionsBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-29 19:31</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Configs;
using OSharp.Core.Context;
using OSharp.Core.Dependency;
using OSharp.Core.Security;


namespace OSharp.Core.Initialize
{
    /// <summary>
    /// 框架初始化选项基类
    /// </summary>
    public abstract class InitializeOptionsBase
    {
        private static OSharpConfig _osharpConfig;

        /// <summary>
        /// 获取 框架配置信息
        /// </summary>
        public OSharpConfig OSharpConfig
        {
            get
            {
                if (_osharpConfig == null)
                {
                    OSharpConfig config = OSharpConfig.Instance;
                    if (DataConfigReseter != null)
                    {
                        config.DataConfig = DataConfigReseter.Reset(config.DataConfig);
                    }
                    _osharpConfig = config;
                }
                return _osharpConfig;
            }
        }

        /// <summary>
        /// 获取或设置 当前运行平台标识
        /// </summary>
        public PlatformToken PlatformToken { get; set; }

        /// <summary>
        /// 获取或设置 数据配置重置者
        /// </summary>
        public IDataConfigReseter DataConfigReseter { get; set; }

        /// <summary>
        /// 获取或设置 依赖注入映射创建器
        /// </summary>
        public IServicesBuilder ServicesBuilder { get; set; }

        /// <summary>
        /// 获取或设置 基础日志初始化器
        /// </summary>
        public IBasicLoggingInitializer BasicLoggingInitializer { get; set; }

        /// <summary>
        /// 获取或设置 依赖注入初始化器
        /// </summary>
        public IIocInitializer IocInitializer { get; set; }

        /// <summary>
        /// 获取或设置 数据库初始化器
        /// </summary>
        public IDatabaseInitializer DatabaseInitializer { get; set; }
    }
}