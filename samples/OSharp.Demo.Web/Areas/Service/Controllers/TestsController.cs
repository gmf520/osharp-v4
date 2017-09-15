// -----------------------------------------------------------------------
//  <copyright file="TestsController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-24 12:06</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

using OSharp.Core.Data;
using OSharp.Core.Dependency;
using OSharp.Core.Security;
using OSharp.Demo.Contracts;
using OSharp.Demo.Models.Identity;
using OSharp.Utility.Extensions;
using OSharp.Web.Http.Extensions;
using OSharp.Web.Http.Filters;
using OSharp.Web.Http.Logging;


namespace OSharp.Demo.Web.Areas.Service.Controllers
{
    [Description("服务-测试")]
    [OperateLogFilter]
    public class TestsController : ApiController
    {
        public IIdentityContract IdentityContract { get; set; }

        /// <summary>
        /// 获取或设置 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        [Description("服务-测试-匿名数据")]
        [HttpGet]
        public IHttpActionResult Test01()
        {
            IFunction function = ControllerContext.Request.GetExecuteFunction(ServiceProvider);
            string name = function != null ? function.Name : null;

            return Ok("Hello World.{0}".FormatWith(name));
        }

        [HttpGet]
        [Authorize]
        [Description("服务-测试-保护数据")]
        public IHttpActionResult Test02()
        {
            return Ok("受保护的数据");
        }

        public IUnitOfWork UnitOfWork { get; set; }

        [HttpGet]
        [AllowAnonymous]
        [Description("服务-测试-测试03")]
        public IHttpActionResult Test03()
        {
            return Ok(IdentityContract.Roles.Select(m => new { m.Name }).First());
        }
    }
}