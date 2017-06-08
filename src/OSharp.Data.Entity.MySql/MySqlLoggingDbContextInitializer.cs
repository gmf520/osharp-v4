// -----------------------------------------------------------------------
//  <copyright file="MySqlLoggingDbContextInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-02 16:18</last-date>
// -----------------------------------------------------------------------

using System.Data.Entity;

using OSharp.Data.Entity.Logging;


namespace OSharp.Data.Entity
{
    /// <summary>
    /// MySql的log数据库上下文初始化
    /// </summary>
    public class MySqlLoggingDbContextInitializer : DbContextInitializerBase<LoggingDbContext>
    {
        /// <summary>
        /// 初始化一个<see cref="MySqlLoggingDbContextInitializer"/>新实例
        /// </summary>
        public MySqlLoggingDbContextInitializer()
        {
            CreateDatabaseInitializer = MigrateInitializer
                = new MigrateDatabaseToLatestVersion<LoggingDbContext, MySqlMigrationsConfiguration<LoggingDbContext>>();
        }
    }
}