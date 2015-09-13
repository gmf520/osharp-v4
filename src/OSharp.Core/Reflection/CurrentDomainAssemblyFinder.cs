// -----------------------------------------------------------------------
//  <copyright file="CurrentDomainAssemblyFinder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-28 2:54</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace OSharp.Core.Reflection
{
    /// <summary>
    /// 当前应用程序域程序集查找器
    /// </summary>
    public class CurrentDomainAssemblyFinder : IAssemblyFinder
    {
        /// <summary>
        /// 查找当前应用程序域符合指定条件的程序集
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        public Assembly[] Find(Func<Assembly, bool> predicate)
        {
            return FindAll().Where(predicate).ToArray();
        }

        /// <summary>
        /// 获取当前应用程序域所有已加载的程序集
        /// </summary>
        /// <returns></returns>
        public Assembly[] FindAll()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}