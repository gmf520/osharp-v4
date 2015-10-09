// -----------------------------------------------------------------------
//  <copyright file="SignalRInitializeOptions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-29 19:58</last-date>
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


namespace OSharp.Web.SignalR.Initialize
{
    /// <summary>
    /// SignalR初始化选项设置
    /// </summary>
    public class SignalRInitializeOptions : InitializeOptionsBase
    {
        /// <summary>
        /// 初始化一个<see cref="SignalRInitializeOptions"/>类型的新实例
        /// </summary>
        public SignalRInitializeOptions(IBasicLoggingInitializer basicLoggingInitializer, IIocInitializer iocInitializer)
        {
            basicLoggingInitializer.CheckNotNull("basicLoggingInitializer");
            iocInitializer.CheckNotNull("iocInitializer");

            PlatformToken = PlatformToken.SignalR;
            DataConfigReseter = new DataConfigReseter();
            ServicesBuilder = new ServicesBuilder();
            DatabaseInitializer = new DatabaseInitializer();
            EntityInfoHandler = new EntityInfoHandler();
            FunctionHandler = new SignalRFunctionHandler();
            BasicLoggingInitializer = basicLoggingInitializer;
            IocInitializer = iocInitializer;
        }
    }
}