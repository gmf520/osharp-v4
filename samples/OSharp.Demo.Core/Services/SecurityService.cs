// -----------------------------------------------------------------------
//  <copyright file="SecurityService.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-14 23:07</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Data;
using OSharp.Core.Security;
using OSharp.Demo.Contracts;
using OSharp.Demo.Security;


namespace OSharp.Demo.Services
{
    /// <summary>
    /// 业务实现——功能模块
    /// </summary>
    public partial class SecurityService : ISecurityContract
    {
        /// <summary>
        /// 获取或设置 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 获取或设置 功能信息仓储对象
        /// </summary>
        public IRepository<Function, Guid> FunctionRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 实体数据信息仓储对象
        /// </summary>
        public IRepository<EntityInfo, Guid> EntityInfoRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 安全权限管理对象
        /// </summary>
        public SecurityManager SecurityManager { get; set; }
    }
}