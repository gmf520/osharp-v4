using OSharp.Web.Mvc;
using RK.TZ.Core.Contracts;
using RK.TZ.Core.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RK.TZ.Web.Controllers
{
    [Description("测试--机构管理")]
    public class DepartmentController : BaseController
    {
        public IDepartmentContract IDepartmentContract { get; set; }
        // GET: Department
        public ActionResult Index()
        {
            DepartmentDto[] data= IDepartmentContract.GetInfos().Select(d => new DepartmentDto()
            {
                Id = d.Id,
                Name = d.Name
            }).ToArray();
            return View(data);
        }
    }
}