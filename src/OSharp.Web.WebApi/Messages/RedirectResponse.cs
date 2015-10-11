using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace OSharp.Web.Http.Messages
{
    public class RedirectResponse : ResourceIdentifierBase
    {
        public RedirectResponse()
            : base(HttpStatusCode.Redirect)
        {
        }

        public RedirectResponse(Uri resource)
            : base(HttpStatusCode.Redirect, resource)
        {
        }
    }
}
