// -----------------------------------------------------------------------
//  <copyright file="HomeController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-14 1:02</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Web.Mvc;

using OSharp.Utility.Logging;
using OSharp.Web.Mvc.Logging;


namespace OSharp.Demo.Web.Controllers
{
    [OperateLogFilter]
    [Description("网站")]
    public class HomeController : Controller
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(HomeController));

        [Description("网站-首页")]
        public ActionResult Index()
        {
            Logger.Debug("访问首页，将转向到后台管理首页");
            //return RedirectToAction("Index", "Home", new { area = "Admin" });
            //var data = new
            //{
            //    OrganizationCount = _identityContract.Organizations.Count(),
            //    UserCount = _identityContract.Users.Count(),
            //    RoleCount = _identityContract.Roles.Count()
            //};
            //ViewBag.Data = data.ToDynamic();
            return View();
        }
    }
}