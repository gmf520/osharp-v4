using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

using OSharp.Core;
using OSharp.Core.Configs;
using OSharp.Core.Context;
using OSharp.Core.Data;
using OSharp.Core.Security;
using OSharp.Web.Mvc.Extensions;
using OSharp.Web.UI;


namespace OSharp.SiteBase.Extensions
{
    /// <summary>
    /// 扩展辅助操作方法
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 获取MVC操作的相关功能信息
        /// </summary>
        public static IFunction GetExecuteFunction(this ControllerContext context)
        {
            const string key = Constants.CurrentFunctionKey;
            IDictionary items = context.HttpContext.Items;
            if (items.Contains(key))
            {
                return (IFunction)items[key];
            }
            string area = context.GetAreaName();
            string controller = context.GetControllerName();
            string action = context.GetActionName();
            IFunction function = OSharpContext.Current.FunctionHandler.GetFunction(area, controller, action);
            if (function != null)
            {
                items.Add(key, function);
            }
            return function;
        }

        /// <summary>
        /// 获取MVC操作的相关功能信息
        /// </summary>
        public static IFunction GetExecuteFunction(this ControllerBase controller)
        {
            return controller.ControllerContext.GetExecuteFunction();
        }

        /// <summary>
        /// 将分页数据转换为表格数据格式
        /// </summary>
        public static GridData<TData> ToGridData<TData>(this PageResult<TData>pageResult )
        {
            return new GridData<TData>(pageResult.Data, pageResult.Total);
        }
    }
}
