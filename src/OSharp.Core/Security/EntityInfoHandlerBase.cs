// -----------------------------------------------------------------------
//  <copyright file="EntityInfoHandlerBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-11 12:16</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using OSharp.Core.Data;
using OSharp.Core.Dependency;
using OSharp.Utility.Collections;
using OSharp.Utility.Extensions;
using OSharp.Utility.Logging;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 实体数据信息处理器
    /// </summary>
    /// <typeparam name="TEntityInfo">实体数据信息类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public abstract class EntityInfoHandlerBase<TEntityInfo, TKey> : IEntityInfoHandler
        where TEntityInfo : EntityInfoBase<TKey>, IEntity<TKey>, new()
    {
        private ILogger _logger;

        /// <summary>
        /// 获取 日志对象
        /// </summary>
        protected ILogger Logger
        {
            get { return _logger ?? (_logger = LogManager.GetLogger(GetType())); }
        }

        /// <summary>
        /// 获取或设置 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 获取或设置 实体类型查找器
        /// </summary>
        public IEntityTypeFinder EntityTypeFinder { get; set; }

        /// <summary>
        /// 获取 所有实体数据集合
        /// </summary>
        public TEntityInfo[] EntityInfos { get; private set; }

        /// <summary>
        /// 从程序集中刷新实体信息，主要检索实现了<see cref="IEntity{TKey}"/>接口的实体类
        /// </summary>
        public void Initialize()
        {
            Type[] entityTypes = EntityTypeFinder.FindAll();
            TEntityInfo[] entityInfos = GetEntityInfos(entityTypes);
            UpdateToRepository(entityInfos);
            RefreshCache();
        }

        /// <summary>
        /// 查找指定实体类型的实体信息
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <returns>符合条件的实体信息</returns>
        public IEntityInfo GetEntityInfo(Type type)
        {
            if (EntityInfos == null || EntityInfos.Length == 0)
            {
                RefreshCache();
            }
            Debug.Assert(EntityInfos != null, "EntityInfos != null");
            return EntityInfos.FirstOrDefault(m => m.ClassName == type.FullName) ??
                EntityInfos.FirstOrDefault(m => type.BaseType != null && m.ClassName == type.BaseType.FullName);
        }

        /// <summary>
        /// 刷新实体信息缓存
        /// </summary>
        public void RefreshCache()
        {
            EntityInfos = GetLastestEntityInfos();
        }

        /// <summary>
        /// 从实体类型中获取实体信息集合
        /// </summary>
        /// <param name="entityTypes">实体类型集合</param>
        /// <returns>实体信息集合</returns>
        protected virtual TEntityInfo[] GetEntityInfos(Type[] entityTypes)
        {
            List<TEntityInfo> entityInfos = new List<TEntityInfo>();
            foreach (Type type in entityTypes.OrderBy(m => m.FullName))
            {
                string fullName = type.FullName;
                if (entityInfos.Exists(m => m.ClassName == fullName))
                {
                    continue;
                }
                TEntityInfo entityInfo = new TEntityInfo()
                {
                    ClassName = fullName,
                    Name = type.ToDescription(),
                    DataLogEnabled = true
                };
                IDictionary<string, string> propertyDict = new Dictionary<string, string>();
                PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo property in properties)
                {
                    propertyDict.Add(property.Name, property.ToDescription());
                }
                entityInfo.PropertyNamesJson = propertyDict.ToJsonString();
                entityInfos.Add(entityInfo);
            }
            return entityInfos.ToArray();
        }

        /// <summary>
        /// 更新实体信息到数据库中
        /// </summary>
        /// <param name="entityInfos">实体信息集合</param>
        protected virtual void UpdateToRepository(TEntityInfo[] entityInfos)
        {
            IRepository<TEntityInfo, TKey> repository = ServiceProvider.GetService<IRepository<TEntityInfo, TKey>>();
            TEntityInfo[] items = repository.TrackEntities.ToArray();

            //删除的实体信息
            TEntityInfo[] removeItems = items.Except(entityInfos,
                EqualityHelper<TEntityInfo>.CreateComparer(m => m.ClassName)).ToArray();
            int removeCount = removeItems.Length;
            repository.UnitOfWork.BeginTransaction();
            int tmpCount = 0;
            foreach (TEntityInfo removeItem in removeItems)
            {
                try
                {
                    removeItem.IsDeleted = true;
                    tmpCount += repository.Delete(removeItem);
                }
                catch (Exception)
                {
                    //无法物理删除，可能是外键约束，改为逻辑删除
                    tmpCount += repository.Recycle(removeItem);
                }
            }
            repository.UnitOfWork.Commit();
            if (tmpCount > 0)
            {
                items = repository.TrackEntities.ToArray();
            }

            repository.UnitOfWork.BeginTransaction();
            //处理新增的实体信息
            TEntityInfo[] addItems = entityInfos.Except(items,
                EqualityHelper<TEntityInfo>.CreateComparer(m => m.ClassName)).ToArray();
            int addCount = addItems.Length;
            repository.Insert(addItems.AsEnumerable());

            //处理更新的实体信息
            int updateCount = 0;
            tmpCount = 0;
            foreach (TEntityInfo item in items)
            {
                bool isUpdate = false;
                TEntityInfo entityInfo = entityInfos.Single(m => m.ClassName == item.ClassName);
                if (item.Name != entityInfo.Name)
                {
                    item.Name = entityInfo.Name;
                    isUpdate = true;
                }
                if (item.PropertyNamesJson != entityInfo.PropertyNamesJson)
                {
                    item.PropertyNamesJson = entityInfo.PropertyNamesJson;
                    isUpdate = true;
                }
                if (isUpdate)
                {
                    tmpCount += repository.Update(item);
                    updateCount++;
                }
            }
            repository.UnitOfWork.Commit();
            if (removeCount > 0 || tmpCount > 0)
            {
                string message = "刷新实体信息";
                if (addCount > 0)
                {
                    message += "，添加实体信息 " + addCount + " 个";
                }
                if (updateCount > 0)
                {
                    message += "，更新实体信息 " + updateCount + " 个";
                }
                if (removeCount > 0)
                {
                    message += "，移除实体信息 " + removeCount + " 个";
                }
                Logger.Info(message);
            }
        }

        /// <summary>
        /// 获取最新实体数据信息
        /// </summary>
        /// <returns></returns>
        protected virtual TEntityInfo[] GetLastestEntityInfos()
        {
            IRepository<TEntityInfo, TKey> repository = ServiceProvider.GetService<IRepository<TEntityInfo, TKey>>();
            if (repository == null)
            {
                return new TEntityInfo[0];
            }
            return repository.Entities.ToArray();
        }
    }
}