using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data;


namespace OSharp.Demo.Dtos.Identity
{
    public class RoleOutputDto : IOutputDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsSystem { get; set; }

        public bool IsLocked { get; set; }

        public OrganizationOutputDto Organization { get; set; }
    }
}
