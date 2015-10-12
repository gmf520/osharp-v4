using AutoMapper;
using RK.TZ.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RK.TZ.Core.Dtos
{
    public class DtoMappers
    {
        public static void MapperRegister()
        {
            Mapper.CreateMap<DepartmentDto, Department>();
        }
    }
}
