using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace OSharp.Web.Http.Handlers
{
    public class TraceMessageHandler : DelegatingHandler
    {
        public TraceMessageHandler(DelegatingHandler innerChannel)
            : base(innerChannel)
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            if (request.Method == HttpMethod.Trace)
            {
                return Task<HttpResponseMessage>.Factory.StartNew(
                    () =>
                    {
                        var response = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StringContent(request.ToString(), Encoding.UTF8, "message/http")
                        };
                        return response;
                    }, cancellationToken);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
