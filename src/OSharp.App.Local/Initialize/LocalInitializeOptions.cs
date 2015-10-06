// -----------------------------------------------------------------------
//  <copyright file="LocalInitializeOptions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-29 21:30</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data.Entity;
using OSharp.Core.Initialize;
using OSharp.Core.Security;
using OSharp.Utility;


namespace OSharp.App.Local.Initialize
{
    /// <summary>
    /// 本地程序初始化选项
    /// </summary>
    public class LocalInitializeOptions : InitializeOptionsBase
    {
        /// <summary>
        /// 初始化一个<see cref="LocalInitializeOptions"/>类型的新实例
        /// </summary>
        public LocalInitializeOptions(IBasicLoggingInitializer basicLoggingInitializer, IIocInitializer iocInitializer)
        {
            basicLoggingInitializer.CheckNotNull("basicLoggingInitializer");
            iocInitializer.CheckNotNull("iocInitializer");

            PlatformToken = PlatformToken.Local;
            DataConfigReseter = new DataConfigReseter();
            DatabaseInitializer = new DatabaseInitializer();
            EntityInfoHandler = new EntityInfoHandler()
            {
                IocResolver = new LocalIocResolver()
            };
            FunctionHandler = new NullFunctionHandler()
            {
                IocResolver = new LocalIocResolver()
            };
            BasicLoggingInitializer = basicLoggingInitializer;
            IocInitializer = iocInitializer;
        }
    }
}