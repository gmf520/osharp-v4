// -----------------------------------------------------------------------
//  <copyright file="IContextTypeResolver.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-02 15:52</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Data;
using OSharp.Core.Dependency;


namespace OSharp.Data.Entity
{
    /// <summary>
    /// 定义数据上下文实例创建器
    /// </summary>
    public interface IDbContextTypeResolver : ISingletonDependency
    {
        /// <summary>
        /// 由实体类型获取关联的上下文类型
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <returns></returns>
        IUnitOfWork Resolve<TEntity, TKey>() where TEntity : IEntity<TKey>;

        /// <summary>
        /// 由实体类型获取关联的上下文类型
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <returns></returns>
        IUnitOfWork Resolve(Type entityType);
    }
}