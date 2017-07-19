// -----------------------------------------------------------------------
//  <copyright file="Extensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-09-12 11:05</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNet.SignalR;


namespace OSharp.Web.SignalR.Extensions
{
    /// <summary>
    /// 扩展辅助操作类
    /// </summary>
    public static class SignalRExtensions
    {
        /// <summary>
        /// 获取请求的IP地址
        /// </summary>
        public static string GetRemoteIp(this IRequest request)
        {
            object value;
            if (request.Environment.TryGetValue("server.RemoteIpAddress", out value))
            {
                return value.ToString();
            }
            return string.Empty;
        }
    }
}