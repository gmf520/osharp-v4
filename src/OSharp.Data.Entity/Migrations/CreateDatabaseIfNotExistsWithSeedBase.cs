// -----------------------------------------------------------------------
//  <copyright file="CreateDatabaseIfNotExistsWithSeedBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-28 2:45</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace OSharp.Data.Entity.Migrations
{
    /// <summary>
    /// 在数据库不存在时使用种子数据创建数据库
    /// </summary>
    public abstract class CreateDatabaseIfNotExistsWithSeedBase<TDbContext> : CreateDatabaseIfNotExists<TDbContext> where TDbContext : DbContext
    {
        /// <summary>
        /// 初始化一个<see cref="CreateDatabaseIfNotExistsWithSeedBase{TDbContext}"/>类型的新实例
        /// </summary>
        protected CreateDatabaseIfNotExistsWithSeedBase()
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