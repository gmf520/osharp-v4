// -----------------------------------------------------------------------
//  <copyright file="OSharpWebApiContext.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>liuxx001</last-editor>
//  <last-date>2015-12-29 1:09</last-date>
// -----------------------------------------------------------------------

using System;
using System.Web.Http.Dependencies;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace OSharp.Web.Http.Context
{
    /// <summary>
    /// OSharp框架Webapi上下文，用于获取当前HttpRequestMessage
    /// </summary>
    [Serializable]
    public class OSharpWebApiContext
    {        
        /// <summary>
        /// 获取 当前请求上下文
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

        private IDependencyScope _dependencyScope;
        /// <summary>
        /// 获取 设置当前请求的依赖注入容器
        /// </summary>
        public IDependencyScope DependencyScope
        {
            get
            {
                return _dependencyScope;
            }
            set
            {
                _dependencyScope = value;
            }
        }
    }
}