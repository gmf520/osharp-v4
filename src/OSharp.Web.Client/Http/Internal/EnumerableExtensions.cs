﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSharp.Web.Http.Internal
{
    internal static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (var item in enumeration)
            {
                action(item);
            }
        }
    }
}
