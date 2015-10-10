// -----------------------------------------------------------------------
//  <copyright file="LocalFrameworkInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-09 18:12</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Dependency;
using OSharp.Core.Initialize;


namespace OSharp.App.Local.Initialize
{
    /// <summary>
    /// 本地程序初始化类
    /// </summary>
    public class LocalFrameworkInitializer : FrameworkInitializerBase
    {
        /// <summary>
        /// 初始化一个<see cref="LocalFrameworkInitializer"/>类型的新实例
        /// </summary>
        public LocalFrameworkInitializer(LocalInitializeOptions options)
            : base(options)
        { }

        /// <summary>
        /// 获取 依赖注入解析器
        /// </summary>
        protected override IIocResolver IocResolver
        {
            get { return new LocalIocResolver(); }
        }
    }
}