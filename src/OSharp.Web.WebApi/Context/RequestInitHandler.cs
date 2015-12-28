// -----------------------------------------------------------------------
//  <copyright file="RequestInitHandler.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>liuxx001</last-editor>
//  <last-date>2015-12-29 1:09</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSharp.Web.Http.Context
{
    /// <summary>
    /// 保存当前请求HttpRequestMessage
    /// </summary>
    public class RequestInitHandler: DelegatingHandler
    {
        /// <summary>
        /// 请求处理
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            OSharpWebApiContext.Current = new OSharpWebApiContext();
            OSharpWebApiContext.Current.DependencyScope = request.GetDependencyScope();
            return base.SendAsync(request, cancellationToken);
        }
    }
}
