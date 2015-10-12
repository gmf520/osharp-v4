using OSharp.Core.Data.Entity;
using RK.TZ.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RK.TZ.Core.ModelConfigurations
{
    public partial class DepartmentConfiguration : EntityConfigurationBase<Department, Int32>
    {
         /// <summary>
        /// 初始化一个<see cref="OrganizationConfiguration"/>类型的新实例
        /// </summary>
        public DepartmentConfiguration()
        {
            DepartmentConfigurationAppend();
        }

        /// <summary>
        /// 额外的数据映射
        /// </summary>
        partial void DepartmentConfigurationAppend();
    }
}
