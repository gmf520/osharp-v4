// -----------------------------------------------------------------------
//  <copyright file="NullEntityInfoHandler.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-21 19:39</last-date>
// -----------------------------------------------------------------------

using System;


namespace OSharp.Core.Security
{ 
    /// <summary>
    /// 空的实体信息处理器，用于不需要收集实体信息的场景
    /// </summary> 
    public class NullEntityInfoHandler : EntityInfoHandlerBase<EntityInfo, Guid>
    {
        /// <summary>
        /// 从实体类型中获取实体信息集合
        /// </summary>
        /// <param name="entityTypes">实体类型集合</param>
        /// <returns>实体信息集合</returns>
        protected override EntityInfo[] GetEntityInfos(Type[] entityTypes)
        {
            return new EntityInfo[0];
        }

        /// <summary>
        /// 获取最新实体数据信息
        /// </summary>
        /// <returns></returns>
        protected override EntityInfo[] GetLastestEntityInfos()
        {
            return new EntityInfo[0];
        }

        /// <summary>
        /// 更新实体信息到数据库中
        /// </summary>
        /// <param name="entityInfos">实体信息集合</param>
        protected override void UpdateToRepository(EntityInfo[] entityInfos)
        { }
    }
}