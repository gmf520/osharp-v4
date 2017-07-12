using System;
using System.Web.Http;

using OSharp.Web.Http.Filters;


namespace OSharp.Web.Http
{
    /// <summary>
    /// OSharp-WebAPI的控制器基类
    /// </summary>
    [OsharpAuthorize]
    [OperateLogFilter]
    public abstract class BaseApiController : ApiController
    {
        /// <summary>
        /// 获取或设置 依赖注入服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }
    }
}
