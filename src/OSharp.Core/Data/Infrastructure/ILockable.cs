// -----------------------------------------------------------------------
//  <copyright file="ILockable.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-21 22:32</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Core.Data
{
    /// <summary>
    /// 定义可锁定功能
    /// </summary>
    public interface ILockable
    {
        /// <summary>
        /// 获取或设置 是否锁定，用于禁用当前信息
        /// </summary>
        bool IsLocked { get; set; }
    }
}