﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSharp.Web.OAuth
{
    public static class GrantTypes
    {
        public static KeyValuePair<string, string> ClientCredentials
        {
            get { return new KeyValuePair<string, string>("grant_type", "client_credentials"); }
        }

        public static KeyValuePair<string, string> RefreshToken
        {
            get { return new KeyValuePair<string, string>("grant_type", "refresh_token"); }
        }
    }
}
