// -----------------------------------------------------------------------
//  <copyright file="EntityInfosController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-15 2:06</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Web.Mvc;

using OSharp.Core.Data.Extensions;
using OSharp.Core.Security;
using OSharp.Demo.Contracts;
using OSharp.Demo.Dtos.Security;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Filter;
using OSharp.Web.Mvc.Extensions;
using OSharp.Web.Mvc.Security;
using OSharp.Web.Mvc.UI;


namespace OSharp.Demo.Web.Areas.Admin.Controllers
{
    [Description("管理-实体数据")]
    public class EntityInfosController : AdminBaseController
    {
        /// <summary>
        /// 获取或设置 安全业务对象
        /// </summary>
        public ISecurityContract SecurityContract { get; set; }

        #region Ajax功能

        #region 获取数据

        [AjaxOnly]
        [Description("管理-实体数据-列表数据")]
        public ActionResult GridData()
        {
            GridRequest request = new GridRequest(Request);
            if (request.PageCondition.SortConditions.Length == 0)
            {
                request.PageCondition.SortConditions = new[]
                {
                    new SortCondition("ClassName")
                };
            }
            Expression<Func<EntityInfo, bool>> predicate = FilterHelper.GetExpression<EntityInfo>(request.FilterGroup);
            var page = SecurityContract.EntityInfos.ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    m.Id,
                    m.Name,
                    m.ClassName,
                    m.DataLogEnabled
                });
            return Json(page.ToGridData(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 功能方法

        [HttpPost]
        [AjaxOnly]
        [Description("管理-实体数据-编辑")]
        public ActionResult Edit(EntityInfoInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = SecurityContract.EditEntityInfos(dtos);
            return Json(result.ToAjaxResult());
        }

        #endregion

        #endregion

        #region 视图功能

        #region Overrides of AdminBaseController

        [Description("管理-实体数据-列表")]
        public override ActionResult Index()
        {
            return base.Index();
        }

        #endregion

        #endregion

    }
}