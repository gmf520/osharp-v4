﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSharp.Web.Http.Caching
{
    public class ThrottleEntry
    {
        public DateTime PeriodStart { get; set; }
        public long Requests { get; set; }

        public ThrottleEntry()
        {
            PeriodStart = DateTime.UtcNow;
            Requests = 0;
        }
    }
}
