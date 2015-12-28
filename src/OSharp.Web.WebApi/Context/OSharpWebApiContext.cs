// -----------------------------------------------------------------------
//  <copyright file="OSharpWebApiContext.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>liuxx001</last-editor>
//  <last-date>2015-12-29 1:09</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Security.Claims;
using OSharp.Core.Security;
using OSharp.Utility;
using OSharp.Core.Dependency;
using System.Web.Http.Dependencies;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace OSharp.Web.Http.Context
{
    /// <summary>
    /// OSharp框架Webapi上下文，用于获取当前HttpRequestMessage
    /// </summary>
    [Serializable]
    public class OSharpWebApiContext : Dictionary<string, object>
    {
        private const string ScopeKey = "__OSharp_WebApi_Context_DependencyScope";
        private static readonly Lazy<OSharpWebApiContext> ContextLazy = new Lazy<OSharpWebApiContext>(() => new OSharpWebApiContext());
        
        /// <summary>
        /// 获取 当前上下文
        /// </summary>
        public static OSharpWebApiContext Current
        {
            get
            {
                return CallContext.HostContext as OSharpWebApiContext;
            }
            [SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
            set
            {
                CallContext.HostContext = value;
            }
        }

        /// <summary>
        /// 获取 当前操作者
        /// </summary>
        public IDependencyScope DependencyScope
        {
            get
            {
                if (!ContainsKey(ScopeKey)) return null;
                return this[ScopeKey] as IDependencyScope;
            }
            private set
            {
                this[ScopeKey] = value;
            }
        }

        /// <summary>
        /// 设置当前请求依赖注入容器
        /// </summary>
        public void SetDependencyScope(IDependencyScope scope)
        {
            if (!ContainsKey(ScopeKey))
            {
                this[ScopeKey] = scope;
            }
        }
    }
}