// -----------------------------------------------------------------------
//  <copyright file="EntityUserMapDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-07 3:51</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Security.Dtos;


namespace OSharp.Demo.Dtos.Security
{
    /// <summary>
    /// 数据用户映射DTO
    /// </summary>
    public class EntityUserMapDto : EntityUserMapBaseDto<int, Guid, int>
    { }
}