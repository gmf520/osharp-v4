// -----------------------------------------------------------------------
//  <copyright file="SignalRHubTypeFinder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-12 21:03</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

using Microsoft.AspNet.SignalR.Hubs;

using OSharp.Core.Reflection;
using OSharp.Core.Security;


namespace OSharp.Web.SignalR.Initialize
{
    /// <summary>
    /// SignalR Hub 类型查找器
    /// </summary>
    public class SignalRHubTypeFinder : IFunctionTypeFinder
    {
        /// <summary>
        /// 获取或设置 程序集查找器
        /// </summary>
        public IAllAssemblyFinder AssemblyFinder { get; set; }

        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        public Type[] Find(Func<Type, bool> predicate)
        {
            return FindAll().Where(predicate).ToArray();
        }

        /// <summary>
        /// 查找所有项
        /// </summary>
        /// <returns></returns>
        public Type[] FindAll()
        {
            Assembly[] assemblies = AssemblyFinder.FindAll();
            return assemblies.SelectMany(assembly => assembly.GetTypes()
                .Where(type => typeof(IHub).IsAssignableFrom(type) && !type.IsAbstract))
                .Distinct().ToArray();
        }
    }
}