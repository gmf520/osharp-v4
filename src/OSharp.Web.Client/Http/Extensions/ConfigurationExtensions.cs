using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;
using System.Web.Http;


namespace OSharp.Web.Http.Extensions
{
    public static class ConfigurationExtensions
    {
        private static readonly IDictionary<CustomErrorsMode, IncludeErrorDetailPolicy> PolicyLookup = new Dictionary
            <CustomErrorsMode, IncludeErrorDetailPolicy>
        {
            { CustomErrorsMode.RemoteOnly, IncludeErrorDetailPolicy.LocalOnly },
            { CustomErrorsMode.On, IncludeErrorDetailPolicy.Never },
            { CustomErrorsMode.Off, IncludeErrorDetailPolicy.Always },
        };

        public static void UseWebConfigCustomErrors(this HttpConfiguration configuration)
        {
            var config = (CustomErrorsSection)ConfigurationManager.GetSection("system.web/customErrors");

            configuration.IncludeErrorDetailPolicy = PolicyLookup[config.Mode];
        }
    }
}
