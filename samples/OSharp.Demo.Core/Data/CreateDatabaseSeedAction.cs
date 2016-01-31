using System;
using System.Data.Entity;

using OSharp.Data.Entity.Migrations;
using OSharp.Demo.Models.Identity;


namespace OSharp.Demo.Data
{
    public class CreateDatabaseSeedAction : ISeedAction
    {
        #region Implementation of ISeedAction

        /// <summary>
        /// 获取 操作排序，数值越小越先执行
        /// </summary>
        public int Order { get { return 1; } }

        /// <summary>
        /// 定义种子数据初始化过程
        /// </summary>
        /// <param name="context">数据上下文</param>
        public void Action(DbContext context)
        {
            context.Set<Role>().Add(new Role() { Name = "系统管理员", Remark = "系统管理员角色，拥有系统最高权限", IsAdmin = true, IsSystem = true, CreatedTime = DateTime.Now });
        }

        #endregion
    }
}
