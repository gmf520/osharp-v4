// -----------------------------------------------------------------------
//  <copyright file="GridRequest.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-08-04 19:12</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

using OSharp.Utility.Data;
using OSharp.Utility.Extensions;
using OSharp.Utility.Filter;


namespace OSharp.Web.Mvc.UI
{
    /// <summary>
    /// Grid查询请求
    /// </summary>
    public class GridRequest
    {
        /// <summary>
        /// 初始化一个<see cref="GridRequest"/>类型的新实例
        /// </summary>
        public GridRequest(HttpRequestBase request)
        {
            string jsonWhere = request.Params["filter_group"];
            FilterGroup = !jsonWhere.IsNullOrEmpty() ? JsonHelper.FromJson<FilterGroup>(jsonWhere) : new FilterGroup();

            int pageIndex = request.Params["pageIndex"].CastTo(1);
            int pageSize = request.Params["pageSize"].CastTo(20);
            PageCondition = new PageCondition(pageIndex, pageSize);
            string sortField = request.Params["sortField"];
            string sortOrder = request.Params["sortOrder"];
            if (!sortField.IsNullOrEmpty() && !sortOrder.IsNullOrEmpty())
            {
                string[] fields = sortField.Split(",", true);
                string[] orders = sortOrder.Split(",", true);
                if (fields.Length != orders.Length)
                {
                    throw new ArgumentException("查询列表的排序参数个数不一致。");
                }
                List<SortCondition> sortConditions = new List<SortCondition>();
                for (int i = 0; i < fields.Length; i++)
                {
                    ListSortDirection direction = orders[i].ToLower() == "desc"
                        ? ListSortDirection.Descending
                        : ListSortDirection.Ascending;
                    sortConditions.Add(new SortCondition(fields[i], direction));
                }
                PageCondition.SortConditions = sortConditions.ToArray();
            }
            else
            {
                PageCondition.SortConditions = new SortCondition[] { };
            }
        }

        /// <summary>
        /// 获取 查询条件组
        /// </summary>
        public FilterGroup FilterGroup { get; private set; }

        /// <summary>
        /// 获取 分页查询条件信息
        /// </summary>
        public PageCondition PageCondition { get; private set; }

        /// <summary>
        /// 添加默认排序条件，只有排序条件为空时有效
        /// </summary>
        /// <param name="conditions"></param>
        public void AddDefaultSortCondition(params SortCondition[] conditions)
        {
            if (PageCondition.SortConditions.Length == 0)
            {
                PageCondition.SortConditions = conditions;
            }
        }

    }
}