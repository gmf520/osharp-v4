// -----------------------------------------------------------------------
//  <copyright file="Repository.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-19 21:40</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using EntityFramework.Extensions;

using OSharp.Core.Data;
using OSharp.Core.Data.Extensions;
using OSharp.Core.Mapping;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;


namespace OSharp.Data.Entity
{
    /// <summary>
    /// EntityFramework的仓储实现
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// 初始化一个<see cref="Repository{TEntity, TKey}"/>类型的新实例
        /// </summary>
        public Repository(IDbContextTypeResolver contextTypeResolver)
        {
            UnitOfWork = contextTypeResolver.Resolve<TEntity, TKey>();
            _dbSet = ((DbContext)UnitOfWork).Set<TEntity>();
        }

        /// <summary>
        /// 获取 当前单元操作对象
        /// </summary>
        public IUnitOfWork UnitOfWork { get; private set; }

        /// <summary>
        /// 获取 当前实体类型的查询数据集，数据将使用不跟踪变化的方式来查询，当数据用于展现时，推荐使用此数据集，如果用于新增，更新，删除时，请使用<see cref="TrackEntities"/>数据集
        /// </summary>
        public IQueryable<TEntity> Entities
        {
            get { return _dbSet.AsNoTracking(); }
        }

        /// <summary>
        /// 获取 当前实体类型的查询数据集，当数据用于新增，更新，删除时，使用此数据集，如果数据用于展现，推荐使用<see cref="Entities"/>数据集
        /// </summary>
        public IQueryable<TEntity> TrackEntities
        {
            get { return _dbSet; }
        }

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public int Insert(TEntity entity)
        {
            entity.CheckNotNull("entity");
            entity.CheckICreatedAudited<TEntity, TKey>().CheckICreatedTime<TEntity, TKey>();
            _dbSet.Add(entity);
            return UnitOfWork.SaveChanges();
        }

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        public int Insert(IEnumerable<TEntity> entities)
        {
            entities = entities as TEntity[] ?? entities.ToArray();
            foreach (TEntity entity in entities)
            {
                entity.CheckICreatedAudited<TEntity, TKey>().CheckICreatedTime<TEntity, TKey>();
            }
            _dbSet.AddRange(entities);
            return UnitOfWork.SaveChanges();
        }

        /// <summary>
        /// 以DTO为载体批量插入实体
        /// </summary>
        /// <typeparam name="TInputDto">添加DTO类型</typeparam>
        /// <param name="dtos">添加DTO信息集合</param>
        /// <param name="checkAction">添加信息合法性检查委托</param>
        /// <param name="updateFunc">由DTO到实体的转换委托</param>
        /// <returns>业务操作结果</returns>
        public OperationResult Insert<TInputDto>(ICollection<TInputDto> dtos,
            Action<TInputDto> checkAction = null,
            Func<TInputDto, TEntity, TEntity> updateFunc = null)
            where TInputDto : IInputDto<TKey>
        {
            dtos.CheckNotNull("dtos");
            List<string> names = new List<string>();
            foreach (TInputDto dto in dtos)
            {
                try
                {
                    if (checkAction != null)
                    {
                        checkAction(dto);
                    }
                    TEntity entity = dto.MapTo<TEntity>();
                    if (updateFunc != null)
                    {
                        entity = updateFunc(dto, entity);
                    }
                    entity.CheckICreatedAudited<TEntity, TKey>().CheckICreatedTime<TEntity, TKey>();
                    _dbSet.Add(entity);
                }
                catch (Exception e)
                {
                    return new OperationResult(OperationResultType.Error, e.Message);
                }
                string name = GetNameValue(dto);
                if (name != null)
                {
                    names.Add(name);
                }
            }
            int count = UnitOfWork.SaveChanges();
            return count > 0
                ? new OperationResult(OperationResultType.Success,
                    names.Count > 0
                        ? "信息“{0}”添加成功".FormatWith(names.ExpandAndToString())
                        : "{0}个信息添加成功".FormatWith(dtos.Count))
                : new OperationResult(OperationResultType.NoChanged);
        }

        /// <summary>
        /// 逻辑删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public int Recycle(TEntity entity)
        {
            entity.CheckNotNull("entity");
            entity.CheckIRecycle<TEntity, TKey>(RecycleOperation.LogicDelete);
            return Update(entity);
        }

