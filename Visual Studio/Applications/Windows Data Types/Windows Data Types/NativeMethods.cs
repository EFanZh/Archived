using System;
using System.Runtime.InteropServices;

namespace WindowsDataTypes
{
    internal static class NativeMethods
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        public extern static Int32 SetWindowTheme(IntPtr hWnd, String pszSubAppName, String pszSubIdList);
    }
}
