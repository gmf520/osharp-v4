// -----------------------------------------------------------------------
//  <copyright file="MethodCallInfo.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-20 15:58</last-date>
// -----------------------------------------------------------------------

using System;


namespace OSharp.Web.SignalR.Client.Reflection
{
    /// <summary>
    /// Container for method name and parameters
    /// </summary>
    internal class MethodCallInfo
    {
        /// <summary> Name of reflected method </summary>
        public string MethodName { get; set; }

        /// <summary> Parameter values </summary>
        public Type[] ParameterTypes { get; set; }
    }
}