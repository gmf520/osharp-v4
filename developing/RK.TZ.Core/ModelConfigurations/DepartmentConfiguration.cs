using RK.TZ.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RK.TZ.Core.ModelConfigurations
{
    public partial class DepartmentConfiguration
    {
        public override Type DbContextType
        {
            get
            {
                return typeof(RKDbContext);
            }
        }
        partial void DepartmentConfigurationAppend()
        {
        }
    }
}
