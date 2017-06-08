// -----------------------------------------------------------------------
//  <copyright file="AppBuilderExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-06 6:16</last-date>
// -----------------------------------------------------------------------

using System;

using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

using OSharp.Core.Dependency;
using OSharp.Core.Security.Properties;

using Owin;


namespace OSharp.Core.Security
{
    /// <summary>
    /// AppBuilder扩展操作类
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// 初始化OAuth
        /// </summary>
        /// <param name="app"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IAppBuilder ConfigureOAuth(this IAppBuilder app, IServiceProvider provider)
        {
            IOAuthAuthorizationServerProvider oauthServerProvider = provider.GetService<IOAuthAuthorizationServerProvider>();
            if (oauthServerProvider == null)
            {
                throw new InvalidOperationException(Resources.OAuthServerProviderIsNull);
            }
            IAuthorizationCodeProvider authorizationCodeProvider = provider.GetService<IAuthorizationCodeProvider>();
            if (authorizationCodeProvider == null)
            {
                throw new InvalidOperationException(Resources.AuthorizationCodeProviderIsNull);
            }
            IRefreshTokenProvider refreshTokenProvider = provider.GetService<IRefreshTokenProvider>();
            if (refreshTokenProvider == null)
            {
                throw new InvalidOperationException(Resources.RefreshTokenProviderIsNull);
            }
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/token"),
                AuthorizeEndpointPath =  new PathString("/authorize"),
                ApplicationCanDisplayErrors = true,
                AuthenticationMode = AuthenticationMode.Active,
#if DEBUG
                AllowInsecureHttp = true,
#endif
                Provider = oauthServerProvider,
                AuthorizationCodeProvider = authorizationCodeProvider,
                RefreshTokenProvider = refreshTokenProvider
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            return app;
        }
    }
}