// -----------------------------------------------------------------------
//  <copyright file="TestsController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-24 12:06</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace OSharp.Demo.Web.Areas.Service.Controllers
{
    [Description("服务-测试")]
    public class TestsApiController : ApiController
    {
        [Description("服务-测试-测试001")]
        [HttpGet]
        public IHttpActionResult Test01()
        {
            return Ok("Hello World.");
        }
    }
}