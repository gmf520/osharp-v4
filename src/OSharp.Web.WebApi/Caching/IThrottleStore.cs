using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSharp.Web.Http.Caching
{
    public interface IThrottleStore
    {
        bool TryGetValue(string key, out ThrottleEntry entry);
        void IncrementRequests(string key);
        void Rollover(string key);
        void Clear();
    }
}
