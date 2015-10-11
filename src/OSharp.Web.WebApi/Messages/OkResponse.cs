﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;


namespace OSharp.Web.Http.Messages
{
    public class OkResponse : HttpResponseMessage
    {
        public OkResponse()
            : base(HttpStatusCode.OK)
        { }
    }


    public class OkResponse<T> : ResourceResponseBase<T>
    {
        public OkResponse(T resource, IEnumerable<MediaTypeWithQualityHeaderValue> accept, IEnumerable<MediaTypeFormatter> formatters)
            : base(HttpStatusCode.OK, resource, accept, formatters)
        { }
    }
}