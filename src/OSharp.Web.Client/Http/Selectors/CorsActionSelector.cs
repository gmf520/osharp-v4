using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using OSharp.Web.Http.Filters;


namespace OSharp.Web.Http.Selectors
{
    public class CorsActionSelector : ApiControllerActionSelector
    {
        private const string Origin = "Origin";
        private const string AccessControlRequestMethod = "Access-Control-Request-Method";
        private const string AccessControlRequestHeaders = "Access-Control-Request-Headers";
        private const string AccessControlAllowMethods = "Access-Control-Allow-Methods";
        private const string AccessControlAllowHeaders = "Access-Control-Allow-Headers";

        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            var originalRequest = controllerContext.Request;
            var isCorsRequest = originalRequest.Headers.Contains(Origin);

            if (originalRequest.Method == HttpMethod.Options && isCorsRequest)
            {
                var currentAccessControlRequestMethod = originalRequest.Headers.GetValues(AccessControlRequestMethod).FirstOrDefault();

                if (!string.IsNullOrEmpty(currentAccessControlRequestMethod))
                {
                    var modifiedRequest = new HttpRequestMessage(
                        new HttpMethod(currentAccessControlRequestMethod),
                        originalRequest.RequestUri);
                    controllerContext.Request = modifiedRequest;
                    var actualDescriptor = base.SelectAction(controllerContext);
                    controllerContext.Request = originalRequest;

                    if (actualDescriptor != null && actualDescriptor.GetFilters().OfType<EnableCorsAttribute>().Any())
                        return new PreflightActionDescriptor(actualDescriptor, AccessControlRequestMethod);
                }
            }

            return base.SelectAction(controllerContext);
        }

        class PreflightActionDescriptor : HttpActionDescriptor
        {
            private readonly HttpActionDescriptor _originalAction;
            private readonly string _prefilghtAccessControlRequestMethod;

            public PreflightActionDescriptor(HttpActionDescriptor originalAction, string accessControlRequestMethod)
            {
                _originalAction = originalAction;
                _prefilghtAccessControlRequestMethod = accessControlRequestMethod;
            }

            public override string ActionName
            {
                get { return _originalAction.ActionName; }
            }

            public override Task<object> ExecuteAsync(HttpControllerContext controllerContext, IDictionary<string, object> arguments, CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Headers.Add(AccessControlAllowMethods, _prefilghtAccessControlRequestMethod);

                var requestedHeaders = string.Join(", ", controllerContext.Request.Headers.GetValues(AccessControlRequestHeaders));

                if (!string.IsNullOrEmpty(requestedHeaders))
                    response.Headers.Add(AccessControlAllowHeaders, requestedHeaders);

                var tcs = new TaskCompletionSource<object>();
                tcs.SetResult(response);
                return tcs.Task;
            }

            public override Collection<HttpParameterDescriptor> GetParameters()
            {
                return _originalAction.GetParameters();
            }

            public override Type ReturnType
            {
                get { return typeof(HttpResponseMessage); }
            }

            public override Collection<FilterInfo> GetFilterPipeline()
            {
                return _originalAction.GetFilterPipeline();
            }

            public override Collection<IFilter> GetFilters()
            {
                return _originalAction.GetFilters();
            }

            public override Collection<T> GetCustomAttributes<T>()
            {
                if (typeof(T).IsAssignableFrom(typeof(AllowAnonymousAttribute)))
                    return new Collection<T> { new AllowAnonymousAttribute() as T };

                return _originalAction.GetCustomAttributes<T>();
            }

            public override HttpActionBinding ActionBinding
            {
                get { return _originalAction.ActionBinding; }
                set { _originalAction.ActionBinding = value; }
            }
        }
    }
}
