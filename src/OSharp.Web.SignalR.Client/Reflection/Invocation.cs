// -----------------------------------------------------------------------
//  <copyright file="Invocation.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-20 15:59</last-date>
// -----------------------------------------------------------------------

using System;


namespace OSharp.Web.SignalR.Client.Reflection
{
    /// <summary>
    /// Container for method name and parameter values
    /// </summary>
    internal class Invocation
    {
        /// <summary> Name of reflected method </summary>
        public string MethodName { get; set; }

        /// <summary> Parameter values </summary>
        public object[] ParameterValues { get; set; }

        /// <summary> Return type, or null if void return </summary>
        public Type ReturnType { get; set; }
    }
}