        /// <summary>
        /// 逻辑删除指定编号的实体
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <returns>操作影响的行数</returns>
        public int Recycle(TKey key)
        {
            CheckEntityKey(key, "key");
            TEntity entity = _dbSet.Find(key);
            return entity == null ? 0 : Recycle(entity);
        }

        /// <summary>
        /// 逻辑删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        public int Recycle(Expression<Func<TEntity, bool>> predicate)
        {
            predicate.CheckNotNull("predicate");
            TEntity[] entities = _dbSet.Where(predicate).ToArray();
            return Recycle(entities);
        }

        /// <summary>
        /// 批量逻辑删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        public int Recycle(IEnumerable<TEntity> entities)
        {
            entities = entities as TEntity[] ?? entities.ToArray();
            foreach (TEntity entity in entities)
            {
                entity.CheckIRecycle<TEntity, TKey>(RecycleOperation.LogicDelete);
                ((DbContext)UnitOfWork).Update<TEntity, TKey>(entity);
            }
            return UnitOfWork.SaveChanges();
        }

        /// <summary>
        /// 逻辑还原实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public int Restore(TEntity entity)
        {
            entity.CheckNotNull("entity");
            entity.CheckIRecycle<TEntity, TKey>(RecycleOperation.Restore);
            return Update(entity);
        }

        /// <summary>
        /// 逻辑还原指定编号的实体
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <returns>操作影响的行数</returns>
        public int Restore(TKey key)
        {
            CheckEntityKey(key, "key");
            TEntity entity = _dbSet.Find(key);
            return entity == null ? 0 : Restore(entity);
        }

        /// <summary>
        /// 逻辑还原所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        public int Restore(Expression<Func<TEntity, bool>> predicate)
        {
            predicate.CheckNotNull("predicate");
            TEntity[] entities = _dbSet.Where(predicate).ToArray();
            return Restore(entities);
        }

        /// <summary>
        /// 批量逻辑还原实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        public int Restore(IEnumerable<TEntity> entities)
        {
            entities = entities as TEntity[] ?? entities.ToArray();
            foreach (TEntity entity in entities)
            {
                entity.CheckIRecycle<TEntity, TKey>(RecycleOperation.Restore);
                ((DbContext)UnitOfWork).Update<TEntity, TKey>(entity);
            }
            return UnitOfWork.SaveChanges();
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public int Delete(TEntity entity)
        {
            entity.CheckNotNull("entity");
            entity.CheckIRecycle<TEntity, TKey>(RecycleOperation.PhysicalDelete);
            _dbSet.Remove(entity);
            return UnitOfWork.SaveChanges();
        }

        /// <summary>
        /// 删除指定编号的实体
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <returns>操作影响的行数</returns>
        public virtual int Delete(TKey key)
        {
            CheckEntityKey(key, "key");
            TEntity entity = _dbSet.Find(key);
            return entity == null ? 0 : Delete(entity);
        }

        /// <summary>
        /// 删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            predicate.CheckNotNull("predicate");
            TEntity[] entities = _dbSet.Where(predicate).ToArray();
            return entities.Length == 0 ? 0 : Delete(entities);
        }

        /// <summary>
        /// 批量删除删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        public int Delete(IEnumerable<TEntity> entities)
        {
            entities = entities as TEntity[] ?? entities.ToArray();
            foreach (TEntity entity in entities)
            {
                entity.CheckIRecycle<TEntity, TKey>(RecycleOperation.PhysicalDelete);
            }
            _dbSet.RemoveRange(entities);
            return UnitOfWork.SaveChanges();
        }

