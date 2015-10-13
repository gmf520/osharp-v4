using OSharp.Core.Data.Entity;
using OSharp.Core.Security;
using RK.TZ.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RK.TZ.Core.ModelConfigurations
{
    public class DepartmentConfiguration : EntityConfigurationBase<Department, Int32>
    { }

    public class EntityInfoConfiguration : EntityConfigurationBase<EntityInfo, Guid>
    { }
    public class FunctionConfiguration : EntityConfigurationBase<Function, Guid>
    { }
}
