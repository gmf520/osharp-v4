// -----------------------------------------------------------------------
//  <copyright file="OperateLogFilterAttribute.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-04 15:26</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web.Mvc;

using OSharp.Core;
using OSharp.Core.Context;
using OSharp.Core.Exceptions;
using OSharp.Core.Logging;
using OSharp.Core.Security;
using OSharp.SiteBase.Extensions;
using OSharp.Web.Extensions;
using OSharp.Web.Mvc.Extensions;

namespace OSharp.SiteBase.Logging
{
    /// <summary>
    /// 操作日志记录过滤器
    /// </summary>
    public class OperateLogFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 获取或设置 数据日志缓存
        /// </summary>
        public IDataLogCache DataLogCache { get; set; }

        /// <summary>
        /// 获取或设置 操作日志输出者
        /// </summary>
        public IOperateLogWriter OperateLogWriter { get; set; }

        /// <summary>
        /// Called after the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            IFunction function = filterContext.GetExecuteFunction();
            if (function == null || !function.OperateLogEnabled)
            {
                return;
            }
            Operator @operator = new Operator()
            {
                Ip = filterContext.HttpContext.Request.GetIpAddress(),
            };
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                ClaimsIdentity identity = filterContext.HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    @operator.UserId = identity.GetClaimValue(ClaimTypes.NameIdentifier);
                    @operator.Name = identity.GetClaimValue(ClaimTypes.Name);
                    @operator.NickName = identity.GetClaimValue(ClaimTypes.GivenName);
                }
            }

            OperateLog operateLog = new OperateLog()
            {
                FunctionName = function.Name,
                Operator = @operator
            };
            if (function.DataLogEnabled)
            {
                foreach (DataLog dataLog in DataLogCache.DataLogs)
                {
                    operateLog.DataLogs.Add(dataLog);
                } 
            }
            OperateLogWriter.Write(operateLog);
        }
    }
}