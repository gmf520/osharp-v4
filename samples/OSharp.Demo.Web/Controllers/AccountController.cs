// -----------------------------------------------------------------------
//  <copyright file="AccountController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-29 22:32</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Web.Mvc;

using OSharp.Web.Mvc;


namespace OSharp.Demo.Web.Controllers
{
    [Description("网站-账户")]
    public class AccountController : BaseController
    {
        [Description("账户-首页")]
        public ActionResult Index()
        {
            return new EmptyResult();
        }
    }
}