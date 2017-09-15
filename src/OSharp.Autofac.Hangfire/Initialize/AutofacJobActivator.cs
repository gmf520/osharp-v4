using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using Hangfire;

using OSharp.Utility;


namespace OSharp.Autofac.Hangfire.Initialize
{
    /// <summary>
    /// Hangfile-Job工作J对象创建器
    /// </summary>
    public class AutofacJobActivator : JobActivator
    {
        /// <summary>
        /// Tag used in setting up per-job lifetime scope registrations.
        /// </summary>
        public static readonly object LifetimeScopeTag = "BackgroundJobScope";
        public const string LifetimeScopeKey = "osharp:hangfire_lifetime_scope";

        private readonly ILifetimeScope _lifetimeScope;
        private readonly bool _useTaggedLifetimeScope;

        /// <summary>
        /// 初始化一个<see cref="AutofacJobActivator"/>类型的新实例
        /// </summary>
        public AutofacJobActivator(ILifetimeScope lifetimeScope, bool useTaggedLifetimeScope = true)
        {
            lifetimeScope.CheckNotNull("lifetimeScope" );
            _lifetimeScope = lifetimeScope;
            _useTaggedLifetimeScope = useTaggedLifetimeScope;
        }
        
        public override object ActivateJob(Type jobType)
        {
            return _lifetimeScope.Resolve(jobType);
        }
        
        /// <summary>
        /// 开始一个生命周期作用域
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override JobActivatorScope BeginScope(JobActivatorContext context)
        {
            ILifetimeScope lifetimeScope = _useTaggedLifetimeScope
                ? _lifetimeScope.BeginLifetimeScope(LifetimeScopeTag)
                : _lifetimeScope.BeginLifetimeScope();
            JobActivatorScope scope = new AutofacJobActivatorScope(lifetimeScope);
            CallContext.LogicalSetData(LifetimeScopeKey, scope);
            return scope;
        }
        
        /// <summary>
        /// 生命周期作用域
        /// </summary>
        private class AutofacJobActivatorScope : JobActivatorScope
        {
            private readonly ILifetimeScope _lifetimeScope;

            public AutofacJobActivatorScope(ILifetimeScope lifetimeScope)
            {
                _lifetimeScope = lifetimeScope;
            }
            
            public override object Resolve(Type type)
            {
                return _lifetimeScope.Resolve(type);
            }
            
            public override void DisposeScope()
            {
                _lifetimeScope.Dispose();
            }
        }
    }
}
