using OSharp.Core.Data;
using RK.TZ.Core.Contracts;
using RK.TZ.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RK.TZ.Core.Services
{
    public class DepartmentService : IDepartmentContract
    {
        public IRepository<Department, int> DepartmentRepository {protected get; set; }


        public IQueryable<Department> GetInfos()
        {
            return DepartmentRepository.Entities;
        }
    }
}
