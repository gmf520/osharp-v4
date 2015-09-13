// -----------------------------------------------------------------------
//  <copyright file="MySqlMigrationsConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-02 15:33</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.Entity;


namespace OSharp.Core.Data.Entity
{
    /// <summary>
    /// MySql自动迁移配置类
    /// </summary>
    /// <typeparam name="TContext">MySql 数据上下文</typeparam>
    public class MySqlMigrationsConfiguration<TContext> : DbMigrationsConfiguration<TContext>
        where TContext : DbContext, IUnitOfWork
    {
        private const string ProviderName = "MySql.Data.MySqlClient";

        /// <summary>
        /// 初始化一个<see cref="MySqlMigrationsConfiguration{TContext}"/>类型的新实例
        /// </summary>
        public MySqlMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = typeof(TContext).FullName;

            SetSqlGenerator(ProviderName, new MySqlMigrationSqlGenerator());
            SetHistoryContextFactory(ProviderName, (conn, schema) => new MySqlHistoryContext(conn, schema));
        }

    }
}