        /// <summary>
        /// 以标识集合批量删除实体
        /// </summary>
        /// <param name="ids">标识集合</param>
        /// <param name="checkAction">删除前置检查委托</param>
        /// <param name="deleteFunc">删除委托，用于删除关联信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult Delete(ICollection<TKey> ids, Action<TEntity> checkAction = null, Func<TEntity, TEntity> deleteFunc = null)
        {
            ids.CheckNotNull("ids");
            List<string> names = new List<string>();
            foreach (TKey id in ids)
            {
                TEntity entity = _dbSet.Find(id);
                try
                {
                    if (checkAction != null)
                    {
                        checkAction(entity);
                    }
                    if (deleteFunc != null)
                    {
                        entity = deleteFunc(entity);
                    }
                    entity.CheckIRecycle<TEntity, TKey>(RecycleOperation.PhysicalDelete);
                    _dbSet.Remove(entity);
                }
                catch (Exception e)
                {
                    return new OperationResult(OperationResultType.Error, e.Message);
                }
                string name = GetNameValue(entity);
                if (name != null)
                {
                    names.Add(name);
                }
            }
            int count = UnitOfWork.SaveChanges();
            return count > 0
                ? new OperationResult(OperationResultType.Success,
                    names.Count > 0
                        ? "信息“{0}”删除成功".FormatWith(names.ExpandAndToString())
                        : "{0}个信息删除成功".FormatWith(ids.Count))
                : new OperationResult(OperationResultType.NoChanged);
        }

        /// <summary>
        /// 直接删除指定编号的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns></returns>
        public int DeleteDirect(TKey key)
        {
            CheckEntityKey(key, "key");
            return DeleteDirect(m => m.Id.Equals(key));
        }

        /// <summary>
        /// 直接删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        public int DeleteDirect(Expression<Func<TEntity, bool>> predicate)
        {
            predicate.CheckNotNull("predicate");
            return _dbSet.Where(predicate).Delete();
        }

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entity">更新后的实体对象</param>
        /// <returns>操作影响的行数</returns>
        public int Update(TEntity entity)
        {
            entity.CheckNotNull("entity");
            ((DbContext)UnitOfWork).Update<TEntity, TKey>(entity);
            return UnitOfWork.SaveChanges();
        }

        /// <summary>
        /// 以DTO为载体批量更新实体
        /// </summary>
        /// <typeparam name="TEditDto">更新DTO类型</typeparam>
        /// <param name="dtos">更新DTO信息集合</param>
        /// <param name="checkAction">更新信息合法性检查委托</param>
        /// <param name="updateFunc">由DTO到实体的转换委托</param>
        /// <returns>业务操作结果</returns>
        public OperationResult Update<TEditDto>(ICollection<TEditDto> dtos,
            Action<TEditDto, TEntity> checkAction = null,
            Func<TEditDto, TEntity, TEntity> updateFunc = null)
            where TEditDto : IInputDto<TKey>
        {
            dtos.CheckNotNull("dtos");
            List<string> names = new List<string>();
            foreach (TEditDto dto in dtos)
            {
                try
                {
                    TEntity entity = _dbSet.Find(dto.Id);
                    if (entity == null)
                    {
                        return new OperationResult(OperationResultType.QueryNull);
                    }
                    if (checkAction != null)
                    {
                        checkAction(dto, entity);
                    }
                    entity = dto.MapTo(entity);
                    if (updateFunc != null)
                    {
                        entity = updateFunc(dto, entity);
                    }
                    ((DbContext)UnitOfWork).Update<TEntity, TKey>(entity);
                }
                catch (Exception e)
                {
                    return new OperationResult(OperationResultType.Error, e.Message);
                }
                string name = GetNameValue(dto);
                if (name != null)
                {
                    names.Add(name);
                }
            }
            int count = UnitOfWork.SaveChanges();
            return count > 0
                ? new OperationResult(OperationResultType.Success,
                    names.Count > 0
                        ? "信息“{0}”更新成功".FormatWith(names.ExpandAndToString())
                        : "{0}个信息更新成功".FormatWith(dtos.Count))
                : new OperationResult(OperationResultType.NoChanged);
        }

        /// <summary>
        /// 直接更新指定编号的数据
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <param name="updatExpression">更新属性表达式</param>
        /// <returns>操作影响的行数</returns>
        public int UpdateDirect(TKey key, Expression<Func<TEntity, TEntity>> updatExpression)
        {
            CheckEntityKey(key, "key");
            updatExpression.CheckNotNull("updatExpression");
            return UpdateDirect(m => m.Id.Equals(key), updatExpression);
        }

