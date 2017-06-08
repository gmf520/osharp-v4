using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;

using OSharp.Core.Dependency;
using OSharp.Core.Security;
using OSharp.Demo.Identity;
using OSharp.Demo.Models.Identity;
using OSharp.Demo.OAuth;

using Owin;


namespace OSharp.Demo.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDemoServices(this IServiceCollection services, IAppBuilder app)
        {
            //Identity
            services.AddScoped<RoleManager<Role, int>, RoleManager>();
            services.AddScoped<UserManager<User, int>, UserManager>();
            services.AddScoped<SignInManager<User, int>, SignInManager>();
            services.AddScoped<IAuthenticationManager>(_ => HttpContext.Current.GetOwinContext().Authentication);
            services.AddScoped<IDataProtectionProvider>(_ => app.GetDataProtectionProvider());

            //Security
            //services.AddScoped<FunctionMapStore>();
            //services.AddScoped<EntityMapStore>();

            //OAuth
            services.AddScoped<OAuthClientStore>();
            services.AddScoped<IOAuthClientRefreshTokenStore, OAuthClientRefreshTokenStore>();
        }
    }
}
