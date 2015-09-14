using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Mvc;

using OSharp.Core;
using OSharp.Core.Caching;
using OSharp.Core.Data;
using OSharp.Core.Data.Entity;
using OSharp.Core.Dependency;
using OSharp.Core.Exceptions;
using OSharp.Core.Logging;
using OSharp.Core.Security;
using OSharp.Demo.Contracts;
using OSharp.Demo.Identity;
using OSharp.Demo.Models.Identity;
using OSharp.SiteBase.Extensions;
using OSharp.SiteBase.Logging;
using OSharp.Utility.Extensions;
using OSharp.Web.Mvc;


namespace OSharp.Demo.Web.Controllers
{
    [OperateLogFilter]
    [Description("网站-测试")]
    public class TestsController : BaseController
    {
        public IDbContextTypeResolver ContextTypeResolver { get; set; }

        public ITestContract TestContract { get; set; }

        public IIocResolver IocResolver { get; set; }

        [Description("测试-首页")]
        public ActionResult Index()
        {
            DateTime dt = DateTime.Now;
            ICache cache = CacheManager.GetCacher<TestsController>();
            const string key = "KEY__fdsaf";
            IFunction function = this.GetExecuteFunction();
            DateTime dt1 = cache.Get<DateTime>(key);
            if (dt1 == DateTime.MinValue)
            {
                cache.Set(key, dt, function);
                dt1 = dt;
            }
            return Content("实际时间：{0}，缓存时间：{1}".FormatWith(dt, dt1));
            return new EmptyResult();
        }

        [Description("测试-测试01")]
        public ActionResult Test01()
        {
            List<Type> entityTypes = new List<Type>()
            {
                typeof(User),
                typeof(Role),
                typeof(Function),
                typeof(OperateLog),
                typeof(DataLog)
            };
            List<string>strs = new List<string>();
            foreach (Type entityType in entityTypes)
            {
                IUnitOfWork uow = ContextTypeResolver.Resolve(entityType);
                strs.Add("{0}: {1} - {2}".FormatWith(entityType.FullName, uow.GetType().FullName, uow.GetHashCode()));
            }
            return Content(strs.ExpandAndToString("<br/>"));
        }

        [Description("测试-测试02")]
        public ActionResult Test02()
        {
            TestContract.Test();
            return Content("end");
        }

        [HttpPost]
        public ActionResult Test03Ajax(List<int> ids)
        {
            return Json(ids);
        }

        public ActionResult Test03()
        {
            return View();
        }
    }
}