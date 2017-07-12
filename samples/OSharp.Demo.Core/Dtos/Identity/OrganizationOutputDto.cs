using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data;


namespace OSharp.Demo.Dtos.Identity
{
    public class OrganizationOutputDto : IOutputDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
