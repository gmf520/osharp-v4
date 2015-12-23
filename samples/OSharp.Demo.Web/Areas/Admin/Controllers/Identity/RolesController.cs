// -----------------------------------------------------------------------
//  <copyright file="RolesController.cs" company="OSharp开源团队">
//      Copyright (c) 2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>最后修改人</last-editor>
//  <last-date>2015-01-09 20:43</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Web.Mvc;

using OSharp.Demo.Contracts;
using OSharp.Demo.Dtos.Identity;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Filter;
using OSharp.Web.Mvc.Extensions;
using OSharp.Web.Mvc.Security;
using OSharp.Web.Mvc.UI;


namespace OSharp.Demo.Web.Areas.Admin.Controllers
{
    [Description("管理-角色")]
    public class RolesController : AdminBaseController
    {
        /// <summary>
        /// 获取或设置 身份认证业务对象
        /// </summary>
        public IIdentityContract IdentityContract { get; set; }

        #region Ajax功能

        #region 获取数据

        //id: 组织机构编号
        [AjaxOnly]
        [Description("管理-角色-列表数据")]
        public ActionResult GridData(int? id)
        {
            GridRequest request = new GridRequest(Request);
            if (id.HasValue && id.Value > 0)
            {
                request.FilterGroup.Rules.Add(new FilterRule("Organization.Id", id.Value));
            }
            var page = GetPageResult(IdentityContract.Roles, m => new
            {
                m.Id,
                m.Name,
                m.Remark,
                m.IsAdmin,
                m.IsSystem,
                m.IsLocked,
                m.CreatedTime,
                OrganizationId = m.Organization == null ? 0 : m.Organization.Id,
                OrganizationName = m.Organization.Name
            }, request);

            return Json(page.ToGridData(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 功能方法

        [HttpPost]
        [AjaxOnly]
        [Description("管理-角色-新增")]
        public ActionResult Add(RoleInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos" );
            OperationResult result = IdentityContract.AddRoles(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [AjaxOnly]
        [Description("管理-角色-编辑")]
        public ActionResult Edit(RoleInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos" );
            OperationResult result = IdentityContract.EditRoles(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [AjaxOnly]
        [Description("管理-角色-删除")]
        public ActionResult Delete(int[] ids)
        {
            ids.CheckNotNull("ids" );
            OperationResult result = IdentityContract.DeleteRoles(ids);
            return Json(result.ToAjaxResult());
        }

        #endregion

        #endregion

        #region 视力功能

        [Description("管理-角色-列表")]
        public override ActionResult Index()
        {
            return base.Index();
        }

        #endregion
    }
}