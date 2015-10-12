// -----------------------------------------------------------------------
//  <copyright file="AjaxResultType.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-05 9:24</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Web.Mvc.UI
{
    /// <summary>
    /// 表示 ajax 操作结果类型的枚举
    /// </summary>
    public enum AjaxResultType
    {
        /// <summary>
        /// 消息结果类型
        /// </summary>
        Info,

        /// <summary>
        /// 成功结果类型
        /// </summary>
        Success,

        /// <summary>
        /// 警告结果类型
        /// </summary>
        Warning,

        /// <summary>
        /// 异常结果类型
        /// </summary>
        Error
    }
}