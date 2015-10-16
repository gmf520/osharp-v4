﻿// -----------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-14 2:36</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Dependency;
using OSharp.Core.Mapping;
using OSharp.Core.Security;
using OSharp.Utility;


namespace OSharp.AutoMapper
{
    /// <summary>
    /// 服务映射信息集合扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加AutoMapper服务映射信息
        /// </summary>
        public static void AddAutoMapperServices(this IServiceCollection services)
        {
            services.CheckNotNull("services" );
            services.AddSingleton<IMapper, AutoMapperMapper>();
            services.AddSingleton<InputDtoTypeFinder>();
            services.AddSingleton<EntityTypeFinder>();
            services.AddSingleton<OutputDtoTypeFinder>();
            services.AddSingleton<IMapTuple, InputDtoEntityMapTuple>();
            services.AddSingleton<IMapTuple, EntityOutputDtoMapTuple>();
        }
    }
}