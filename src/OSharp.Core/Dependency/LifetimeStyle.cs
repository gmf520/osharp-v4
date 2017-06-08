// -----------------------------------------------------------------------
//  <copyright file="LifetimeStyle.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-06 19:29</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Core.Dependency
{
    /// <summary>
    /// 表示依赖注入的对象生命周期
    /// </summary>
    public enum LifetimeStyle
    {
        /// <summary>
        /// 实时模式，每次获取都创建不同对象
        /// </summary>
        Transient,

        /// <summary>
        /// 局部模式，同一生命周期获得相同对象，不同生命周期获得不同对象（PerRequest）
        /// </summary>
        Scoped,

        /// <summary>
        /// 单例模式，在第一次获取实例时创建，之后都获得相同对象
        /// </summary>
        Singleton
    }
}