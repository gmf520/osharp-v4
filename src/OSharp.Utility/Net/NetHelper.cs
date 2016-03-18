// -----------------------------------------------------------------------
//  <copyright file="NetHelper.cs" company="柳柳软件">
//      Copyright (c) 2016 66SOFT. All rights reserved.
//  </copyright>
//  <site>http://www.66soft.net</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-16 13:07</last-date>
// -----------------------------------------------------------------------

using System.Runtime.InteropServices;


namespace OSharp.Utility.Net
{
    /// <summary>
    /// 网络辅助操作
    /// </summary>
    public static class NetHelper
    {
        /// <summary>
        /// 网络是否畅通
        /// </summary>
        public static bool IsInternetConnected()
        {
            int i;
            bool state = InternetGetConnectedState(out i, 0);
            return state;
        }

        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int connectionDescription, int reservedValue);
    }
}
