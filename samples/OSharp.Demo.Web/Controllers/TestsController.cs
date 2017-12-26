using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using OSharp.Core.Caching;
using OSharp.Core.Data;
using OSharp.Data.Entity;
using OSharp.Core.Dependency;
using OSharp.Core.Extensions;
using OSharp.Core.Security;
using OSharp.Demo.Contracts;
using OSharp.Demo.Dtos.OAuth;
using OSharp.Demo.Identity;
using OSharp.Demo.Models.Identity;
using OSharp.Demo.OAuth;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;
using OSharp.Web.Mvc;
using OSharp.Web.Mvc.Extensions;
using OSharp.Web.Mvc.Filters;


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
                format.FormatWith("IRepository<User,int>", ServiceProvider.GetService<IRepository<User,int>>().GetHashCode()),
                format.FormatWith("UserManager", ServiceProvider.GetService<UserManager<User,int>>().GetHashCode()),
                format.FormatWith("UserManager", ServiceProvider.GetService<UserManager>().GetHashCode()),
            };
            return Content(lines.ExpandAndToString("<br>"));
        }

        public async Task<ActionResult> Test1()
        {
            OAuthClientStore oAuthClientStore = ServiceProvider.GetService<OAuthClientStore>();
            OperationResult result = null;
            //ClientInputDto clientDto = new ClientInputDto()
            //{
            //    Name = "测试客户端01",
            //    ClientType = ClientType.Application,
            //    Url = "http://localhost:10240",
            //    LogoUrl = "http://localhost:10240",
            //    RedirectUrl = "http://localhost:10240"
            //};
            //result = await clientStore.AddClient(clientDto);
            OAuthClientSecretInputDto secretDto = new OAuthClientSecretInputDto()
            {
                Type = "Test Type",
                Remark = "Remark",
                ClientId = 2
            };
            result = await oAuthClientStore.CreateClientSecret(secretDto);
            return Content(result.Message);
        }

        public async Task<ActionResult> Test2()
        {
            UserManager manager = ServiceProvider.GetService<UserManager>();
            IdentityResult result = await manager.AddPasswordAsync(1, "gmf31529019");
            return Content(result.ToJsonString());
        }

        public ActionResult Test3()
        {
            return null;
        }
    }
}