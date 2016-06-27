// -----------------------------------------------------------------------
//  <copyright file="IRepository.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-02-06 15:46</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using OSharp.Core.Dependency;
using OSharp.Utility.Data;


namespace OSharp.Core.Data
{
    /// <summary>
    /// 实体仓储模型的数据标准操作
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IRepository<TEntity, TKey> : IScopeDependency 
        where TEntity : IEntity<TKey>
    {
        #region 属性

        /// <summary>
        /// 获取 当前单元操作对象
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// 获取 当前实体类型的查询数据集，数据将使用不跟踪变化的方式来查询，当数据用于展现时，推荐使用此数据集，如果用于新增，更新，删除时，请使用<see cref="TrackEntities"/>数据集
        /// </summary>
        IQueryable<TEntity> Entities { get; }

        /// <summary>
        /// 获取 当前实体类型的查询数据集，当数据用于新增，更新，删除时，使用此数据集，如果数据用于展现，推荐使用<see cref="Entities"/>数据集
        /// </summary>
        IQueryable<TEntity> TrackEntities { get; }

        #endregion

        #region 方法

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        int Insert(TEntity entity);

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        int Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// 以DTO为载体批量插入实体
        /// </summary>
        /// <typeparam name="TInputDto">添加DTO类型</typeparam>
        /// <param name="dtos">添加DTO信息集合</param>
        /// <param name="checkAction">添加信息合法性检查委托</param>
        /// <param name="updateFunc">由DTO到实体的转换委托</param>
        /// <returns>业务操作结果</returns>
        OperationResult Insert<TInputDto>(ICollection<TInputDto> dtos,
            Action<TInputDto> checkAction = null,
            Func<TInputDto, TEntity, TEntity> updateFunc = null)
            where TInputDto : IInputDto<TKey>;

        /// <summary>
        /// 逻辑删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        int Recycle(TEntity entity);

        /// <summary>
        /// 逻辑删除指定编号的实体
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <returns>操作影响的行数</returns>
        int Recycle(TKey key);

        /// <summary>
        /// 逻辑删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        int Recycle(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 批量逻辑删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        int Recycle(IEnumerable<TEntity> entities);

        /// <summary>
        /// 逻辑还原实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        int Restore(TEntity entity);

        /// <summary>
        /// 逻辑还原指定编号的实体
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <returns>操作影响的行数</returns>
        int Restore(TKey key);

        /// <summary>
        /// 逻辑还原所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        int Restore(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 批量逻辑还原实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        int Restore(IEnumerable<TEntity> entities);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        int Delete(TEntity entity);

        /// <summary>
        /// 删除指定编号的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>操作影响的行数</returns>
        int Delete(TKey key);

        /// <summary>
        /// 删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        int Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        int Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// 以标识集合批量删除实体
        /// </summary>
        /// <param name="ids">标识集合</param>
        /// <param name="checkAction">删除前置检查委托</param>
        /// <param name="deleteFunc">删除委托，用于删除关联信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult Delete(ICollection<TKey> ids, Action<TEntity> checkAction = null, Func<TEntity, TEntity> deleteFunc = null);

        /// <summary>
        /// 直接删除指定编号的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns></returns>
        int DeleteDirect(TKey key);

        /// <summary>
        /// 直接删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        int DeleteDirect(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entity">更新后的实体对象</param>
        /// <returns>操作影响的行数</returns>
        int Update(TEntity entity);

        /// <summary>
        /// 以DTO为载体批量更新实体
        /// </summary>
        /// <typeparam name="TEditDto">更新DTO类型</typeparam>
        /// <param name="dtos">更新DTO信息集合</param>
        /// <param name="checkAction">更新信息合法性检查委托</param>
        /// <param name="updateFunc">由DTO到实体的转换委托</param>
        /// <returns>业务操作结果</returns>
        OperationResult Update<TEditDto>(ICollection<TEditDto> dtos,
            Action<TEditDto, TEntity> checkAction = null,
            Func<TEditDto, TEntity, TEntity> updateFunc = null)
            where TEditDto : IInputDto<TKey>;

        /// <summary>
        /// 直接更新指定编号的数据
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <param name="updatExpression">更新属性表达式</param>
        /// <returns>操作影响的行数</returns>
        int UpdateDirect(TKey key, Expression<Func<TEntity, TEntity>> updatExpression);

        /// <summary>
        /// 直接更新指定条件的数据
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="updatExpression">更新属性表达式</param>
        /// <returns>操作影响的行数</returns>
        int UpdateDirect(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updatExpression);

        /// <summary>
        /// 检查实体是否存在
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="id">编辑的实体标识</param>
        /// <returns>是否存在</returns>
        bool CheckExists(Expression<Func<TEntity, bool>> predicate, TKey id = default(TKey));

        /// <summary>
        /// 查找指定主键的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>符合主键的实体，不存在时返回null</returns>
        TEntity GetByKey(TKey key);

        /// <summary>
        /// 查询指定条件的实体
        /// </summary>
        /// <param name="predicate">查询表达式</param>
        /// <returns>符合条件的实体集合</returns>
        [Obsolete("此API即将移除，请使用 TrackEntities 查询数据集 替换此方法的查询")]
        IEnumerable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取贪婪加载导航属性的查询数据集
        /// </summary>
        /// <param name="path">属性表达式，表示要贪婪加载的导航属性</param>
        /// <returns>查询数据集</returns>
        IQueryable<TEntity> GetInclude<TProperty>(Expression<Func<TEntity, TProperty>> path);

        /// <summary>
        /// 获取贪婪加载多个导航属性的查询数据集
        /// </summary>
        /// <param name="paths">要贪婪加载的导航属性名称数组</param>
        /// <returns>查询数据集</returns>
        IQueryable<TEntity> GetIncludes(params string[] paths);

        /// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回此集中的实体。 
        /// 默认情况下，上下文会跟踪返回的实体；可通过对返回的 DbRawSqlQuery 调用 AsNoTracking 来更改此设置。 请注意返回实体的类型始终是此集的类型，而不会是派生的类型。 如果查询的一个或多个表可能包含其他实体类型的数据，则必须编写适当的 SQL 查询以确保只返回适当类型的实体。 与接受 SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。 您可以在 SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。 您提供的任何参数值都将自动转换为 DbParameter。 context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @p0", userSuppliedAuthor); 或者，您还可以构造一个 DbParameter 并将它提供给 SqlQuery。 这允许您在 SQL 查询字符串中使用命名参数。 context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        /// </summary>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <param name="sql">SQL 查询字符串。</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数。 如果使用输出参数，则它们的值在完全读取结果之前不可用。 这是由于 DbDataReader 的基础行为而导致的，有关详细信息，请参见 http://go.microsoft.com/fwlink/?LinkID=398589。</param>
        /// <returns></returns>
        IEnumerable<TEntity> SqlQuery(string sql, bool trackEnabled = true, params object[] parameters);
#if NET45
        /// <summary>
        /// 异步插入实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// 异步批量插入实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        Task<int> InsertAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 异步以DTO为载体批量插入实体
        /// </summary>
        /// <typeparam name="TInputDto">添加DTO类型</typeparam>
        /// <param name="dtos">添加DTO信息集合</param>
        /// <param name="checkAction">添加信息合法性检查委托</param>
        /// <param name="updateFunc">由DTO到实体的转换委托</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> InsertAsync<TInputDto>(ICollection<TInputDto> dtos,
            Func<TInputDto, Task> checkAction = null,
            Func<TInputDto, TEntity, Task<TEntity>> updateFunc = null)
            where TInputDto : IInputDto<TKey>;

