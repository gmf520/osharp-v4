// -----------------------------------------------------------------------
//  <copyright file="EntityInfo.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-14 3:28</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;

using OSharp.Utility.Data;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 实体类——实体信息
    /// </summary>
    [Description("权限-实体信息")]
    public class EntityInfo : EntityInfoBase<Guid>
    {
        /// <summary>
        /// 初始化一个<see cref="EntityInfo"/>类型的新实例
        /// </summary>
        public EntityInfo()
        {
            Id = CombHelper.NewComb();
        }
    }
}