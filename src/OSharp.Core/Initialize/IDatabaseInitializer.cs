// -----------------------------------------------------------------------
//  <copyright file="IDbContextsInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-29 12:26</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Configs;
using OSharp.Core.Dependency;


namespace OSharp.Core.Initialize
{
    /// <summary>
    /// 定义数据库初始化器，从程序集中反射实体映射类并加载到相应上下文类中，进行上下文类型的初始化
    /// </summary>
    public interface IDatabaseInitializer : ISingletonDependency
    {
        /// <summary>
        /// 开始初始化数据库
        /// </summary>
        /// <param name="config">数据库配置信息</param>
        void Initialize(DataConfig config);
    }
}