        /// <summary>
        /// 异步逻辑删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        Task<int> RecycleAsync(TEntity entity);

        /// <summary>
        /// 异步逻辑删除指定编号的实体
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <returns>操作影响的行数</returns>
        Task<int> RecycleAsync(TKey key);

        /// <summary>
        /// 异步逻辑删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        Task<int> RecycleAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 异步批量逻辑删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        Task<int> RecycleAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 异步逻辑还原实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        Task<int> RestoreAsync(TEntity entity);

        /// <summary>
        /// 异步逻辑还原指定编号的实体
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <returns>操作影响的行数</returns>
        Task<int> RestoreAsync(TKey key);

        /// <summary>
        /// 异步逻辑还原所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        Task<int> RestoreAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 异步批量逻辑还原实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        Task<int> RestoreAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        Task<int> DeleteAsync(TEntity entity);

        /// <summary>
        /// 异步删除指定编号的实体
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <returns>操作影响的行数</returns>
        Task<int> DeleteAsync(TKey key);

        /// <summary>
        /// 异步删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 异步批量删除删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        Task<int> DeleteAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 异步以标识集合批量删除实体
        /// </summary>
        /// <param name="ids">标识集合</param>
        /// <param name="checkAction">删除前置检查委托</param>
        /// <param name="deleteFunc">删除委托，用于删除关联信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteAsync(ICollection<TKey> ids, Func<TEntity, Task> checkAction = null, Func<TEntity, Task<TEntity>> deleteFunc = null);

        /// <summary>
        /// 直接删除指定编号的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns></returns>
        Task<int> DeleteDirectAsync(TKey key);

        /// <summary>
        /// 直接删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        Task<int> DeleteDirectAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 异步更新实体对象
        /// </summary>
        /// <param name="entity">更新后的实体对象</param>
        /// <returns>操作影响的行数</returns>
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// 异步以DTO为载体批量更新实体
        /// </summary>
        /// <typeparam name="TEditDto">更新DTO类型</typeparam>
        /// <param name="dtos">更新DTO信息集合</param>
        /// <param name="checkAction">更新信息合法性检查委托</param>
        /// <param name="updateFunc">由DTO到实体的转换委托</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateAsync<TEditDto>(ICollection<TEditDto> dtos,
            Func<TEditDto, TEntity, Task> checkAction = null,
            Func<TEditDto, TEntity, Task<TEntity>> updateFunc = null)
            where TEditDto : IInputDto<TKey>;

        /// <summary>
        /// 直接更新指定编号的数据
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <param name="updatExpression">更新属性表达式</param>
        /// <returns>操作影响的行数</returns>
        Task<int> UpdateDirectAsync(TKey key, Expression<Func<TEntity, TEntity>> updatExpression);

        /// <summary>
        /// 直接更新指定条件的数据
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="updatExpression">更新属性表达式</param>
        /// <returns>操作影响的行数</returns>
        Task<int> UpdateDirectAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updatExpression);

        /// <summary>
        /// 异步检查实体是否存在
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="id">编辑的实体标识</param>
        /// <returns>是否存在</returns>
        Task<bool> CheckExistsAsync(Expression<Func<TEntity, bool>> predicate, TKey id = default(TKey));

        /// <summary>
        /// 异步查找指定主键的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>符合主键的实体，不存在时返回null</returns>
        Task<TEntity> GetByKeyAsync(TKey key);

        /// <summary>
        /// 异步查询指定条件的实体
        /// </summary>
        /// <param name="predicate">查询表达式</param>
        /// <returns>符合条件的实体集合</returns>
        [Obsolete("此API即将移除，请使用 TrackEntities 查询数据集 替换此方法的查询")]
        Task<IEnumerable<TEntity>> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate);

#endif

        #endregion
    }
}