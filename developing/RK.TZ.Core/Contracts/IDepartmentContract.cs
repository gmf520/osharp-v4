using OSharp.Core.Dependency;
using RK.TZ.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RK.TZ.Core.Contracts
{
    public interface IDepartmentContract: ILifetimeScopeDependency
    {
         IQueryable<Department> GetInfos();
    }
}
