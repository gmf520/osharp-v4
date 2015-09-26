// -----------------------------------------------------------------------
//  <copyright file="LocalFrameworkInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-25 12:39</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Configs;
using OSharp.Core.Data.Entity;
using OSharp.Core.Initialize;
using OSharp.Core.Security;


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
        public LocalFrameworkInitializer()
        {
            PlatformToken = PlatformToken.Local;
            DataConfigReseter = new DataConfigReseter();
            DatabaseInitializer = new DatabaseInitializer();
            EntityInfoHandler = new EntityInfoHandler()
            {
                IocResolver = new IocResolver()
            };
            FunctionHandler = new NullFunctionHandler()
            {
                IocResolver = new IocResolver()
            };
        }
    }
}