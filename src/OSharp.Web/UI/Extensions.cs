using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Utility.Data;
using OSharp.Utility.Extensions;


namespace OSharp.Web.UI
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 操作结果转ajax操作结果
        /// </summary>
        public static AjaxResult ToAjaxResult(this OperationResult<object> result)
        {
            string content = result.Message ?? result.ResultType.ToDescription();
            AjaxResultType type = result.ResultType.ToAjaxResultType();
            return new AjaxResult(content, type);
        }

        /// <summary>
        /// 把业务结果类型<see cref="OperationResultType"/>转换为Ajax结果类型<see cref="AjaxResultType"/>
        /// </summary>
        public static AjaxResultType ToAjaxResultType(this OperationResultType resultType)
        {
            switch (resultType)
            {
                case OperationResultType.Success:
                    return AjaxResultType.Success;
                case OperationResultType.NoChanged:
                    return AjaxResultType.Info;
                default:
                    return AjaxResultType.Error;
            }
        }

        /// <summary>
        /// 判断业务结果类型是否是Error结果
        /// </summary>
        public static bool IsError(this OperationResultType resultType)
        {
            return resultType == OperationResultType.QueryNull || resultType == OperationResultType.ValidError
                || resultType == OperationResultType.Error;
        }
    }
}
