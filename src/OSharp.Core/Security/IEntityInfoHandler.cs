// -----------------------------------------------------------------------
//  <copyright file="IEntityInfoHandler.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-14 0:25</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Dependency;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 实体信息处理器
    /// </summary>
    public interface IEntityInfoHandler : ISingletonDependency
    {
        /// <summary>
        /// 从程序集中刷新实体信息数据
        /// </summary>
        void Initialize();

        /// <summary>
        /// 查找指定实体类型的实体信息
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <returns>符合条件的实体信息</returns>
        IEntityInfo GetEntityInfo(Type type);

        /// <summary>
        /// 刷新实体信息缓存
        /// </summary>
        void RefreshCache();
    }
}