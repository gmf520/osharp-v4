using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;


namespace OSharp.Web.Http.Messages
{
    public class CreateResponse : ResourceResponseBase
    {
        public CreateResponse()
            : base(HttpStatusCode.Created)
        { }

        public CreateResponse(IApiResource resource)
            : base(HttpStatusCode.Created, resource)
        { }
    }


    public class CreateResponse<T> : ResourceResponseBase<T>
    {
        public CreateResponse(T resource, IEnumerable<MediaTypeWithQualityHeaderValue> accept, IEnumerable<MediaTypeFormatter> formatters)
            : base(HttpStatusCode.Created, resource, accept, formatters)
        { }
    }
}