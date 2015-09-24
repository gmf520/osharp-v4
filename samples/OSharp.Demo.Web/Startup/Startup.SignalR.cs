using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Owin;


namespace OSharp.Demo.Web
{
    public partial class Startup
    {
        public static void ConfigureSignalR(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}