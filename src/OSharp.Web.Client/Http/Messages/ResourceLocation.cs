using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSharp.Web.Http.Messages
{
    public class ResourceLocation
    {
        public Uri Location { get; private set; }

        public void Set(Uri location)
        {
            Location = location;
        }

    }
}
