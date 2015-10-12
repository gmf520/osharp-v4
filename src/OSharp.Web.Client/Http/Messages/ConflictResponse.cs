﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace OSharp.Web.Http.Messages
{
    public class ConflictResponse : ResourceResponseBase
    {
        public ConflictResponse()
            : base(HttpStatusCode.Conflict)
        {
        }

        public ConflictResponse(IApiResource apiResource)
            : base(HttpStatusCode.Conflict, apiResource)
        {
        }
    }

    public class ConflictResponse<T> : ResourceResponseBase<T>
    {
        public ConflictResponse(T resource, IEnumerable<MediaTypeWithQualityHeaderValue> accept, IEnumerable<MediaTypeFormatter> formatters)
            : base(HttpStatusCode.Conflict, resource, accept, formatters)
        {
        }
    }
}
