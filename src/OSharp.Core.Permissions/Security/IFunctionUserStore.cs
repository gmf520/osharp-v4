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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Security.Dtos;
using OSharp.Utility.Data;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 定义功能用户映射存储
    /// </summary>
    public interface IFunctionUserStore<in TFunctionUserMapDto, in TKey, in TFunctionKey, TUserKey>
        where TFunctionUserMapDto : FunctionUserMapBaseDto<TKey, TFunctionKey, TUserKey>
    {
        /// <summary>
        /// 增加功能用户映射信息
        /// </summary>
        /// <param name="dto">功能用户映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> AddFunctionUserMapAsync(TFunctionUserMapDto dto);

        /// <summary>
        /// 编辑功能用户映射信息
        /// </summary>
        /// <param name="dto">功能用户映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> EditFunctionUserMapAsync(TFunctionUserMapDto dto);

        /// <summary>
        /// 删除功能用户映射信息
        /// </summary>
        /// <param name="id">功能用户映射编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteFunctionUserMapAsync(TKey id);

        /// <summary>
        /// 获取功能的用户及其限制类型
        /// </summary>
        /// <param name="functionId">功能编号</param>
        /// <returns>用户及其限制类型的集合</returns>
        Task<IEnumerable<Tuple<string, FilterType>>> GetUsersAsync(TFunctionKey functionId);

        /// <summary>
        /// 验证功能是否允许访问
        /// </summary>
        /// <param name="functionId">功能编号</param>
        /// <param name="userNames">要验证的用户名</param>
        /// <returns>是否允许访问</returns>
        Task<bool> IsUsersVisiteEnabledAsync(TFunctionKey functionId, params string[] userNames);
    }
}