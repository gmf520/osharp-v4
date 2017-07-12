// -----------------------------------------------------------------------
//  <copyright file="IEntityInfoStore.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-13 14:27</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using OSharp.Core.Data;
using OSharp.Utility.Data;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 定义实体数据信息存储
    /// </summary>
    /// <typeparam name="TEntityInfo">实体数据信息类型</typeparam>
    /// <typeparam name="TEntityInfoKey">实体数据信息编号类型</typeparam>
    /// <typeparam name="TEntityInfoInputDto">实体数据输入DTO</typeparam>
    public interface IEntityInfoStore<TEntityInfo, in TEntityInfoKey, in TEntityInfoInputDto>
        where TEntityInfo : IEntityInfo, IEntity<TEntityInfoKey>
        where TEntityInfoInputDto : EntityInfoBaseInputDto<TEntityInfoKey>
        where TEntityInfoKey : IEquatable<TEntityInfoKey>
    {
        #region 实体数据信息业务

        /// <summary>
        /// 获取 实体数据信息查询数据集
        /// </summary>
        IQueryable<TEntityInfo> EntityInfos { get; }

        /// <summary>
        /// 检查实体数据信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的实体数据信息编号</param>
        /// <returns>实体数据信息是否存在</returns>
        Task<bool> CheckEntityInfoExists(Expression<Func<TEntityInfo, bool>> predicate, TEntityInfoKey id = default(TEntityInfoKey));

        /// <summary>
        /// 更新实体数据信息信息
        /// </summary>
        /// <param name="dto">包含更新信息的实体数据信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateEntityInfo(TEntityInfoInputDto dto);

        #endregion
    }
}