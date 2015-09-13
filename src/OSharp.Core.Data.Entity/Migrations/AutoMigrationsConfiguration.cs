using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSharp.Core.Data.Entity.Migrations
{
    /// <summary>
    /// 自动迁移配置
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class AutoMigrationsConfiguration<TContext> : DbMigrationsConfiguration<TContext>
        where TContext : DbContext, IUnitOfWork
    {
        /// <summary>
        /// 初始化一个<see cref="AutoMigrationsConfiguration{TContext}"/>类型的新实例
        /// </summary>
        public AutoMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = typeof(TContext).FullName;
        }
    }
}
