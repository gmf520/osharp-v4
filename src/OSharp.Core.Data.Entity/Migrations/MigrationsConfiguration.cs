// -----------------------------------------------------------------------
//  <copyright file="MigrationsConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-05 9:26</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OSharp.Core.Data.Entity.Migrations
{
    /// <summary>
    /// 默认迁移配置
    /// </summary>
    public class MigrationsConfiguration : DbMigrationsConfiguration<CodeFirstDbContext>
    {
        static MigrationsConfiguration()
        {
            SeedActions = new List<ISeedAction>();
        }

        /// <summary>
        /// 初始化一个<see cref="MigrationsConfiguration"/>类型的新实例
        /// </summary>
        public MigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        /// <summary>
        /// 获取 数据迁移初始化种子数据操作信息集合，各个模块可以添加自己的数据初始化操作
        /// </summary>
        public static ICollection<ISeedAction> SeedActions { get; private set; }

        protected override void Seed(CodeFirstDbContext context)
        {
            IEnumerable<ISeedAction> seedActions = SeedActions.OrderBy(m => m.Order);
            foreach (ISeedAction seedAction in seedActions)
            {
                seedAction.Action(context);
            }
        }
    }
}