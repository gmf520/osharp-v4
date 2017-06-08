// -----------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-31 2:15</last-date>
// -----------------------------------------------------------------------

using Microsoft.Owin.Security.OAuth;

using OSharp.Core.Dependency;
using OSharp.Utility;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 服务映射信息集合扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加OAuth服务映射令牌
        /// </summary>
        public static void AddOAuthServices(this IServiceCollection services)
        {
            services.CheckNotNull("services");
            services.AddSingleton<IOAuthAuthorizationServerProvider, OsharpAuthorizationServerProvider>();
            services.AddSingleton<IAuthorizationCodeProvider, OsharpAuthorizationCodeProvider>();
            services.AddSingleton<IRefreshTokenProvider, OsharpRefreshTokenProvider>();

            services.AddSingleton<IOAuthClientIdProvider, DatetimeOAuthClientIdProvider>();
            services.AddSingleton<IOAuthClientSecretProvider, GuidOAuthClientSecretProvider>();
        }
    }
}