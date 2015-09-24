// -----------------------------------------------------------------------
//  <copyright file="SignalRFrameworkInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-25 0:51</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data.Entity;
using OSharp.Core.Initialize;
using OSharp.Core.Security;


namespace OSharp.Web.SignalR.Initialize
{
    /// <summary>
    /// SignalR框架初始化
    /// </summary>
    public class SignalRFrameworkInitializer : FrameworkInitializerBase
    {
        /// <summary>
        /// 初始化一个<see cref="SignalRFrameworkInitializer"/>类型的新实例
        /// </summary>
        public SignalRFrameworkInitializer()
        {
            DatabaseInitializer = new DatabaseInitializer();
            EntityInfoHandler = new EntityInfoHandler()
            {
                IocResolver = new IocResolver()
            };
            FunctionHandler = new FunctionHandler()
            {
                IocResolver = new IocResolver()
            };
        }
    }
}