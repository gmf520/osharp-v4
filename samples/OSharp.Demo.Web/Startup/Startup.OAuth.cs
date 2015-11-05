using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Owin;
using Microsoft.Owin.Builder;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

using OSharp.Core.Dependency;
using OSharp.Core.Security;

using Owin;


namespace OSharp.Demo.Web
{
    public partial class Startup
    {
        private static void ConfigureOAuth(IAppBuilder app, IServiceProvider provider)
        {
            IOAuthAuthorizationServerProvider oauthServerProvider = provider.GetService<IOAuthAuthorizationServerProvider>();
            if (oauthServerProvider == null)
            {
                throw new InvalidOperationException("OAuth服务提供者获取失败，请确保已在依赖注入中初始化IOAuthAuthorizationServerProvider接口");
            }
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString(Paths.TokenPath),
                AuthorizeEndpointPath = new PathString(Paths.AuthorizePath),
                ApplicationCanDisplayErrors = true,
                AuthenticationMode = AuthenticationMode.Active,
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(2),
#if DEBUG
                AllowInsecureHttp = true,
#endif
                Provider = oauthServerProvider,
                AuthorizationCodeProvider = new OsharpAuthorizationCodeProvider(),
                RefreshTokenProvider = new OsharpRefreshTokenProvider()
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}