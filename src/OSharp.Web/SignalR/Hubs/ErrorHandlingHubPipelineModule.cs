using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR.Hubs;


namespace OSharp.Web.SignalR.Hubs
{
    public class ErrorHandlingHubPipelineModule : HubPipelineModule
    {
        #region Overrides of HubPipelineModule

        protected override void OnIncomingError(ExceptionContext exceptionContext, IHubIncomingInvokerContext invokerContext)
        {
            string connectionId = invokerContext.Hub.Context.ConnectionId;
            Exception ex = exceptionContext.Error.InnerException ?? exceptionContext.Error;
            invokerContext.Hub.Clients.Caller.ExceptionHandler(ex);
        }

        #endregion
    }
}
