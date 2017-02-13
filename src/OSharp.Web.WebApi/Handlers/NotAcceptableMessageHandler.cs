using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

using OSharp.Web.Http.Extensions;


namespace OSharp.Web.Http.Handlers
{
    public class NotAcceptableMessageHandler : DelegatingHandler
    {
        private readonly IContentNegotiator _contentNegotiator;
        private readonly IEnumerable<MediaTypeFormatter> _mediaTypeFormatters;

        public NotAcceptableMessageHandler(HttpConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            _contentNegotiator = configuration.Services.GetContentNegotiator();
            _mediaTypeFormatters = configuration.Formatters;
        }

        public NotAcceptableMessageHandler(HttpConfiguration configuration, HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            _contentNegotiator = configuration.Services.GetContentNegotiator();
            _mediaTypeFormatters = configuration.Formatters;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            ContentNegotiationResult result = _contentNegotiator.Negotiate(_mediaTypeFormatters, request.Headers.Accept);
            if (result == null)
            {
                return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.NotAcceptable));
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}