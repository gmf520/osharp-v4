﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace OSharp.Web.Http.Messages
{
    public class NotModifiedResponse : ResourceResponseBase
    {
        public NotModifiedResponse()
            : base(HttpStatusCode.NotModified)
        {
        }

        public NotModifiedResponse(IApiResource resource)
            : base(HttpStatusCode.NotModified, resource)
        {
        }

        public NotModifiedResponse(EntityTagHeaderValue etag)
            : base(HttpStatusCode.NotModified)
        {
            this.Headers.ETag = etag;
        }

    }

    public class NotModifiedResponse<T> : ResourceResponseBase<T>
    {
        public NotModifiedResponse(T resource, IEnumerable<MediaTypeWithQualityHeaderValue> accept, IEnumerable<MediaTypeFormatter> formatters)
            : base(HttpStatusCode.NotModified, resource, accept, formatters)
        {
        }
    }
}
