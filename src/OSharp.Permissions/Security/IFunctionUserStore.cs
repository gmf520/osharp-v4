// -----------------------------------------------------------------------
//  <copyright file="IFunctionUserStore.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-04 13:38</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using OSharp.Core.Dependency;
using OSharp.Core.Security.Dtos;
using OSharp.Utility.Data;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 定义功能用户映射存储
    /// </summary>
    public interface IFunctionUserStore<in TFunctionUserMapInputDto, in TKey, in TFunctionKey, TUserKey> : IScopeDependency
        where TFunctionUserMapInputDto : FunctionUserMapBaseInputDto<TKey, TFunctionKey, TUserKey>
    {
        /// <summary>
        /// 增加功能用户映射信息
        /// </summary>
        /// <param name="dto">功能用户映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> CreateFunctionUserMapAsync(TFunctionUserMapInputDto dto);

        /// <summary>
        /// 编辑功能用户映射信息
        /// </summary>
        /// <param name="dto">功能用户映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateFunctionUserMapAsync(TFunctionUserMapInputDto dto);

        /// <summary>
        /// 删除功能用户映射信息
        /// </summary>
        /// <param name="id">功能用户映射编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteFunctionUserMapAsync(TKey id);
        
    }
}