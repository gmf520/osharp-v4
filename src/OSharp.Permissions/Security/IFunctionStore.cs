﻿// -----------------------------------------------------------------------
//  <copyright file="IFunctionStore.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-13 14:22</last-date>
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
    /// 定义功能信息存储
    /// </summary>
    /// <typeparam name="TFunction">功能信息类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    /// <typeparam name="TFunctionInputDto">功能信息输入DTO</typeparam>
    public interface IFunctionStore<TFunction, in TFunctionKey, in TFunctionInputDto>
        where TFunction : IFunction, IEntity<TFunctionKey>
        where TFunctionInputDto : FunctionBaseInputDto<TFunctionKey>
        where TFunctionKey : IEquatable<TFunctionKey>
    {
        /// <summary>
        /// 获取 功能信息查询数据集
        /// </summary>
        IQueryable<TFunction> Functions { get; }

        /// <summary>
        /// 检查功能信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的功能信息编号</param>
        /// <returns>功能信息是否存在</returns>
        Task<bool> CheckFunctionExists(Expression<Func<TFunction, bool>> predicate, TFunctionKey id = default(TFunctionKey));

        /// <summary>
        /// 添加功能信息信息
        /// </summary>
        /// <param name="dto">要添加的功能信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> CreateFunction(TFunctionInputDto dto);

        /// <summary>
        /// 更新功能信息信息
        /// </summary>
        /// <param name="dto">包含更新信息的功能信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateFunction(TFunctionInputDto dto);

        /// <summary>
        /// 删除功能信息信息
        /// </summary>
        /// <param name="id">要删除的功能信息编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteFunction(TFunctionKey id);
    }
}