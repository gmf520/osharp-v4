// -----------------------------------------------------------------------
//  <copyright file="DbContextInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-28 3:37</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data.Entity.Migrations;


namespace OSharp.Core.Data.Entity
{
    /// <summary>
    /// 默认 上下文初始化操作类
    /// </summary>
    public sealed class DefaultDbContextInitializer : DbContextInitializerBase<DefaultDbContext>
    {
        
    }
}