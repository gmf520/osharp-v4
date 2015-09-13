// -----------------------------------------------------------------------
//  <copyright file="EntityMapperAssemblyFinder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-29 14:26</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data.Entity;
using OSharp.Core.Reflection;
using OSharp.Utility.Extensions;


namespace OSharp.SiteBase.Initialize
{
    /// <summary>
    /// 实体映射程序集查找器
    /// </summary>
    public class EntityMapperAssemblyFinder : IAssemblyFinder
    {
        /// <summary>
        /// 初始化一个<see cref="EntityMapperAssemblyFinder"/>类型的新实例
        /// </summary>
        public EntityMapperAssemblyFinder()
        {
            AllAssemblyFinder = new CurrentDomainAssemblyFinder();
        }

        /// <summary>
        /// 获取或设置 所有程序集查找器
        /// </summary>
        public IAssemblyFinder AllAssemblyFinder { get; set; }

        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        public Assembly[] Find(Func<Assembly, bool> predicate)
        {
            return FindAll().Where(predicate).ToArray();
        }

        /// <summary>
        /// 查找所有项
        /// </summary>
        /// <returns></returns>
        public Assembly[] FindAll()
        {
            Type baseType = typeof(IEntityMapper);
            Assembly[] assemblies = AllAssemblyFinder.Find(assembly =>
                assembly.GetTypes().Any(type => baseType.IsAssignableFrom(type) && !type.IsAbstract));
            return assemblies;
        }
    }
}