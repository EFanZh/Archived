using System;
using System.Collections.Generic;

namespace FontViewer
{
    internal static class Utility
    {
        public static bool TryFindFirst<T>(this IEnumerable<T> source, Predicate<T> predicate, out T result)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    result = item;
                    return true;
                }
            }

            result = default(T);
            return false;
        }
    }
}
