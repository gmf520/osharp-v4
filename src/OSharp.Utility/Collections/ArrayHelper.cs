// -----------------------------------------------------------------------
//  <copyright file="ArrayHelper.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>谢龙江</last-editor>
//  <last-date>2015-10-18 8:53</last-date>
// -----------------------------------------------------------------------

using System;


namespace OSharp.Utility.Collections
{
    /// <summary>
    /// 判断对象是否为数组
    /// </summary>
    public static class ArrayHelper
    {
        public static bool IsArray(this object o)
        {
            if (null == o) return false;
            return o.GetType().BaseType == typeof(Array);
        }
    }
}