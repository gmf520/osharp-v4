// -----------------------------------------------------------------------
//  <copyright file="ICreatedAudited.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-21 14:35</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Core.Data
{
    /// <summary>
    /// 给信息添加 创建时间、创建者 属性，在实体创建时，将自动提取当前用户为创建者
    /// </summary>
    public interface ICreatedAudited : ICreatedTime
    {
        /// <summary>
        /// 获取或设置 创建者编号
        /// </summary>
        string CreatorUserId { get; set; }
    }
}