// -----------------------------------------------------------------------
//  <copyright file="AdminBaseController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-28 15:30</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

using OSharp.Core.Data;
using OSharp.Core.Data.Extensions;
using OSharp.Utility.Filter;
using OSharp.Web.Mvc;
using OSharp.Web.Mvc.Filters;
using OSharp.Web.Mvc.UI;


namespace OSharp.Demo.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 后台管理控制器蕨类
    /// </summary>
    [OperateLogFilter]
    public abstract class AdminBaseController : BaseController
    {
        protected virtual IQueryable<TEntity> GetQueryData<TEntity, TKey>(IQueryable<TEntity> source, out int total, GridRequest request = null)
            where TEntity : EntityBase<TKey>
            where TKey : IEquatable<TKey>
        {
            if (request == null)
            {
                request = new GridRequest(Request);
            }
            Expression<Func<TEntity, bool>> predicate = FilterHelper.GetExpression<TEntity>(request.FilterGroup);
            return source.Where(predicate, request.PageCondition, out total);
        }

        protected virtual PageResult<TResult> GetPageResult<TEntity, TResult>(IQueryable<TEntity> source,
            Expression<Func<TEntity, TResult>> selector,
            GridRequest request = null)
        {
            if (request == null)
            {
                request = new GridRequest(Request);
            }
            Expression<Func<TEntity, bool>> predicate = FilterHelper.GetExpression<TEntity>(request.FilterGroup);
            return source.ToPage(predicate, request.PageCondition, selector);
        }

        public virtual ActionResult Index()
        {
            return View();
        }
    }
}