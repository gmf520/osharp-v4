// -----------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-10 11:49</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;

using OSharp.Core.Configs;
using OSharp.Core.Data;
using OSharp.Core.Dependency;
using OSharp.Data.Entity.Properties;
using OSharp.Utility.Extensions;


namespace OSharp.Data.Entity
{
    /// <summary>
    /// 服务映射集合扩展操作
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加数据层服务映射信息
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        public static void AddDataServices(this IServiceCollection services)
        {
            //添加上下文类型
            if (OSharpConfig.DataConfigReseter == null)
            {
                OSharpConfig.DataConfigReseter = new DataConfigReseter();
            }
            DataConfig config = OSharpConfig.Instance.DataConfig;
            Type[] contextTypes = config.ContextConfigs.Where(m => m.Enabled).Select(m => m.ContextType).ToArray();
            Type baseType = typeof(IUnitOfWork);
            foreach (var contextType in contextTypes)
            {
                if (!baseType.IsAssignableFrom(contextType))
                {
                    throw new InvalidOperationException(Resources.ContextTypeNotIUnitOfWorkType.FormatWith(contextType));
                }
                services.AddScoped(baseType, contextType);
                services.AddScoped(contextType);
            }

            //其他数据层初始化类型
            services.AddSingleton<IEntityMapperAssemblyFinder, EntityMapperAssemblyFinder>();
        }
    }
}