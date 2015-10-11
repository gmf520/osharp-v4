using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace OSharp.Web.Http.Messages
{
    public class MovedPermanentlyResponse : ResourceIdentifierBase
    {
        public MovedPermanentlyResponse()
            : base(HttpStatusCode.MovedPermanently)
        {
        }

        public MovedPermanentlyResponse(Uri resource)
            : base(HttpStatusCode.MovedPermanently, resource)
        {
        }
    }
}
