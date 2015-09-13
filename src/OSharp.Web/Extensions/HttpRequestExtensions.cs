// -----------------------------------------------------------------------
//  <copyright file="HttpRequestExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-12-23 19:18</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using OSharp.Utility.Extensions;


namespace OSharp.Web.Extensions
{
    /// <summary>
    /// <see cref="HttpRequest"/>扩展辅助操作类
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 获取IP地址
        /// </summary>
        public static string GetIpAddress(this HttpRequestBase request)
        {
            string result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (result.IsNullOrEmpty())
            {
                result = request.ServerVariables["REMOTE_ADDR"];
            }
            return result;
        }
    }
}