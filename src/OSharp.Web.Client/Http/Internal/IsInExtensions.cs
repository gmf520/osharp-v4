using System;
using System.Linq;


namespace OSharp.Web.Http.Internal
{
    internal static class IsInExtensions
    {
        public static bool IsIn(this object item, params object[] list)
        {
            if (list == null || list.Length == 0)
                return false;
            return list.Any(x => x == item);
        }

        public static bool IsIn(this string item, params string[] list)
        {
            if (list == null || list.Length == 0)
                return false;

            return list.Any(x => x == item || (x != null && x.Equals(item, StringComparison.CurrentCultureIgnoreCase)));
        }

    }
}
