// -----------------------------------------------------------------------
//  <copyright file="IdentityExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-08 0:33</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNet.Identity;

using OSharp.Utility.Data;
using OSharp.Utility.Extensions;


namespace OSharp.Core.Identity
{
    /// <summary>
    /// 认证扩展辅助操作
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// 将<see cref="IdentityResult"/>转化为<see cref="OperationResult"/>
        /// </summary>
        public static OperationResult ToOperationResult(this IdentityResult identityResult)
        {
            return identityResult.Succeeded
                ? new OperationResult(OperationResultType.Success)
                : new OperationResult(OperationResultType.Error, identityResult.Errors.ExpandAndToString());
        }
    }
}