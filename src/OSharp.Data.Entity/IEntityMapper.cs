// -----------------------------------------------------------------------
//  <copyright file="IEntityMapper.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-07-16 22:18</last-date>
// -----------------------------------------------------------------------

using System;
using System.Data.Entity.ModelConfiguration.Configuration;


namespace OSharp.Data.Entity
{
    /// <summary>
    /// 实体映射接口
    /// </summary>
    public interface IEntityMapper
    {
        /// <summary>
        /// 获取 相关上下文类型，如为null，将使用默认上下文，否则使用指定的上下文类型
        /// </summary>
        Type DbContextType { get; }

        /// <summary>
        /// 将当前实体映射对象注册到当前数据访问上下文实体映射配置注册器中
        /// </summary>
        /// <param name="configurations">实体映射配置注册器</param>
        void RegistTo(ConfigurationRegistrar configurations);
    }
}