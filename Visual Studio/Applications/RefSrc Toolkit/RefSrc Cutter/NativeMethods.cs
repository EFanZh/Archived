using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

namespace RefSrcCutter
{
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern SafeFileHandle CreateFile(string lpFileName, DesiredAccess dwDesiredAccess, ShareMode dwShareMode, IntPtr lpSecurityAttributes, CreationDisposition dwCreationDisposition, FileAttributes dwFlagsAndAttributes, IntPtr hTemplateFile);
    }
}
