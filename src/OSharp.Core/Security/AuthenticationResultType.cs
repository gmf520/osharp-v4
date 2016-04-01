// -----------------------------------------------------------------------
//  <copyright file="AuthenticationResultType.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-03 18:16</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 表示权限验证结果的枚举
    /// </summary>
    public enum AuthenticationResultType
    {
        /// <summary>
        /// 权限检查通过
        /// </summary>
        [Description("权限检查通过。")] Allowed,

        /// <summary>
        /// 该操作需要登录后才能继续进行
        /// </summary>
        [Description("该操作需要登录后才能继续进行。")] LoggedOut,

        /// <summary>
        /// 权限不足
        /// </summary>
        [Description("当前用户权限不足，不能继续操作。")] PurviewLack,

        /// <summary>
        /// 功能锁定
        /// </summary>
        [Description("当前功能已经被锁定，不能继续操作。")] FunctionLocked,

        /// <summary>
        /// 功能不存在
        /// </summary>
        [Description("指定功能不存在。")] FunctionNotFound,

        /// <summary>
        /// 出现错误
        /// </summary>
        [Description("权限检查出现错误")]
        Error
    }
}