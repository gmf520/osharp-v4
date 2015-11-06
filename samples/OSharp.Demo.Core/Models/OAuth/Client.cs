﻿// -----------------------------------------------------------------------
//  <copyright file="Client.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-05 19:00</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Security.Models;


namespace OSharp.Demo.Models.OAuth
{
    /// <summary>
    /// 实体类——OAuth客户端信息
    /// </summary>
    public class Client : ClientBase<int, ClientSecret, int>
    { }
}