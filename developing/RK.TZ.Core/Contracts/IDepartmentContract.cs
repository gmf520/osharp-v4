using OSharp.Core.Dependency;
using RK.TZ.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RK.TZ.Core.Contracts
{
    [ServiceContract]
    public interface IDepartmentContract: ILifetimeScopeDependency
    {
         [OperationContract]
         IQueryable<Department> GetInfos();
    }
}
