using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

using OSharp.Core.Dependency;
using OSharp.Core.Identity;
using OSharp.Core.Security.Properties;
using OSharp.Utility;
using OSharp.Utility.Extensions;


namespace OSharp.Core.Security
{
    /// <summary>
    /// Osharp-OAuth验证实现服务提供者
    /// </summary>
    public class OsharpAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IOAuthClientValidator _ioAuthClientValidator;
        private readonly IPasswordValidator _passwordValidator;

        /// <summary>
        /// 初始化一个<see cref="OsharpAuthorizationServerProvider"/>类型的新实例
        /// </summary>
        public OsharpAuthorizationServerProvider(IServiceProvider serviceProvider)
        {
            serviceProvider.CheckNotNull("serviceProvider");
            _ioAuthClientValidator = serviceProvider.GetService<IOAuthClientValidator>();
            if (_ioAuthClientValidator == null)
            {
                throw new InvalidOperationException(Resources.ClientValidatorIsNull);
            }
            _passwordValidator = serviceProvider.GetService<IPasswordValidator>();
            if (_passwordValidator == null)
            {
                throw new InvalidOperationException(Resources.PasswordValidatorIsNull);
            }
        }

        /// <summary>
        /// 验证请求中的客户端Id与客户端密钥的合法性
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId, clientSecret;
            context.TryGetBasicCredentials(out clientId, out clientSecret);
            //判断客户端Id与客户端密钥的合法性，不合法的拦截
            bool validated = await _ioAuthClientValidator.Validate(clientId, clientSecret);
            if (!validated)
            {
                context.SetError("invalid_client", "client is not valid.");
                return;
            }
            context.Validated(clientId);
            await base.ValidateClientAuthentication(context);
        }

        /// <summary>
        /// 当客户端Id与客户端密钥验证通过后，生成在线票据令牌
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.ClientId));
            List<string> scopes = context.Scope.Where(m => !m.IsMissing()).ToList();
            if (scopes.Any())
            {
                identity.AddClaims(scopes.Select(m => new Claim("urn:oauth:scope", m)));
            }
            
            AuthenticationProperties properties = new AuthenticationProperties(
                new Dictionary<string, string>() { { "as:client_id", context.ClientId } });
            AuthenticationTicket ticket = new AuthenticationTicket(identity, properties);
            context.Validated(ticket);
            return Task.FromResult(0);
        }

        /// <summary>
        /// 验证用户名与密码，并生成在线票据令牌
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //调用登录服务验证用户名与密码
            bool validated = await _passwordValidator.Validate(context.UserName, context.Password);
            if (!validated)
            {
                context.SetError("invalid_user", "username or password is not valid.");
                return;
            }
            //生成令牌
            ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            List<string> scopes = context.Scope.Where(m => !m.IsMissing()).ToList();
            if (scopes.Any())
            {
                identity.AddClaims(scopes.Select(m => new Claim("urn:oauth:scope", m)));
            }

            AuthenticationProperties properties = new AuthenticationProperties(
                new Dictionary<string, string>() { { "as:client_id", context.ClientId } });
            AuthenticationTicket ticket = new AuthenticationTicket(identity, properties);
            context.Validated(ticket);
        }

        /// <summary>
        /// 刷新客户端令牌
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            string originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            string currentClient = context.ClientId;
            if (originalClient != currentClient)
            {
                context.Rejected();
                return Task.FromResult(0);
            }
            ClaimsIdentity identity = new ClaimsIdentity(context.Ticket.Identity);
            identity.AddClaim(new Claim("newClaim", "refreshToken"));
            AuthenticationTicket ticket = new AuthenticationTicket(identity, context.Ticket.Properties);
            context.Validated(ticket);
            return base.GrantRefreshToken(context);
        }

        /// <summary>
        /// 验证重定向域名是否符合当前客户端Id创建时填写的主域名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            string redirectUri = await _ioAuthClientValidator.GetRedirectUrl(context.ClientId);
            if (redirectUri != null)
            {
                context.Validated(redirectUri);
            }
            await Task.FromResult(0);
        }
    }
}
