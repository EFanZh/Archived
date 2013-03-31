using System.Reflection;
using System.Windows.Forms;

namespace Test
{
    internal static class ExtensionMethods
    {
        public static void SetDoubleBuffered(this ListView listview, bool value)
        {
            PropertyInfo pi = typeof(ListView).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            pi.SetValue(listview, value);
        }

        public static void SetWindowTheme(this ListView listview, string pszSubAppName, string pszSubIdList)
        {
            NativeMethods.SetWindowTheme(listview.Handle, pszSubAppName, pszSubIdList);
        }
    }
}
