// -----------------------------------------------------------------------
//  <copyright file="MySqlHistoryContext.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-02 15:40</last-date>
// -----------------------------------------------------------------------

using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;


namespace OSharp.Data.Entity
{
    /// <summary>
    /// MySql迁移历史上下文，更改<see cref="HistoryRow"/>模型以适应MySql数据库的特性
    /// site:http://stackoverflow.com/questions/20832546/entity-framework-with-mysql-and-migrations-failing-because-max-key-length-is-76
    /// </summary>
    public class MySqlHistoryContext : HistoryContext
    {
        /// <summary>
        /// 初始化一个<see cref="MySqlHistoryContext"/>类型的新实例
        /// </summary>
        public MySqlHistoryContext(DbConnection existingConnection, string defaultSchema)
            : base(existingConnection, defaultSchema)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HistoryRow>().Property(m => m.MigrationId).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<HistoryRow>().Property(m => m.ContextKey).HasMaxLength(200).IsRequired();
        }
    }
}