// -----------------------------------------------------------------------
//  <copyright file="DefaultMySqlDbContextInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-02 15:44</last-date>
// -----------------------------------------------------------------------

using System.Data.Entity;

using EntityFramework;
using EntityFramework.Batch;


namespace OSharp.Data.Entity
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