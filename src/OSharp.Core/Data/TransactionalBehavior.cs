// -----------------------------------------------------------------------
//  <copyright file="TransactionalBehavior.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-18 14:13</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Core.Data
{
    /// <summary>
    /// 在执行数据库命令或查询期间控制事务创建行为。
    /// </summary>
    public enum TransactionalBehavior
    {
        /// <summary>
        /// 如果存在现有事务，则使用它，否则在没有事务的情况下执行命令或查询。
        /// </summary>
        DoNotEnsureTransaction,

        /// <summary>
        /// 如果不存在任何事务，则使用新事务进行操作。
        /// </summary>
        EnsureTransaction
    }
}