// -----------------------------------------------------------------------
//  <copyright file="DbContextExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-07-17 2:32</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

using OSharp.Core.Data;
using OSharp.Core.Data.Extensions;
using OSharp.Core.Dependency;
using OSharp.Core.Logging;
using OSharp.Core.Security;
using OSharp.Utility;


namespace OSharp.Data.Entity
{
    /// <summary>
    /// 上下文扩展辅助操作类
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// 更新上下文中指定的实体的状态
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="dbContext">上下文对象</param>
        /// <param name="entities">要更新的实体类型</param>
        public static void Update<TEntity, TKey>(this DbContext dbContext, params TEntity[] entities)
            where TEntity : class, IEntity<TKey>
        {
            dbContext.CheckNotNull("dbContext");
            entities.CheckNotNull("entities");

            DbSet<TEntity> dbSet = dbContext.Set<TEntity>();
            foreach (TEntity entity in entities)
            {
                entity.CheckIUpdateAudited<TEntity, TKey>();
                try
                {
                    DbEntityEntry<TEntity> entry = dbContext.Entry(entity);
                    if (entry.State == EntityState.Detached)
                    {
                        dbSet.Attach(entity);
                        entry.State = EntityState.Modified;
                    }
                }
                catch (InvalidOperationException)
                {
                    TEntity oldEntity = dbSet.Find(entity.Id);
                    dbContext.Entry(oldEntity).CurrentValues.SetValues(entity);
                }
            }
        }
        
        /// <summary>
        /// 获取数据上下文的变更日志信息
        /// </summary>
        public static IEnumerable<DataLog> GetEntityDataLogs(this DbContext dbContext, IServiceProvider provider)
        {
            if (provider == null)
            {
                return Enumerable.Empty<DataLog>();
            }
            IEntityInfoHandler entityInfoHandler = provider.GetService<IEntityInfoHandler>();
            if (entityInfoHandler == null)
            {
                return Enumerable.Empty<DataLog>();
            }

            ObjectContext objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;
            ObjectStateManager manager = objectContext.ObjectStateManager;

            IEnumerable<DataLog> logs = from entry in manager.GetObjectStateEntries(EntityState.Added).Where(entry => entry.Entity != null)
                let entityInfo = entityInfoHandler.GetEntityInfo(entry.Entity.GetType())
                where entityInfo != null && entityInfo.DataLogEnabled
                select GetAddedLog(entry, entityInfo);

            logs = logs.Concat(from entry in manager.GetObjectStateEntries(EntityState.Modified).Where(entry => entry.Entity != null)
                let entityInfo = entityInfoHandler.GetEntityInfo(entry.Entity.GetType())
                where entityInfo != null && entityInfo.DataLogEnabled
                select GetModifiedLog(entry, entityInfo));

            logs = logs.Concat(from entry in manager.GetObjectStateEntries(EntityState.Deleted).Where(entry => entry.Entity != null)
                let entityInfo = entityInfoHandler.GetEntityInfo(entry.Entity.GetType())
                where entityInfo != null && entityInfo.DataLogEnabled
                select GetDeletedLog(entry, entityInfo));

            return logs;
        }

        /// <summary>
        /// 异步获取数据上下文的变更日志信息
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<DataLog>> GetEntityOperateLogsAsync(this DbContext dbContext, IServiceProvider provider)
        {
            return await Task.FromResult(dbContext.GetEntityDataLogs(provider));
        }
        
        /// <summary>
        /// 获取添加数据的日志信息
        /// </summary>
        /// <param name="entry">实体状态跟踪信息</param>
        /// <param name="entityInfo">实体数据信息</param>
        /// <returns>新增数据日志信息</returns>
        private static DataLog GetAddedLog(ObjectStateEntry entry, IEntityInfo entityInfo)
        {
            DataLog log = new DataLog(entityInfo.ClassName, entityInfo.Name, OperatingType.Insert);
            for (int i = 0; i < entry.CurrentValues.FieldCount; i++)
            {
                string name = entry.CurrentValues.GetName(i);
                if (name == "Timestamp")
                {
                    continue;
                }
                object value = entry.CurrentValues.GetValue(i);
                if (name == "Id")
                {
                    log.EntityKey = value.ToString();
                }
                Type fieldType = entry.CurrentValues.GetFieldType(i);
                DataLogItem logItem = new DataLogItem()
                {
                    Field = name,
                    FieldName = entityInfo.PropertyNames[name],
                    NewValue = value == null ? null : value.ToString(),
                    DataType = fieldType == null ? null : fieldType.Name
                };
                log.LogItems.Add(logItem);
            }
            return log;
        }

        /// <summary>
        /// 获取修改数据的日志信息
        /// </summary>
        /// <param name="entry">实体状态跟踪信息</param>
        /// <param name="entityInfo">实体数据信息</param>
        /// <returns>修改数据日志信息</returns>
        private static DataLog GetModifiedLog(ObjectStateEntry entry, IEntityInfo entityInfo)
        {
            DataLog log = new DataLog(entityInfo.ClassName, entityInfo.Name, OperatingType.Update);
            for (int i = 0; i < entry.CurrentValues.FieldCount; i++)
            {
                string name = entry.CurrentValues.GetName(i);
                if (name == "Timestamp")
                {
                    continue;
                }
                object currentValue = entry.CurrentValues.GetValue(i);
                object originalValue = entry.OriginalValues[name];
                if (name == "Id")
                {
                    log.EntityKey = originalValue.ToString();
                }
                if (currentValue.Equals(originalValue))
                {
                    continue;
                }
                Type fieldType = entry.CurrentValues.GetFieldType(i);
                DataLogItem logItem = new DataLogItem()
                {
                    Field = name,
                    FieldName = entityInfo.PropertyNames[name],
                    NewValue = currentValue == null ? null : currentValue.ToString(),
                    OriginalValue = originalValue == null ? null : originalValue.ToString(),
                    DataType = fieldType == null ? null : fieldType.Name
                };
                log.LogItems.Add(logItem);
            }
            return log;
        }

        /// <summary>
        /// 获取删除数据的日志信息
        /// </summary>
        /// <param name="entry">实体状态跟踪信息</param>
        /// <param name="entityInfo">实体数据信息</param>
        /// <returns>删除数据日志信息</returns>
        private static DataLog GetDeletedLog(ObjectStateEntry entry, IEntityInfo entityInfo)
        {
            DataLog log = new DataLog(entityInfo.ClassName, entityInfo.Name, OperatingType.Delete);
            for (int i = 0; i < entry.OriginalValues.FieldCount; i++)
            {
                string name = entry.OriginalValues.GetName(i);
                if (name == "Timestamp")
                {
                    continue;
                }
                object originalValue = entry.OriginalValues[i];
                if (name == "Id")
                {
                    log.EntityKey = originalValue.ToString();
                }
                Type fieldType = entry.OriginalValues.GetFieldType(i);
                DataLogItem logItem = new DataLogItem()
                {
                    Field = name,
                    FieldName = entityInfo.PropertyNames[name],
                    OriginalValue = originalValue == null ? null : originalValue.ToString(),
                    DataType = fieldType == null ? null : fieldType.Name
                };
                log.LogItems.Add(logItem);
            }
            return log;
        }
    }
}