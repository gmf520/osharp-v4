using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using Hangfire;

using OSharp.Core.Dependency;
using OSharp.Core.Security;


namespace OSharp.Autofac.Hangfire.Initialize
{
    public class HangfireAutofacIocBuilder : IocBuilderBase
    {
        /// <summary>
        /// 初始化一个<see cref="IocBuilderBase"/>类型的新实例
        /// </summary>
        /// <param name="services">服务信息集合</param>
        public HangfireAutofacIocBuilder(IServiceCollection services)
            : base(services)
        { }

        #region Overrides of IocBuilderBase

        /// <summary>
        /// 添加自定义服务映射
        /// </summary>
        /// <param name="services">服务信息集合</param>
        protected override void AddCustomTypes(IServiceCollection services)
        {
            services.AddInstance(this);
            services.AddSingleton<IIocResolver, HangfireIocResolver>();
            services.AddSingleton<IFunctionHandler, NullFunctionHandler>();
        }

        /// <summary>
        /// 重写以实现构建服务并设置各个平台的Resolver
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        /// <param name="assemblies">要检索的程序集集合</param>
        /// <returns>服务提供者</returns>
        protected override IServiceProvider BuildAndSetResolver(IServiceCollection services, Assembly[] assemblies)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Populate(services);
            IContainer container = builder.Build();
            JobActivator activator = new AutofacJobActivator(container);
            JobActivator.Current = activator;
            HangfireIocResolver.LifetimeResolveFunc = type =>
            {
                JobActivatorScope scope = CallContext.LogicalGetData(AutofacJobActivator.LifetimeScopeKey) as JobActivatorScope;
                if (scope == null)
                {
                    return null;
                }
                return scope.Resolve(type);
            };
            return (IServiceProvider)activator.ActivateJob(typeof(IServiceProvider));
        }

        #endregion
    }
}
