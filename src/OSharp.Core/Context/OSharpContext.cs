// -----------------------------------------------------------------------
//  <copyright file="OSharpContext.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-28 0:41</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Dependency;
using OSharp.Core.Properties;


namespace OSharp.Core.Context
{
    /// <summary>
    /// OSharp上下文，存储OSharp全局配置信息
    /// </summary>
    public static class OSharpContext
    {
        private static IServiceCollection _iocRegisterServices;

        /// <summary>
        /// 获取 依赖注入注册映射信息集合
        /// </summary>
        public static IServiceCollection IocRegisterServices
        {
            get
            {
                if (_iocRegisterServices == null)
                {
                    throw new InvalidOperationException(Resources.Context_BuildServicesFirst);
                }
                return _iocRegisterServices;
            }
            internal set { _iocRegisterServices = value; }
        }

        /// <summary>
        /// 获取或设置 依赖注入服务提供者
        /// </summary>
        public static IServiceProvider IocServiceProvider { get; set; }
        
    }
}