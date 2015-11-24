// -----------------------------------------------------------------------
//  <copyright file="DbContextInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-28 3:37</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OSharp.Core.Data.Entity
{
    /// <summary>
    /// 上下文初始化操作类
    /// </summary>
    public class DbContextInitializer : DbContextInitializerBase<DefaultDbContext>
    {
        private static readonly Lazy<DbContextInitializer> InstanceLazy = new Lazy<DbContextInitializer>(() => new DbContextInitializer());

        /// <summary>
        /// 获取或设置 获取当前上下文初始化对象
        /// </summary>
        public static DbContextInitializer Instance
        {
            get { return InstanceLazy.Value; }
        }
    }
}