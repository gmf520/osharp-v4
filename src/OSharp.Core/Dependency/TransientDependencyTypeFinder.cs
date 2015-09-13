// -----------------------------------------------------------------------
//  <copyright file="TransientDependencyTypeFinder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-31 17:16</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Reflection;


namespace OSharp.Core.Dependency
{
    /// <summary>
    /// <see cref="ITransientDependency"/>接口实现类查找器
    /// </summary>
    public class TransientDependencyTypeFinder : ITypeFinder
    {
        /// <summary>
        /// 初始化一个<see cref="SingletonDependencyTypeFinder"/>类型的新实例
        /// </summary>
        public TransientDependencyTypeFinder()
        {
            AssemblyFinder = new CurrentDomainAssemblyFinder();
        }

        /// <summary>
        /// 获取或设置 程序集查找器
        /// </summary>
        public IAssemblyFinder AssemblyFinder { get; set; }

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
            return assemblies.SelectMany(assembly =>
                assembly.GetTypes().Where(type =>
                    typeof(ITransientDependency).IsAssignableFrom(type) && !type.IsAbstract))
                .Distinct().ToArray();
        }
    }
}