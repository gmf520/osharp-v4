// -----------------------------------------------------------------------
//  <copyright file="UsersController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-10 12:36</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using OSharp.Core.Caching;
using OSharp.Core.Data.Extensions;
using OSharp.Core.Security;
using OSharp.Demo.Contracts;
using OSharp.Demo.Dtos.Identity;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Web.Mvc.Extensions;
using OSharp.Web.Mvc.Security;
using OSharp.Web.Mvc.UI;


namespace OSharp.Demo.Web.Areas.Admin.Controllers
{
    [Description("管理-用户")]
    public class UsersController : AdminBaseController
    {
        /// <summary>
        /// 获取或设置 身份认证业务对象
        /// </summary>
        public IIdentityContract IdentityContract { get; set; }

        #region 视图功能

        [Description("管理-用户-列表")]
        public override ActionResult Index()
        {
            return View();
        }

        #endregion

        #region Ajax功能

        #region 获取数据

        [AjaxOnly]
        [Description("管理-用户-列表数据")]
        public ActionResult GridData()
        {
            GridRequest request = new GridRequest(Request);
            request.AddDefaultSortCondition(new SortCondition("CreatedTime", ListSortDirection.Descending));
            var page = GetPageResult(IdentityContract.Users,
                m => new
                {
                    m.Id,
                    m.UserName,
                    m.NickName,
                    m.Email,
                    m.EmailConfirmed,
                    m.PhoneNumber,
                    m.PhoneNumberConfirmed,
                    m.LockoutEndDateUtc,
                    m.AccessFailedCount,
                    m.IsLocked,
                    m.CreatedTime
                },
                request);
            return Json(page.ToGridData(), JsonRequestBehavior.AllowGet);
        }

        [Description("管理-用户-节点数据")]
        public ActionResult NoteData()
        {
            IFunction function = ControllerContext.GetExecuteFunction(ServiceProvider);
            //var nodes = IdentityContract.Users.Where(m => !m.IsLocked).OrderBy(m => m.NickName)
            //    .Select(m => new
            //    {
            //        m.Id,
            //        m.UserName,
            //        m.NickName
            //    });
            var nodes = IdentityContract.Users.ToCacheList(m => !m.IsLocked,
                m => new
                {
                    m.Id,
                    m.UserName,
                    m.NickName
                },
                function);
            return Json(nodes, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 功能方法

        [HttpPost]
        [AjaxOnly]
        [AllowAnonymous]
        [Description("管理-用户-新增")]
        public async Task<ActionResult> Add(UserInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = await IdentityContract.AddUsers(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [AjaxOnly]
        [Logined]
        [Description("管理-用户-编辑")]
        public async Task<ActionResult> Edit(UserInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = await IdentityContract.EditUsers(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [AjaxOnly]
        [RoleLimit]
        [Description("管理-用户-删除")]
        public async Task<ActionResult> Delete(int[] ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = await IdentityContract.DeleteUsers(ids);
            return Json(result.ToAjaxResult());
        }

        #endregion

        #endregion
    }
}