// -----------------------------------------------------------------------
//  <copyright file="DbContextManager.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-28 20:30</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using OSharp.Core.Data;
using OSharp.Data.Entity.Properties;
using OSharp.Utility;
using OSharp.Utility.Extensions;


namespace OSharp.Data.Entity
{
    /// <summary>
    /// 数据上下文管理器
    /// </summary>
    public sealed class DbContextManager
    {
        private static readonly Lazy<DbContextManager> InstanceLazy = new Lazy<DbContextManager>(() => new DbContextManager());

        /// <summary>
        /// 上下文类型-上下文初始化类型
        /// </summary>
        private readonly IDictionary<Type, DbContextInitializerBase> _contextInitializerDict
            = new Dictionary<Type, DbContextInitializerBase>();
        private readonly ConcurrentDictionary<Type, Type> _entityContextCache = new ConcurrentDictionary<Type, Type>();
        
        /// <summary>
        /// 获取 上下文管理器的唯一实例
        /// </summary>
        public static DbContextManager Instance { get { return InstanceLazy.Value; } }
        
        /// <summary>
        /// 注册上下文初始化器
        /// </summary>
        /// <param name="contextType">上下文类型</param>
        /// <param name="initializer">上下文初始化器</param>
        public void RegisterInitializer(Type contextType, DbContextInitializerBase initializer)
        {
            if (_contextInitializerDict.ContainsKey(contextType))
            {
                return;
            }
            _contextInitializerDict[contextType] = initializer;
            initializer.Initialize();
        }
        
        /// <summary>
        /// 获取实体类型对应的上下文类型
        /// </summary>
        public Type GetDbContexType(Type entityType)
        {
            entityType.CheckNotNull("entityType");
            if (!typeof(IEntity<>).IsGenericAssignableFrom(entityType))
            {
                throw new InvalidOperationException(Resources.DbContextManager_TypeNotEntityType.FormatWith(entityType.FullName));
            }
            Type contextType = null;
            if (_entityContextCache.ContainsKey(entityType))
            {
                contextType = _entityContextCache[entityType];
            }
            else
            {
                foreach (var item in _contextInitializerDict.Where(item => item.Value.EntityMappers.ContainsKey(entityType)))
                {
                    contextType = item.Key;
                    _entityContextCache[entityType] = contextType;
                    break;
                }
            }
            if (contextType == null)
            {
                throw new InvalidOperationException(Resources.DbContextManager_EntityTypeNotMaptoDbContext.FormatWith(entityType.FullName));
            }
            return contextType;
        }

        /// <summary>
        /// 获取 实体映射集合
        /// </summary>
        /// <param name="dbContextType">上下文类型</param>
        /// <returns>实体集合</returns>
        /// <exception cref="InvalidOperationException">如果<c>dbContextType</c>不存在则抛出<c>InvalidOperationException</c>异常</exception>
        public IEnumerable<IEntityMapper> GetEntityMappers(Type dbContextType)
        {
            dbContextType.CheckNotNull("dbContextType");
            if (!typeof(DbContextBase<>).IsGenericAssignableFrom(dbContextType))
            {
                throw new InvalidOperationException(Resources.DbContextManager_TypeNotDbContextType.FormatWith(dbContextType.FullName));
            }

            DbContextInitializerBase initializer;
            if (_contextInitializerDict.TryGetValue(dbContextType, out initializer))
            {
                return initializer.EntityMappers.Values;
            }
            return new List<IEntityMapper>();
        }
    }
}