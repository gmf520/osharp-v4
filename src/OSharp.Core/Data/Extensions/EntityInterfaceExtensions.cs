using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using OSharp.Core.Context;
using OSharp.Core.Exceptions;
using OSharp.Utility;
using OSharp.Utility.Extensions;


namespace OSharp.Core.Data.Extensions
{
    /// <summary>
    /// 实体接口相关扩展
    /// </summary>
    public static class EntityInterfaceExtensions
    {
        /// <summary>
        /// 检测并执行<see cref="IAudited"/>接口的逻辑
        /// </summary>
        /// <param name="entity">要检测的实体信息</param>
        public static TEntity CheckICreatedTime<TEntity, TKey>(this TEntity entity) where TEntity : IEntity<TKey>
        {
            if (!(entity is ICreatedTime))
            {
                return entity;
            }
            ICreatedTime entity1 = entity as ICreatedTime;
            entity1.CreatedTime = DateTime.Now;
            return (TEntity)entity1;
        }

        /// <summary>
        /// 检测并执行<see cref="ICreatedAudited"/>接口的逻辑
        /// </summary>
        /// <param name="entity">要检测的实体信息</param>
        public static TEntity CheckICreatedAudited<TEntity, TKey>(this TEntity entity) where TEntity : IEntity<TKey>
        {
            if (!(entity is ICreatedAudited))
            {
                return entity;
            }
            ICreatedAudited entity1 = entity as ICreatedAudited;
            ClaimsIdentity identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                entity1.CreatorUserId = identity.GetClaimValue(ClaimTypes.NameIdentifier);
            }
            entity1.CreatedTime = DateTime.Now;
            return (TEntity)entity1;
        }

        /// <summary>
        /// 检测并执行<see cref="IUpdateAudited"/>接口的逻辑
        /// </summary>
        /// <param name="entity">要检测的实体信息</param>
        public static TEntity CheckIUpdateAudited<TEntity, TKey>(this TEntity entity) where TEntity : IEntity<TKey>
        {
            if (!(entity is IUpdateAudited))
            {
                return entity;
            }
            IUpdateAudited entity1 = entity as IUpdateAudited;
            ClaimsIdentity identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                entity1.LastUpdatorUserId = identity.GetClaimValue(ClaimTypes.NameIdentifier);
            }
            entity1.LastUpdatedTime = DateTime.Now;
            return (TEntity)entity1;
        }

        /// <summary>
        /// 检测并执行<see cref="IRecycle"/>接口的逻辑
        /// </summary>
        /// <param name="entity">要检测的实体信息</param>
        /// <param name="operation">回收站操作类型</param>
        public static TEntity CheckIRecycle<TEntity, TKey>(this TEntity entity, RecycleOperation operation)
            where TEntity : IEntity<TKey>
        {
            if (!(entity is IRecycle))
            {
                return entity;
            }
            IRecycle entity1 = entity as IRecycle;
            switch (operation)
            {
                case RecycleOperation.LogicDelete:
                    if (entity1.IsDeleted)
                    {
                        throw new InvalidOperationException("数据已是回收状态，不能逻辑删除");
                    }
                    entity1.IsDeleted = true;
                    break;
                case RecycleOperation.Restore:
                    if (!entity1.IsDeleted)
                    {
                        throw new InvalidOperationException("数据不处于回收状态，不能还原");
                    }
                    entity1.IsDeleted = false;
                    break;
                case RecycleOperation.PhysicalDelete:
                    if (!entity1.IsDeleted)
                    {
                        throw new InvalidOperationException("数据不处于回收状态，不能永久删除");
                    }
                    break;
            }
            return (TEntity)entity1;
        }

        /// <summary>
        /// 判断指定类型是否为<see cref="IEntity{TKey}"/>实体类型
        /// </summary>
        /// <param name="type">要判断的类型</param>
        /// <returns></returns>
        public static bool IsEntityType(this Type type)
        {
            type.CheckNotNull("type");
            return typeof(IEntity<>).IsGenericAssignableFrom(type) && !type.IsAbstract;
        }
    }
}
