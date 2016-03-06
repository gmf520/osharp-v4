using System;
using System.Web.Http;


namespace OSharp.Web.Http
{
    /// <summary>
    /// WebAPI的控制器基类
    /// </summary>
    //[CustomAuthorize]
    public abstract class BaseApiController : ApiController
    {
        /// <summary>
        /// 获取或设置 依赖注入服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }
    }
}
