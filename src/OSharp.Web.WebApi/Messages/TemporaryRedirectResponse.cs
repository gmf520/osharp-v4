using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;


namespace OSharp.Web.Http.Messages
{
    public class TemporaryRedirectResponse : ResourceIdentifierBase
    {
        public TemporaryRedirectResponse()
            : base(HttpStatusCode.TemporaryRedirect)
        { }

        public TemporaryRedirectResponse(Uri resource)
            : base(HttpStatusCode.TemporaryRedirect, resource)
        { }
    }
}