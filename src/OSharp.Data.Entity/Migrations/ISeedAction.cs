// -----------------------------------------------------------------------
//  <copyright file="ISeedAction.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>最后修改人</last-editor>
//  <last-date>2015-06-28 17:12</last-date>
// -----------------------------------------------------------------------

using System.Data.Entity;


namespace OSharp.Data.Entity.Migrations
{
    /// <summary>
    /// 初始化种子数据接口
    /// </summary>
    public interface ISeedAction
    {
        /// <summary>
        /// 获取 操作排序，数值越小越先执行
        /// </summary>
        int Order { get; }

        /// <summary>
        /// 定义种子数据初始化过程
        /// </summary>
        /// <param name="context">数据上下文</param>
        void Action(DbContext context);
    }
}