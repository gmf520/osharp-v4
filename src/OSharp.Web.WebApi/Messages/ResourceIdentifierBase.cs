using System;
using System.Net;
using System.Net.Http;


namespace OSharp.Web.Http.Messages
{
    public abstract class ResourceIdentifierBase : HttpResponseMessage
    {
        protected ResourceIdentifierBase(HttpStatusCode httpStatusCode)
            : base(httpStatusCode)
        {
        }

        protected ResourceIdentifierBase(HttpStatusCode httpStatusCode, Uri resource)
            : this(httpStatusCode)
        {
            Headers.Location = resource;
        }
    }
}
