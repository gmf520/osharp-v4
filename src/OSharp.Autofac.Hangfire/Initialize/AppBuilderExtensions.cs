using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hangfire;

using OSharp.Core;
using OSharp.Core.Dependency;
using OSharp.Utility;

using Owin;


namespace OSharp.Autofac.Hangfire.Initialize
{
    /// <summary>
    /// <see cref="IAppBuilder"/>初始化扩展
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// 初始化OSharp框架的Hangfire功能
        /// </summary>
        public static IAppBuilder UseOSharpHangfile(this IAppBuilder app, IIocBuilder iocBuilder)
        {
            iocBuilder.CheckNotNull("iocBuilder");
            IFrameworkInitializer initializer = new FrameworkInitializer();
            initializer.Initialize(iocBuilder);
            return app;
        }
    }
}
