// -----------------------------------------------------------------------
//  <copyright file="IAudited.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-21 14:56</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Core.Data
{
    /// <summary>
    /// 表示审计属性，包含<see cref="ICreatedAudited"/>与<see cref="IUpdateAudited"/>
    /// </summary>
    public interface IAudited : ICreatedAudited, IUpdateAudited
    { }
}