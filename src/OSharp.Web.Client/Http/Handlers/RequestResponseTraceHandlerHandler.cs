using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using OSharp.Web.Http.Extensions;
using OSharp.Web.Http.Logging;


namespace OSharp.Web.Http.Handlers
{
    /// <summary>
    /// 消息处理——请求响应跟踪
    /// </summary>
    public class RequestResponseTraceHandler : DelegatingHandler
    {
        private readonly ILoggingRepository _repository;

        public RequestResponseTraceHandler(ILoggingRepository repository)
        {
            _repository = repository;
        }

        public RequestResponseTraceHandler(HttpMessageHandler innerHandler, ILoggingRepository repository)
            : base(innerHandler)
        {
            _repository = repository;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Log the request information
            LogRequestLoggingInfo(request);

            // Execute the request
            return base.SendAsync(request, cancellationToken).ContinueWith(task =>
            {
                var response = task.Result;
                // Extract the response logging info then persist the information
                LogResponseLoggingInfo(response);
                return response;
            });
        }

        private void LogRequestLoggingInfo(HttpRequestMessage request)
        {
            var info = new ApiLoggingInfo
            {
                HttpMethod = request.Method.Method,
                UriAccessed = request.RequestUri.AbsoluteUri,
                IpAddress = request.GetClientIpAddress(),
                MessageType = HttpMessageType.Request,
                LoggingLevel = LoggingLevel.Trace
            };

            ExtractMessageHeadersIntoLoggingInfo(info, request.Headers.ToList());

            if (request.Content != null)
            {
                request.Content.ReadAsByteArrayAsync()
                    .ContinueWith(task =>
                    {
                        info.BodyContent = Encoding.UTF8.GetString(task.Result);
                        _repository.Log(info);

                    });

                return;
            }

            _repository.Log(info);
        }

        private void LogResponseLoggingInfo(HttpResponseMessage response)
        {
            var info = new ApiLoggingInfo
            {
                MessageType = HttpMessageType.Response,
                HttpMethod = response.RequestMessage.Method.ToString(),
                ResponseStatusCode = response.StatusCode,
                ResponseStatusMessage = response.ReasonPhrase,
                UriAccessed = response.RequestMessage.RequestUri.AbsoluteUri,
                IpAddress = response.RequestMessage.GetClientIpAddress(),
                LoggingLevel = LoggingLevel.Trace
            };

            ExtractMessageHeadersIntoLoggingInfo(info, response.Headers.ToList());

            if (response.Content != null)
            {
                response.Content.ReadAsByteArrayAsync()
                    .ContinueWith(task =>
                    {
                        var responseMsg = Encoding.UTF8.GetString(task.Result);
                        info.BodyContent = responseMsg;
                        _repository.Log(info);
                    });

                return;
            }

            _repository.Log(info);
        }

        private void ExtractMessageHeadersIntoLoggingInfo(ApiLoggingInfo info, List<KeyValuePair<string, IEnumerable<string>>> headers)
        {
            headers.ForEach(h =>
            {
                // convert the header values into one long string from a series of IEnumerable<string> values so it looks for like a HTTP header
                var headerValues = new StringBuilder();

                if (h.Value != null)
                {
                    foreach (var hv in h.Value)
                    {
                        if (headerValues.Length > 0)
                        {
                            headerValues.Append(", ");
                        }
                        headerValues.Append(hv);
                    }
                }
                info.Headers.Add(string.Format("{0}: {1}", h.Key, headerValues.ToString()));
            });
        }
    }
}
