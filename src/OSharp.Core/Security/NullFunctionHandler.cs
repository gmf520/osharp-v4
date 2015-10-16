﻿// -----------------------------------------------------------------------
//  <copyright file="NullFunctionHandler.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-12 20:50</last-date>
// -----------------------------------------------------------------------

using System;
using System.Reflection;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 空的功能信息处理器
    /// </summary>
    public class NullFunctionHandler : FunctionHandlerBase<Function, Guid>
    {
        /// <summary>
        /// 初始化一个<see cref="NullFunctionHandler"/>类型的新实例
        /// </summary>
        public NullFunctionHandler()
        {
            TypeFinder = new NullFunctionTypeFinder();
            MethodInfoFinder = new NullFunctionMethodInfoFinder();
        }
        
        /// <summary>
        /// 获取 功能技术提供者，如Mvc/WebApi/SignalR等，用于区分功能来源，各技术更新功能时，只更新属于自己技术的功能
        /// </summary>
        protected override PlatformToken PlatformToken
        {
            get { return PlatformToken.Local; }
        }

        /// <summary>
        /// 重写以实现从类型信息创建功能信息
        /// </summary>
        /// <param name="type">类型信息</param>
        /// <returns></returns>
        protected override Function GetFunction(Type type)
        {
            return new Function();
        }

        /// <summary>
        /// 重写以实现从方法信息创建功能信息
        /// </summary>
        /// <param name="method">方法信息</param>
        /// <returns></returns>
        protected override Function GetFunction(MethodInfo method)
        {
            return new Function();
        }

        /// <summary>
        /// 重写以实现从类型中获取功能的区域信息
        /// </summary>
        protected override string GetArea(Type type)
        {
            return "OSHARP_DEFAULT_AREA";
        }
    }
}