        /// <summary>
        /// 直接更新指定条件的数据
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="updatExpression">更新属性表达式</param>
        /// <returns>操作影响的行数</returns>
        public int UpdateDirect(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updatExpression)
        {
            predicate.CheckNotNull("predicate");
            updatExpression.CheckNotNull("updatExpression");
            
            return _dbSet.Where(predicate).Update(updatExpression);
        }

        /// <summary>
        /// 检查实体是否存在
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="id">编辑的实体标识</param>
        /// <returns>是否存在</returns>
        public bool CheckExists(Expression<Func<TEntity, bool>> predicate, TKey id = default(TKey))
        {
            predicate.CheckNotNull("predicate");
            TKey defaultId = default(TKey);
            var entity = _dbSet.Where(predicate).Select(m => new { m.Id }).FirstOrDefault();
            bool exists = (!typeof(TKey).IsValueType && ReferenceEquals(id, null)) || id.Equals(defaultId)
                ? entity != null
                : entity != null && !entity.Id.Equals(id);
            return exists;
        }

        /// <summary>
        /// 查找指定主键的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>符合主键的实体，不存在时返回null</returns>
        public TEntity GetByKey(TKey key)
        {
            CheckEntityKey(key, "key");
            return _dbSet.Find(key);
        }

        /// <summary>
        /// 查询指定条件的实体
        /// </summary>
        /// <param name="predicate">查询表达式</param>
        /// <returns>符合条件的实体集合</returns>
        [Obsolete("此API即将移除，请使用 TrackEntities 查询数据集 替换此方法的查询")]
        public IEnumerable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> predicate)
        {
            predicate.CheckNotNull("predicate");
            return _dbSet.Where(predicate).ToList();
        }

        /// <summary>
        /// 获取贪婪加载导航属性的查询数据集
        /// </summary>
        /// <param name="path">属性表达式，表示要贪婪加载的导航属性</param>
        /// <returns>查询数据集</returns>
        public IQueryable<TEntity> GetInclude<TProperty>(Expression<Func<TEntity, TProperty>> path)
        {
            path.CheckNotNull("path");
            return _dbSet.Include(path);
        }

        /// <summary>
        /// 获取贪婪加载多个导航属性的查询数据集
        /// </summary>
        /// <param name="paths">要贪婪加载的导航属性名称数组</param>
        /// <returns>查询数据集</returns>
        public IQueryable<TEntity> GetIncludes(params string[] paths)
        {
            paths.CheckNotNull("paths");
            IQueryable<TEntity> source = _dbSet;
            foreach (string path in paths)
            {
                source = source.Include(path);
            }
            return source;
        }

        /// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回此集中的实体。 
        /// 默认情况下，上下文会跟踪返回的实体；可通过对返回的 DbRawSqlQuery 调用 AsNoTracking 来更改此设置。 请注意返回实体的类型始终是此集的类型，而不会是派生的类型。 如果查询的一个或多个表可能包含其他实体类型的数据，则必须编写适当的 SQL 查询以确保只返回适当类型的实体。 与接受 SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。 您可以在 SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。 您提供的任何参数值都将自动转换为 DbParameter。 context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @p0", userSuppliedAuthor); 或者，您还可以构造一个 DbParameter 并将它提供给 SqlQuery。 这允许您在 SQL 查询字符串中使用命名参数。 context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        /// </summary>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <param name="sql">SQL 查询字符串。</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数。 如果使用输出参数，则它们的值在完全读取结果之前不可用。 这是由于 DbDataReader 的基础行为而导致的，有关详细信息，请参见 http://go.microsoft.com/fwlink/?LinkID=398589。</param>
        /// <returns></returns>
        public IEnumerable<TEntity> SqlQuery(string sql, bool trackEnabled = true, params object[] parameters)
        {
            return trackEnabled
                ? _dbSet.SqlQuery(sql, parameters)
                : _dbSet.SqlQuery(sql, parameters).AsNoTracking();
        }

        /// <summary>
        /// 异步插入实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> InsertAsync(TEntity entity)
        {
            entity.CheckNotNull("entity");
            entity.CheckICreatedAudited<TEntity, TKey>().CheckICreatedTime<TEntity, TKey>();
            _dbSet.Add(entity);
            return await UnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 异步批量插入实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> InsertAsync(IEnumerable<TEntity> entities)
        {
            entities = entities as TEntity[] ?? entities.ToArray();
            foreach (TEntity entity in entities)
            {
                entity.CheckICreatedAudited<TEntity, TKey>().CheckICreatedTime<TEntity, TKey>();
            }
            _dbSet.AddRange(entities);
            return await UnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 异步以DTO为载体批量插入实体
        /// </summary>
        /// <typeparam name="TInputDto">添加DTO类型</typeparam>
        /// <param name="dtos">添加DTO信息集合</param>
        /// <param name="checkAction">添加信息合法性检查委托</param>
        /// <param name="updateFunc">由DTO到实体的转换委托</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> InsertAsync<TInputDto>(ICollection<TInputDto> dtos,
            Func<TInputDto, Task> checkAction = null,
            Func<TInputDto, TEntity, Task<TEntity>> updateFunc = null)
            where TInputDto : IInputDto<TKey>
        {
            dtos.CheckNotNull("dtos");
            List<string> names = new List<string>();
            foreach (TInputDto dto in dtos)
            {
                try
                {
                    if (checkAction != null)
                    {
                        await checkAction(dto);
                    }
                    TEntity entity = dto.MapTo<TEntity>();
                    if (updateFunc != null)
                    {
                        entity = await updateFunc(dto, entity);
                    }
                    entity.CheckICreatedAudited<TEntity, TKey>().CheckICreatedTime<TEntity, TKey>();
                    _dbSet.Add(entity);
                }
                catch (Exception e)
                {
                    return new OperationResult(OperationResultType.Error, e.Message);
                }
                string name = GetNameValue(dto);
                if (name != null)
                {
                    names.Add(name);
                }
            }
            int count = await UnitOfWork.SaveChangesAsync();
            return count > 0
                ? new OperationResult(OperationResultType.Success,
                    names.Count > 0
                        ? "信息“{0}”添加成功".FormatWith(names.ExpandAndToString())
                        : "{0}个信息添加成功".FormatWith(dtos.Count))
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 异步逻辑删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> RecycleAsync(TEntity entity)
        {
            entity.CheckNotNull("entity");
            entity.CheckIRecycle<TEntity, TKey>(RecycleOperation.LogicDelete);
            return await UpdateAsync(entity);
        }

        /// <summary>
        /// 异步逻辑删除指定编号的实体
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> RecycleAsync(TKey key)
        {
            CheckEntityKey(key, "key");
            TEntity entity = _dbSet.Find(key);
            return entity == null ? 0 : await RecycleAsync(entity);
        }

        /// <summary>
        /// 异步逻辑删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> RecycleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            predicate.CheckNotNull("predicate");
            TEntity[] entities = _dbSet.Where(predicate).ToArray();
            return await RecycleAsync(entities);
        }

        /// <summary>
        /// 异步批量逻辑删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> RecycleAsync(IEnumerable<TEntity> entities)
        {
            entities = entities as TEntity[] ?? entities.ToArray();
            foreach (TEntity entity in entities)
            {
                entity.CheckIRecycle<TEntity, TKey>(RecycleOperation.LogicDelete);
                ((DbContext)UnitOfWork).Update<TEntity, TKey>(entity);
            }
            return await UnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 异步逻辑还原实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> RestoreAsync(TEntity entity)
        {
            entity.CheckNotNull("entity");
            entity.CheckIRecycle<TEntity, TKey>(RecycleOperation.Restore);
            return await UpdateAsync(entity);
        }

        /// <summary>
        /// 异步逻辑还原指定编号的实体
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> RestoreAsync(TKey key)
        {
            CheckEntityKey(key, "key");
            TEntity entity = _dbSet.Find(key);
            return entity == null ? 0 : await RestoreAsync(entity);
        }

        /// <summary>
        /// 异步逻辑还原所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> RestoreAsync(Expression<Func<TEntity, bool>> predicate)
        {
            predicate.CheckNotNull("predicate");
            TEntity[] entities = _dbSet.Where(predicate).ToArray();
            return await RestoreAsync(entities);
        }

        /// <summary>
        /// 异步批量逻辑还原实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> RestoreAsync(IEnumerable<TEntity> entities)
        {
            entities = entities as TEntity[] ?? entities.ToArray();
            foreach (TEntity entity in entities)
            {
                entity.CheckIRecycle<TEntity, TKey>(RecycleOperation.Restore);
                ((DbContext)UnitOfWork).Update<TEntity, TKey>(entity);
            }
            return await UnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> DeleteAsync(TEntity entity)
        {
            entity.CheckNotNull("entity");
            entity.CheckIRecycle<TEntity, TKey>(RecycleOperation.PhysicalDelete);
            _dbSet.Remove(entity);
            return await UnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 异步删除指定编号的实体
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> DeleteAsync(TKey key)
        {
            CheckEntityKey(key, "key");
            TEntity entity = await _dbSet.FindAsync(key);
            return entity == null ? 0 : await DeleteAsync(entity);
        }

        /// <summary>
        /// 异步删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            predicate.CheckNotNull("predicate");
            TEntity[] entities = await _dbSet.Where(predicate).ToArrayAsync();
            return entities.Length == 0 ? 0 : await DeleteAsync(entities);
        }

        /// <summary>
        /// 异步批量删除删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> DeleteAsync(IEnumerable<TEntity> entities)
        {
            entities = entities as TEntity[] ?? entities.ToArray();
            foreach (TEntity entity in entities)
            {
                entity.CheckIRecycle<TEntity, TKey>(RecycleOperation.PhysicalDelete);
            }
            _dbSet.RemoveRange(entities);
            return await UnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 异步以标识集合批量删除实体
        /// </summary>
        /// <param name="ids">标识集合</param>
        /// <param name="checkAction">删除前置检查委托</param>
        /// <param name="deleteFunc">删除委托，用于删除关联信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> DeleteAsync(ICollection<TKey> ids,
            Func<TEntity, Task> checkAction = null,
            Func<TEntity, Task<TEntity>> deleteFunc = null)
        {
            ids.CheckNotNull("ids");
            List<string> names = new List<string>();
            foreach (TKey id in ids)
            {
                TEntity entity = await _dbSet.FindAsync(id);
                try
                {
                    if (checkAction != null)
                    {
                        await checkAction(entity);
                    }
                    if (deleteFunc != null)
                    {
                        entity = await deleteFunc(entity);
                    }
                    entity.CheckIRecycle<TEntity, TKey>(RecycleOperation.PhysicalDelete);
                    _dbSet.Remove(entity);
                }
                catch (Exception e)
                {
                    return new OperationResult(OperationResultType.Error, e.Message);
                }
                string name = GetNameValue(entity);
                if (name != null)
                {
                    names.Add(name);
                }
            }
            int count = await UnitOfWork.SaveChangesAsync();
            return count > 0
                ? new OperationResult(OperationResultType.Success,
                    names.Count > 0
                        ? "信息“{0}”删除成功".FormatWith(names.ExpandAndToString())
                        : "{0}个信息删除成功".FormatWith(ids.Count))
                : new OperationResult(OperationResultType.NoChanged);
        }

        /// <summary>
        /// 直接删除指定编号的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns></returns>
        public async Task<int> DeleteDirectAsync(TKey key)
        {
            CheckEntityKey(key, "key");
            return await DeleteDirectAsync(m => m.Id.Equals(key));
        }

        /// <summary>
        /// 直接删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> DeleteDirectAsync(Expression<Func<TEntity, bool>> predicate)
        {
            predicate.CheckNotNull("predicate");
            return await _dbSet.Where(predicate).DeleteAsync();
        }

        /// <summary>
        /// 异步更新实体对象
        /// </summary>
        /// <param name="entity">更新后的实体对象</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> UpdateAsync(TEntity entity)
        {
            entity.CheckNotNull("entity");
            ((DbContext)UnitOfWork).Update<TEntity, TKey>(entity);
            return await UnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 异步以DTO为载体批量更新实体
        /// </summary>
        /// <typeparam name="TEditDto">更新DTO类型</typeparam>
        /// <param name="dtos">更新DTO信息集合</param>
        /// <param name="checkAction">更新信息合法性检查委托</param>
        /// <param name="updateFunc">由DTO到实体的转换委托</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> UpdateAsync<TEditDto>(ICollection<TEditDto> dtos,
            Func<TEditDto, TEntity, Task> checkAction = null,
            Func<TEditDto, TEntity, Task<TEntity>> updateFunc = null)
            where TEditDto : IInputDto<TKey>
        {
            dtos.CheckNotNull("dtos");
            List<string> names = new List<string>();
            foreach (TEditDto dto in dtos)
            {
                try
                {
                    TEntity entity = await _dbSet.FindAsync(dto.Id);
                    if (entity == null)
                    {
                        return new OperationResult(OperationResultType.QueryNull);
                    }
                    if (checkAction != null)
                    {
                        await checkAction(dto, entity);
                    }
                    entity = dto.MapTo(entity);
                    if (updateFunc != null)
                    {
                        entity = await updateFunc(dto, entity);
                    }
                    ((DbContext)UnitOfWork).Update<TEntity, TKey>(entity);
                }
                catch (Exception e)
                {
                    return new OperationResult(OperationResultType.Error, e.Message);
                }
                string name = GetNameValue(dto);
                if (name != null)
                {
                    names.Add(name);
                }
            }
            int count = await UnitOfWork.SaveChangesAsync();
            return count > 0
                ? new OperationResult(OperationResultType.Success,
                    names.Count > 0
                        ? "信息“{0}”更新成功".FormatWith(names.ExpandAndToString())
                        : "{0}个信息更新成功".FormatWith(dtos.Count))
                : new OperationResult(OperationResultType.NoChanged);
        }

        /// <summary>
        /// 直接更新指定编号的数据
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <param name="updatExpression">更新属性表达式</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> UpdateDirectAsync(TKey key, Expression<Func<TEntity, TEntity>> updatExpression)
        {
            CheckEntityKey(key, "key");
            updatExpression.CheckNotNull("updatExpression");
            return await UpdateDirectAsync(m => m.Id.Equals(key), updatExpression);
        }

        /// <summary>
        /// 直接更新指定条件的数据
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="updatExpression">更新属性表达式</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> UpdateDirectAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updatExpression)
        {
            predicate.CheckNotNull("predicate");
            updatExpression.CheckNotNull("updatExpression");
            return await _dbSet.Where(predicate).UpdateAsync(updatExpression);
        }

        /// <summary>
        /// 异步检查实体是否存在
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="id">编辑的实体标识</param>
        /// <returns>是否存在</returns>
        public async Task<bool> CheckExistsAsync(Expression<Func<TEntity, bool>> predicate, TKey id = default(TKey))
        {
            predicate.CheckNotNull("predicate");
            TKey defaultId = default(TKey);
            var entity = await _dbSet.Where(predicate).Select(m => new { m.Id }).FirstOrDefaultAsync();
            bool exists = (!(typeof(TKey).IsValueType) && ReferenceEquals(id, null)) || id.Equals(defaultId)
                ? entity != null
                : entity != null && !entity.Id.Equals(id);
            return exists;
        }

        /// <summary>
        /// 异步查找指定主键的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>符合主键的实体，不存在时返回null</returns>
        public async Task<TEntity> GetByKeyAsync(TKey key)
        {
            CheckEntityKey(key, "key");
            return await _dbSet.FindAsync(key);
        }

        /// <summary>
        /// 异步查询指定条件的实体
        /// </summary>
        /// <param name="predicate">查询表达式</param>
        /// <returns>符合条件的实体集合</returns>
        [Obsolete("此API即将移除，请使用 TrackEntities 查询数据集 替换此方法的查询")]
        public async Task<IEnumerable<TEntity>> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate)
        {
            predicate.CheckNotNull("predicate");
            return await _dbSet.Where(predicate).ToListAsync();
        }

        #region 私有方法

        private static void CheckEntityKey(object key, string keyName)
        {
            key.CheckNotNull("key");
            keyName.CheckNotNull("keyName");

            Type type = key.GetType();
            if (type == typeof(int))
            {
                ((int)key).CheckGreaterThan(keyName, 0);
            }
            else if (type == typeof(string))
            {
                ((string)key).CheckNotNullOrEmpty(keyName);
            }
            else if (type == typeof(Guid))
            {
                ((Guid)key).CheckNotEmpty(keyName);
            }
        }

        private static string GetNameValue(object value)
        {
            dynamic obj = value;
            try
            {
                return obj.Name;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}