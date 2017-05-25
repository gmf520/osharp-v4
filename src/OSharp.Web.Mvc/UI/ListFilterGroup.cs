using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using OSharp.Utility.Data;
using OSharp.Utility.Extensions;
using OSharp.Utility.Filter;


namespace OSharp.Web.Mvc.UI
{
    /// <summary>
    /// 列表数据筛选条件组
    /// </summary>
    public class ListFilterGroup : FilterGroup
    {
        /// <summary>
        /// 初始化一个<see cref="ListFilterGroup"/>类型的新实例
        /// </summary>
        public ListFilterGroup(HttpRequestBase request)
        {
            string jsonWhere = request.Params["filter_group"];
            if (jsonWhere.IsNullOrEmpty())
            {
                return;
            }
            FilterGroup group = JsonHelper.FromJson<FilterGroup>(jsonWhere);
            Rules = group.Rules;
            Groups = group.Groups;
            Operate = group.Operate;
        }
    }
}
