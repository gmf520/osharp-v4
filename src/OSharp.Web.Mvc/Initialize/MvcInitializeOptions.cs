// -----------------------------------------------------------------------
//  <copyright file="MvcInitializeOptions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-29 19:48</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data.Entity;
using OSharp.Core.Dependency;
using OSharp.Core.Initialize;
using OSharp.Core.Security;
using OSharp.Utility;


namespace OSharp.Web.Mvc.Initialize
{
    /// <summary>
    /// Mvc初始化选项设置
    /// </summary>
    public class MvcInitializeOptions : InitializeOptionsBase
    {
        /// <summary>
        /// 初始化一个<see cref="MvcInitializeOptions"/>类型的新实例
        /// </summary>
        public MvcInitializeOptions(IBasicLoggingInitializer basicLoggingInitializer, IIocInitializer iocInitializer)
        {
            basicLoggingInitializer.CheckNotNull("basicLoggingInitializer");
            iocInitializer.CheckNotNull("iocInitializer");

            PlatformToken = PlatformToken.Mvc;
            DataConfigReseter = new DataConfigReseter();
            ServicesBuilder = new ServicesBuilder();
            DatabaseInitializer = new DatabaseInitializer();
            BasicLoggingInitializer = basicLoggingInitializer;
            IocInitializer = iocInitializer;
        }
    }
}