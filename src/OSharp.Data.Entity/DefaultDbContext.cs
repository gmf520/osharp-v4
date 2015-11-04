// -----------------------------------------------------------------------
//  <copyright file="DefaultDbContext.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-28 3:34</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Utility.Extensions;


namespace OSharp.Core.Data.Entity
{
    /// <summary>
    /// 默认 EntityFramework 数据上下文
    /// </summary>
    public class DefaultDbContext : DbContextBase<DefaultDbContext>
    {
        /// <summary>
        /// 初始化一个<see cref="DefaultDbContext"/>类型的新实例
        /// </summary>
        public DefaultDbContext()
            : this(GetConnectionStringName())
        { }

        /// <summary>
        /// 初始化一个<see cref="DefaultDbContext"/>类型的新实例
        /// </summary>
        public DefaultDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }

        /// <summary>
        /// 获取 数据库连接串名称
        /// </summary>
        /// <returns></returns>
        private static string GetConnectionStringName()
        {
            string name = ConfigurationManager.AppSettings.Get("OSharp-ConnectionStringName") ?? "default";
            return name;
        }

        #region Overrides of DbContextBase<DefaultDbContext>

        /// <summary>
        /// 获取 是否允许数据库日志记录
        /// </summary>
        protected override bool DataLoggingEnabled
        {
            get { return ConfigurationManager.AppSettings.Get("OSharp-DataLoggingEnabled").CastTo(false); }
        }

        /// <summary>
        /// 获取 数据上下文初始化类
        /// </summary>
        public override DbContextInitializerBase<DefaultDbContext> DbContextInitializer
        {
            get { return Entity.DbContextInitializer.Instance; }
        }

        #endregion
    }
}