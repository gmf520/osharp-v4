// -----------------------------------------------------------------------
//  <copyright file="RoleLimitAttribute.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-13 9:48</last-date>
// -----------------------------------------------------------------------

using System;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 指定功能只允许特定角色可以访问
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RoleLimitAttribute : Attribute
    { }
}