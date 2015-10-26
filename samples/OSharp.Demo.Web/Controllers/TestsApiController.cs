using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using OSharp.Core.Security;
using OSharp.Utility.Extensions;
using OSharp.Web.Http.Extensions;


namespace OSharp.Demo.Web.Controllers
{
    public class TestsApiController : ApiController
    {
        /// <summary>
        /// 获取或设置 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        [Description("服务-测试-测试002")]
        [HttpGet]
        public IHttpActionResult Test01()
        {
            try
            {
                IFunction function = ControllerContext.Request.GetExecuteFunction(ServiceProvider);
                string name = function != null ? function.Name : null;

                return Ok("Hello World.{0}".FormatWith(name));
            }
            catch (Exception ex)
            {
                return Ok("Hello World.{0}".FormatWith(ex.Message));
            }
        }

    }
}
