﻿// -----------------------------------------------------------------------
//  <copyright file="RegexPatterns.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-01-09 14:53</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Utility.Data
{
    /// <summary>
    /// 常用正则表达式字符串
    /// </summary>
    public class RegexPatterns
    {
        public const string Ip = @"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))";

        public const string SubstringFormat = "(?<=({0})).+(?=({1}))";

        public const string Email = @"[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+";

        public const string Unicode = @"[\u4E00-\u9FA5\uE815-\uFA29]+";

        public const string Url = @"(http|https|ftp|rtsp|mms):(\/\/|\\\\)[A-Za-z0-9%\-_@]+\.[A-Za-z0-9%\-_@]+[A-Za-z0-9\.\/=\?%\-&_~`@:\+!;]*";

        public const string MobileNumber = @"[1][3-8]\d{9}";
    }
}