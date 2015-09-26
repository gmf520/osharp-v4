// -----------------------------------------------------------------------
//  <copyright file="FunctionHandler.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-25 0:32</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR.Hubs;

using OSharp.Core.Reflection;
using OSharp.Core.Security;
using OSharp.Utility.Extensions;
using OSharp.Web.SignalR.Properties;


namespace OSharp.Web.SignalR.Initialize
{
    /// <summary>
    /// SignalR功能处理器
    /// </summary>
    public class FunctionHandler : FunctionHandlerBase<Function, Guid>
    {
        /// <summary>
        /// 获取或设置 控制器类型查找器
        /// </summary>
        protected override ITypeFinder TypeFinder
        {
            get { return new HubTypeFinder();}
        }

        /// <summary>
        /// 获取或设置 功能查找器
        /// </summary>
        protected override IMethodInfoFinder MethodInfoFinder
        {
            get { return new HubMethodInfoFinder();}
        }

        /// <summary>
        /// 获取 功能技术提供者，如Mvc/WebApi/SignalR等，用于区分功能来源，各技术更新功能时，只更新属于自己技术的功能
        /// </summary>
        protected override PlatformToken PlatformToken { get { return PlatformToken.SignalR; } }

        /// <summary>
        /// 重写以实现从类型信息创建功能信息
        /// </summary>
        /// <param name="type">类型信息</param>
        /// <returns></returns>
        protected override Function GetFunction(Type type)
        {
            if (!typeof(IHub).IsAssignableFrom(type))
            {
                throw new InvalidOperationException(Resources.HubMethodInfoFinder_TypeNotHubType.FormatWith(type.FullName));
            }
            Function function = new Function()
            {
                Name = type.ToDescription(),
                Area = GetArea(type),
                Controller = type.Name.Replace("Hub", string.Empty),
                IsController = true,
                FunctionType = FunctionType.Anonymouse,
                PlatformToken = PlatformToken
            };
            return function;
        }

        /// <summary>
        /// 重写以实现从方法信息创建功能信息
        /// </summary>
        /// <param name="method">方法信息</param>
        /// <returns></returns>
        protected override Function GetFunction(MethodInfo method)
        {
            Type type = method.DeclaringType;
            if (type == null)
            {
                throw new InvalidOperationException(Resources.FunctionHandler_DefindActionTypeIsNull.FormatWith(method.Name));
            }
            if (typeof(IHub).IsAssignableFrom(type))
            {
                throw new InvalidOperationException(Resources.FunctionHandler_MethodOwnTypeNotHubType.FormatWith(method.Name, type.FullName));
            }

            FunctionType functionType = FunctionType.Anonymouse;
            if (method.HasAttribute<LoginedAttribute>(true))
            {
                functionType = FunctionType.Logined;
            }
            else if (method.HasAttribute<RoleLimitAttribute>(true))
            {
                functionType = FunctionType.RoleLimit;
            }
            Function function = new Function()
            {
                Name = method.ToDescription(),
                Area = GetArea(type),
                Controller = type.Name.Replace("Hub", string.Empty),
                Action = method.Name,
                FunctionType = functionType,
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
            return "AreaSignalR";
        }
    }
}