// -----------------------------------------------------------------------
//  <copyright file="DtoMappers.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-24 16:13</last-date>
// -----------------------------------------------------------------------

using AutoMapper;

using OSharp.Core.Security;
using OSharp.Demo.Dtos.Identity;
using OSharp.Demo.Dtos.Security;
using OSharp.Demo.Models.Identity;


namespace OSharp.Demo.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class DtoMappers
    {
        /// <summary>
        /// 
        /// </summary>
        public static void MapperRegister()
        {
            //Identity
            Mapper.CreateMap<OrganizationDto, Organization>();
            Mapper.CreateMap<UserDto, User>();
            Mapper.CreateMap<RoleDto, Role>();
            //Security
            Mapper.CreateMap<FunctionDto, Function>();
            Mapper.CreateMap<EntityInfoDto, EntityInfo>();
        }
    }
}