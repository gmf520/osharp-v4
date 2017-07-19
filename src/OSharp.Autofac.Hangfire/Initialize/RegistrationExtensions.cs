using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac.Builder;


namespace OSharp.Autofac.Hangfire.Initialize
{
    public static class RegistrationExtensions
    {
        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InstancePerBackgroundJob<TLimit, TActivatorData, TStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TStyle> registration)
        {
            return InstancePerBackgroundJob(registration, new object[] { });
        }

        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InstancePerBackgroundJob<TLimit, TActivatorData, TStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TStyle> registration, params object[] lifetimeScopeTags)
        {
            if (registration == null)
            {
                throw new ArgumentNullException("registration");
            }
            object[] tags = new[] { AutofacJobActivator.LifetimeScopeTag }.Concat(lifetimeScopeTags).ToArray();
            return registration.InstancePerMatchingLifetimeScope(tags);
        }
    }
}
