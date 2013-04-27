using System;
using System.Runtime.InteropServices;

namespace TextFileTools
{
    internal static class NativeMethods
    {
        public const Int32 COLOR_WINDOW = 5;
        public const Int32 GCLP_HBRBACKGROUND = -10;

        [DllImport("user32.dll", EntryPoint = "SetClassLong")]
        public static extern IntPtr SetClassLongPtr([In] IntPtr hWnd, [In] Int32 nIndex, [In] IntPtr dwNewLong);
    }
}
