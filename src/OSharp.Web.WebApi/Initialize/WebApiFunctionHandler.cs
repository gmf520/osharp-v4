﻿// -----------------------------------------------------------------------
//  <copyright file="WebApiFunctionHandler.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-09 16:15</last-date>
// -----------------------------------------------------------------------

using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;

using OSharp.Core.Security;
using OSharp.Utility;
using OSharp.Utility.Extensions;
using OSharp.Web.Http.Properties;
using System.Collections.Generic;

namespace OSharp.Web.Http.Initialize
{
    /// <summary>
    /// WebApi功能信息处理器
    /// </summary>
    public class WebApiFunctionHandler : FunctionHandlerBase<Function, Guid>
    {
        /// <summary>
        /// 获取 功能技术提供者，如Mvc/WebApi/SignalR等，用于区分功能来源，各技术更新功能时，只更新属于自己技术的功能
        /// </summary>
        protected override PlatformToken PlatformToken
        {
            get { return PlatformToken.WebApi; }
        }

        /// <summary>
        /// 重写以实现从类型信息创建功能信息
        /// </summary>
        /// <param name="type">类型信息</param>
        /// <returns></returns>
        protected override Function GetFunction(Type type)
        {
            if (!typeof(ApiController).IsAssignableFrom(type))
            {
                throw new InvalidOperationException(Resources.ActionMethodInfoFinder_TypeNotApiControllerType.FormatWith(type.FullName));
            }

            FunctionType controllerFunctionType = type.HasAttribute<LoginedAttribute>() || type.HasAttribute<AuthorizeAttribute>()
                ? FunctionType.Logined
                : (type.HasAttribute<RoleLimitAttribute>()
                    ? FunctionType.RoleLimit
                    : FunctionType.Anonymouse);

            Function function = new Function()
            {
                Name = type.ToDescription(),
                Area = GetArea(type),
                Controller = type.Name.Replace("ApiController", string.Empty).Replace("Controller", string.Empty),
                IsController = true,
                FunctionType = controllerFunctionType,
                PlatformToken = PlatformToken
            };
            return function;
        }

        /// <summary>
        /// 重写以实现从方法信息创建功能信息
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        protected override Function GetFunction(MethodInfo method)
        {
            if (!typeof(IHttpActionResult).IsAssignableFrom(method.ReturnType)
                && (!method.ReturnType.IsGenericType || method.ReturnType.GetGenericTypeDefinition() != typeof(Task<>)
                    || !typeof(IHttpActionResult).IsAssignableFrom(method.ReturnType.GetGenericArguments()[0])))
            { }
            
            Type type = method.DeclaringType;
            if (type == null)
            {
                throw new InvalidOperationException(Resources.FunctionHandler_DefindActionTypeIsNull.FormatWith(method.Name));
            }

            FunctionType controllerFunctionType = type.HasAttribute<LoginedAttribute>() || type.HasAttribute<AuthorizeAttribute>()
                ? FunctionType.Logined
                : (type.HasAttribute<RoleLimitAttribute>()
                    ? FunctionType.RoleLimit
                    : FunctionType.Anonymouse);

            FunctionType actionFunctionType = method.HasAttribute<LoginedAttribute>() || method.HasAttribute<AuthorizeAttribute>()
                ? FunctionType.Logined
                : method.HasAttribute<AllowAnonymousAttribute>()
                    ? FunctionType.Anonymouse
                    : method.HasAttribute<RoleLimitAttribute>()
                        ? FunctionType.RoleLimit
                        : controllerFunctionType;

            Function function = new Function()
            {
                Name = method.ToDescription(),
                Area = GetArea(type),
                Controller = type.Name.Replace("ApiController", string.Empty).Replace("Controller", string.Empty),
                Action = method.Name,
                FunctionType = actionFunctionType,
                PlatformToken = PlatformToken,
                IsController = false,
                IsAjax = false,
                IsChild = false
            };
            return function;
        }

        /// <summary>
        /// 重写以实现从类型中获取功能的区域信息
        /// </summary>
        protected override string GetArea(Type type)
        {
            type.Required<Type, InvalidOperationException>(m => typeof(ApiController).IsAssignableFrom(m) && !m.IsAbstract,
                Resources.ActionMethodInfoFinder_TypeNotApiControllerType.FormatWith(type.FullName));
            string @namespace = type.Namespace;
            if (@namespace == null)
            {
                return null;
            }
            int index = @namespace.IndexOf("Areas", StringComparison.Ordinal) + 6;
            string area = index > 6 ? @namespace.Substring(index, @namespace.IndexOf(".Controllers", StringComparison.Ordinal) - index) : null;
            return area;
        }

        /// <summary>
        /// 重写以实现是否忽略指定方法的功能信息
        /// </summary>
        /// <param name="action">要判断的功能信息</param>
        /// <param name="method">功能相关的方法信息</param>
        /// <param name="functions">已存在的功能信息集合</param>
        /// <returns></returns>
        protected override bool IsIgnoreMethod(Function action, MethodInfo method, IEnumerable<Function> functions)
        {
            bool flag = base.IsIgnoreMethod(action, method, functions);
            return flag && method.HasAttribute<HttpPostAttribute>();
        }
    }
}