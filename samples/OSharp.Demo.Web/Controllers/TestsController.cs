﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Claims;
using System.Web.Mvc;

using OSharp.Core;
using OSharp.Core.Caching;
using OSharp.Core.Context;
using OSharp.Core.Data;
using OSharp.Data.Entity;
using OSharp.Core.Dependency;
using OSharp.Core.Extensions;
using OSharp.Core.Logging;
using OSharp.Core.Security;
using OSharp.Demo.Contracts;
using OSharp.Demo.Identity;
using OSharp.Demo.Models.Identity;
using OSharp.Utility.Extensions;
using OSharp.Web.Mvc;
using OSharp.Web.Mvc.Extensions;
using OSharp.Web.Mvc.Logging;


namespace OSharp.Demo.Web.Controllers
{
    [OperateLogFilter]
    [Description("网站-测试")]
    public class TestsController : BaseController
    {
        public IServiceProvider ServiceProvider { get; set; }

        public IDbContextTypeResolver ContextTypeResolver { get; set; }

        public ITestContract TestContract { get; set; }

        public IIocResolver IocResolver { get; set; }

        [Description("测试-首页")]
        public ActionResult Index()
        {
            return View();
        }

        [Description("测试-缓存测试")]
        public ActionResult TestCacher()
        {
            DateTime dt = DateTime.Now;
            ICache cache = CacheManager.GetCacher<TestsController>();
            const string key = "KEY__fdsaf";
            IFunction function = this.GetExecuteFunction(ServiceProvider);
            DateTime dt1 = cache.Get<DateTime>(key);
            if (dt1 == DateTime.MinValue)
            {
                cache.Set(key, dt, function);
                dt1 = dt;
            }
            return Content("实际时间：{0}，缓存时间：{1}".FormatWith(dt, dt1));
        }

        public ActionResult TestClaims()
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            ViewBag.Identity = identity;
            return View();
        }

        public ActionResult TestIoc()
        {
            const string format = "{0}: {1}";
            List<string>lines = new List<string>()
            {
                format.FormatWith("ServiceProvider", ServiceProvider.GetHashCode()),
                format.FormatWith("DefaultDbContext", ServiceProvider.GetService<DefaultDbContext>().GetHashCode()),
                format.FormatWith("DefaultDbContext", ServiceProvider.GetService<DefaultDbContext>().GetHashCode()),
                format.FormatWith("IRepository<User,int>", ServiceProvider.GetService<IRepository<User,int>>().GetHashCode()),
                format.FormatWith("IRepository<User,int>", ServiceProvider.GetService<IRepository<User,int>>().GetHashCode())
            };
            return Content(lines.ExpandAndToString("<br>"));
        }
    }
}