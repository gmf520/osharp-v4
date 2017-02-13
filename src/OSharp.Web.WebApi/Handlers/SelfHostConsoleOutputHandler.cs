using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace OSharp.Web.Http.Handlers
{
    public class SelfHostConsoleOutputHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine(@"{0}	:	{1}	- {2}", DateTime.Now, request.RequestUri.AbsolutePath, request.Method);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
