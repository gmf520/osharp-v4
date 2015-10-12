// -----------------------------------------------------------------------
//  <copyright file="NullFunctionMethodInfoFinder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-12 20:49</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 空的方法查找器
    /// </summary>
    public class NullFunctionMethodInfoFinder : IFunctionMethodInfoFinder
    {
        /// <summary>
        /// 查找指定条件的方法信息
        /// </summary>
        /// <param name="type">控制器类型</param>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        public MethodInfo[] Find(Type type, Func<MethodInfo, bool> predicate)
        {
            return FindAll(type).Where(predicate).ToArray();
        }

        /// <summary>
        /// 从指定类型查找方法信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public MethodInfo[] FindAll(Type type)
        {
            return new MethodInfo[0];
        }
    }
}