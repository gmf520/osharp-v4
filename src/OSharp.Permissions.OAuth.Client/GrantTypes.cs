// -----------------------------------------------------------------------
//  <copyright file="GrantTypes.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-06 8:06</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;


namespace OSharp.Web.Http.OAuth
{
    public static class GrantTypes
    {
        private const string KeyName = "grant_type";

        public static KeyValuePair<string, string> ClientCredentials
        {
            get { return new KeyValuePair<string, string>(KeyName, "client_credentials"); }
        }

        public static KeyValuePair<string, string> RefreshToken
        {
            get { return new KeyValuePair<string, string>(KeyName, "refresh_token"); }
        }

        public static KeyValuePair<string, string> OwnerCredentials
        {
            get { return new KeyValuePair<string, string>(KeyName, "password");}
        }
    }
}