// -----------------------------------------------------------------------
//  <copyright file="MigrationsConfigurationBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-28 2:48</last-date>
// -----------------------------------------------------------------------

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
    /// 数据迁移配置基类
    /// </summary>
    public abstract class MigrationsConfigurationBase<TDbContext> : DbMigrationsConfiguration<TDbContext> where TDbContext : DbContext
    {
        /// <summary>
        /// 初始化一个<see cref="MigrationsConfigurationBase{TDbContext}"/>类型的新实例
        /// </summary>
        protected MigrationsConfigurationBase()
        {
            SeedActions = new List<ISeedAction>();
        }

        /// <summary>
        /// 获取 数据迁移初始化种子数据操作信息集合，各个模块可以添加自己的数据初始化操作
        /// </summary>
        public ICollection<ISeedAction> SeedActions { get; private set; }

        protected override void Seed(TDbContext context)
        {
            IEnumerable<ISeedAction> seedActions = SeedActions.OrderBy(m => m.Order);
            foreach (ISeedAction seedAction in seedActions)
            {
                seedAction.Action(context);
            }
        }
    }
}