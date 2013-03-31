using System;
using System.Collections.Generic;

namespace ImgProc
{
    internal static class Utilities
    {
        static Utilities()
        {
            ProcessorCount = Environment.ProcessorCount;
        }

        public static int ProcessorCount
        {
            get;
            private set;
        }

        public static string CombinePath(string path1, string path2)
        {
            string s;
            if (path1.EndsWith("\\") || path1.EndsWith("/"))
            {
                s = path1.Substring(0, path1.Length - 1);
            }
            else
            {
                s = path1;
            }
            if (path2.StartsWith("\\") || path2.StartsWith("/"))
            {
                s += path2;
            }
            else
            {
                s += "\\" + path2;
            }
            return s;
        }

        public static ISet<T> CreateInstanceSet<T>(ISet<Type> typeSet)
            where T : class
        {
            var hs = new HashSet<T>();
            foreach (var type in typeSet)
            {
                hs.Add(Activator.CreateInstance(type) as T);
            }
            return hs;
        }

        public static bool IsTypeOf(this Type type0, Type type)
        {
            if (type.IsClass)
            {
                return type0.IsSubclassOf(type);
            }
            else if (type.IsInterface)
            {
                return type0.GetInterface(type.Name) != null;
            }
            else
            {
                return false;
            }
        }

        public static int IntRound(double a)
        {
            return (int)Math.Round(a);
        }
    }
}
