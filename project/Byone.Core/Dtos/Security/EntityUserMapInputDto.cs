// -----------------------------------------------------------------------
//  <copyright file="EntityUserMapInputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-14 3:38</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Security.Dtos;


namespace Byone.Core.Dtos.Security
{
    /// <summary>
    /// 数据用户映射输入DTO
    /// </summary>
    public class EntityUserMapInputDto : EntityUserMapBaseInputDto<int, Guid, int>
    { }
}