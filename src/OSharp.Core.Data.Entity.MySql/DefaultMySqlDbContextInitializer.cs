// -----------------------------------------------------------------------
//  <copyright file="DefaultMySqlDbContextInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-02 15:44</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntityFramework;
using EntityFramework.Batch;


namespace OSharp.Core.Data.Entity
{
    /// <summary>
    /// MySql数据上下文初始化
    /// </summary>
    public class MySqlDefaultDbContextInitializer : DbContextInitializerBase<DefaultDbContext>
    {
        /// <summary>
        /// 初始化一个<see cref="MySqlDefaultDbContextInitializer"/>新实例
        /// </summary>
        public MySqlDefaultDbContextInitializer()
        {
            CreateDatabaseInitializer = MigrateInitializer
                = new MigrateDatabaseToLatestVersion<DefaultDbContext, MySqlMigrationsConfiguration<DefaultDbContext>>();
            Locator.Current.Register<IBatchRunner>(() => new MySqlBatchRunner());
        }
    }
}