// -----------------------------------------------------------------------
//  <copyright file="FunctionsController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-15 1:03</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

using OSharp.Core.Data.Extensions;
using OSharp.Core.Security;
using OSharp.Demo.Contracts;
using OSharp.Demo.Dtos.Security;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Filter;
using OSharp.Web.Mvc.Security;
using OSharp.Web.Mvc.UI;


namespace OSharp.Demo.Web.Areas.Admin.Controllers
{
    [Description("管理-功能")]
    public class FunctionsController : AdminBaseController
    {
        /// <summary>
        /// 获取或设置 安全业务对象
        /// </summary>
        public ISecurityContract SecurityContract { get; set; }

        #region Ajax功能

        #region 获取数据

        [AjaxOnly]
        [Description("管理-功能-列表数据")]
        public ActionResult GridData()
        {
            GridRequest request = new GridRequest(Request);
            if (request.PageCondition.SortConditions.Length == 0)
            {
                request.PageCondition.SortConditions = new[]
                {
                    new SortCondition("Area"),
                    new SortCondition("Controller"), 
                    new SortCondition("Name")
                };
            }
            Expression<Func<Function, bool>> predicate = FilterHelper.GetExpression<Function>(request.FilterGroup);
            var page = SecurityContract.Functions.ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    m.Id,
                    m.Name,
                    m.Url,
                    m.FunctionType,
                    m.OperateLogEnabled,
                    m.DataLogEnabled,
                    m.CacheExpirationSeconds,
                    m.IsCacheSliding,
                    m.Area,
                    m.Controller,
                    m.Action,
                    m.IsController,
                    m.IsAjax,
                    m.IsChild,
                    m.IsLocked,
                    m.IsTypeChanged,
                    m.IsCustom
                });
            GridData<object> gridData = new GridData<object>() { Total = page.Total };
            gridData.Rows = page.Data.Select(m => new
            {
                m.Id,
                m.Name,
                m.Url,
                m.FunctionType,
                m.OperateLogEnabled,
                m.DataLogEnabled,
                m.CacheExpirationSeconds,
                m.IsCacheSliding,
                m.Area,
                m.Controller,
                m.Action,
                ModuleName = m.Area + "-" + m.Controller,
                m.IsController,
                m.IsAjax,
                m.IsChild,
                m.IsLocked,
                m.IsTypeChanged,
                m.IsCustom
            }).ToArray();
            return Json(gridData, JsonRequestBehavior.AllowGet);
        }
        
        #endregion

        #region 功能方法

        [HttpPost]
        [AjaxOnly]
        [Description("管理-功能-新增")]
        public ActionResult Add(FunctionInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = SecurityContract.AddFunctions(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [AjaxOnly]
        [Description("管理-功能-编辑")]
        public async Task<ActionResult> Edit(FunctionInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos" );
            OperationResult result = await SecurityContract.EditFunctions(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [AjaxOnly]
        [Description("管理-功能-删除")]
        public ActionResult Delete(Guid[] ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = SecurityContract.DeleteFunctions(ids);
            return Json(result.ToAjaxResult());
        }

        #endregion

        #endregion

        #region 视图功能

        [Description("管理-功能-列表")]
        public override ActionResult Index()
        {
            return base.Index();
        }

        #endregion

    }
}