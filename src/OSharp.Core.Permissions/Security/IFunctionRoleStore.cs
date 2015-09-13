// -----------------------------------------------------------------------
//  <copyright file="IFunctionRoleStore.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-04 12:16</last-date>
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
    /// 定义功能角色映射存储
    /// </summary>
    public interface IFunctionRoleStore<in TFunctionRoleMapDto, in TKey, in TFunctionKey, TRoleKey>
        where TFunctionRoleMapDto : FunctionRoleMapBaseDto<TKey, TFunctionKey, TRoleKey>
    {
        /// <summary>
        /// 增加功能角色映射信息
        /// </summary>
        /// <param name="dto">功能角色映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> AddFunctionRoleMapAsync(TFunctionRoleMapDto dto);

        /// <summary>
        /// 编辑功能角色映射信息
        /// </summary>
        /// <param name="dto">功能角色映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> EditFunctionRoleMapAsync(TFunctionRoleMapDto dto);

        /// <summary>
        /// 删除功能角色映射信息
        /// </summary>
        /// <param name="id">功能角色映射编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteFunctionRoleMapAsync(TKey id);

        /// <summary>
        /// 获取功能的角色及其限制类型
        /// </summary>
        /// <param name="functionId">功能编号</param>
        /// <returns>角色及其限制类型的集合</returns>
        Task<IEnumerable<Tuple<string, FilterType>>> GetRolesAsync(TFunctionKey functionId);

        /// <summary>
        /// 验证功能是否允许访问
        /// </summary>
        /// <param name="functionId">功能编号</param>
        /// <param name="roleNames">要验证的角色名</param>
        /// <returns>是否允许访问</returns>
        Task<bool> IsRolesVisiteEnabledAsync(TFunctionKey functionId, params string[] roleNames);
    }
}