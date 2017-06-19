using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

using OSharp.Web.Http.Internal;


namespace OSharp.Web.Http.Handlers
{
    /// <summary>
    /// 请求的生命周期作用域处理器
    /// </summary>
    public class RequestLifetimeScopeHandler : DelegatingHandler
    {
        /// <summary>
        /// Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
        /// </summary>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task`1"/>. The task object representing the asynchronous operation.
        /// </returns>
        /// <param name="request">The HTTP request message to send to the server.</param><param name="cancellationToken">A cancellation token to cancel operation.</param><exception cref="T:System.ArgumentNullException">The <paramref name="request"/> was null.</exception>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            IDependencyScope scope = CallContext.LogicalGetData(InternalConstants.RequestLifetimeScopeKey) as IDependencyScope;
            if (scope == null)
            {
                scope = request.GetDependencyScope();
                CallContext.LogicalSetData(InternalConstants.RequestLifetimeScopeKey, scope);
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}
