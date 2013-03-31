using System;
using Microsoft.Win32;

namespace FixFontRegistry
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string tt_postfix = " (TrueType)";
            string ot_postfix = " (OpenType)";
            string[] tt_ext = new string[] { ".ttf", ".ttc" };
            string[] ot_ext = new string[] { ".otf" };
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", RegistryKeyPermissionCheck.ReadWriteSubTree);
            foreach (var name in key.GetValueNames())
            {
                var value = key.GetValue(name) as string;
                if (IsMatchValue(value, tt_ext))
                {
                    if (!IsMatchName(name, tt_postfix))
                    {
                        Console.WriteLine("{0} : {1}", name, value);
                        if (IsMatchName(name, ot_postfix))
                        {
                            RenameRegistryValue(key, name, name.Substring(0, name.Length - ot_postfix.Length) + tt_postfix);
                            Console.WriteLine("Fixed.", name, value);
                        }
                    }
                }
                else if (IsMatchValue(value, ot_ext))
                {
                    if (!IsMatchName(name, ot_postfix))
                    {
                        Console.WriteLine("{0} : {1}", name, value);
                        if (IsMatchName(name, tt_postfix))
                        {
                            RenameRegistryValue(key, name, name.Substring(0, name.Length - tt_postfix.Length) + ot_postfix);
                            Console.WriteLine("Fixed.", name, value);
                        }
                    }
                }
            }
            Console.ReadLine();
        }

        private static bool IsMatchValue(string value, string[] exts)
        {
            foreach (var ext in exts)
            {
                if (value.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsMatchName(string name, string postfix)
        {
            if (name.EndsWith(postfix, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public static void RenameRegistryValue(RegistryKey key, string name, string new_name)
        {
            key.SetValue(new_name, key.GetValue(name));
            key.DeleteValue(name);
        }
    }
}
