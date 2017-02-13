// -----------------------------------------------------------------------
//  <copyright file="LoggingController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-30 3:51</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

using OSharp.Core.Logging;
using OSharp.Demo.Contracts;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Filter;
using OSharp.Web.Mvc.Security;
using OSharp.Web.Mvc.UI;


namespace OSharp.Demo.Web.Areas.Admin.Controllers
{
    [Description("管理-日志")]
    public class LoggingController : AdminBaseController
    {
        /// <summary>
        /// 获取或设置 日志业务对象
        /// </summary>
        public ILoggingContract LoggingContract { get; set; }

        #region Ajax功能

        #region 获取数据

        [AjaxOnly]
        [Description("管理-操作日志-列表数据")]
        public ActionResult OperateLogGridData()
        {
            int total;
            GridRequest request = new GridRequest(Request);
            if (request.PageCondition.SortConditions.Length == 0)
            {
                request.PageCondition.SortConditions = new[] { new SortCondition("CreatedTime", ListSortDirection.Descending) };
            }
            var datas = GetQueryData<OperateLog, int>(LoggingContract.OperateLogs, out total, request).Select(m => new
            {
                m.Id,
                m.FunctionName,
                m.Operator.UserId,
                m.Operator.NickName,
                m.Operator.Ip,
                m.CreatedTime,
                DataLogs = new { m.Id, m.DataLogs.Count }
            }).ToList();
            return Json(new GridData<object>(datas, total), JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        [Description("管理-数据日志-列表数据")]
        public ActionResult DataLogGridData(int id)
        {
            GridRequest request = new GridRequest(Request);
            if (id > 0)
            {
                request.FilterGroup.Rules.Add(new FilterRule("OperateLog.Id", id));
            }
            int total;
            var datas = GetQueryData<DataLog, int>(LoggingContract.DataLogs, out total, request).Select(m => new
            {
                m.Id,
                m.EntityKey,
                m.Name,
                m.EntityName,
                m.OperateType,
                LogItems = m.LogItems.Select(n => new
                {
                    n.Field,
                    n.FieldName,
                    n.OriginalValue,
                    n.NewValue,
                    n.DataType
                })
            }).ToList();
            return Json(new GridData<object>(datas, total), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 功能方法

        [AjaxOnly]
        [Description("管理-操作日志-删除")]
        public ActionResult DeleteOperateLogs(int[] ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = LoggingContract.DeleteOperateLogs(ids);
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region 视图功能

        [Description("管理-日志模块-首页")]
        public override ActionResult Index()
        {
            return RedirectToAction("OperateLogs");
        }

        [Description("管理-操作日志-列表")]
        public ActionResult OperateLogs()
        {
            return View();
        }

        [Description("管理-系统日志-列表")]
        public ActionResult SystemLogs()
        {
            return Content("数据日志");
        }

        #endregion
    }
}