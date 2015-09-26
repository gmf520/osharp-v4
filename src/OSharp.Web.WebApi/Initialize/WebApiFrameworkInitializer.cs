// -----------------------------------------------------------------------
//  <copyright file="WebApiFrameworkInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-23 21:48</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data.Entity;
using OSharp.Core.Initialize;
using OSharp.Core.Security;


namespace OSharp.Web.Http.Initialize
{
    /// <summary>
    /// WebApi框架初始化器
    /// </summary>
    public class WebApiFrameworkInitializer : FrameworkInitializerBase
    {
        /// <summary>
        /// 初始化一个<see cref="WebApiFrameworkInitializer"/>类型的新实例
        /// </summary>
        public WebApiFrameworkInitializer()
        {
            PlatformToken = PlatformToken.WebApi;
            DataConfigReseter = new DataConfigReseter();
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