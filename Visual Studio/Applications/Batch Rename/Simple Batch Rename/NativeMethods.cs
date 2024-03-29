using System;
using System.Runtime.InteropServices;

namespace SimpleBatchRename
{
    internal static class NativeMethods
    {
        public const Int32 WM_NOTIFY = 0x004e;
        public const Int32 WM_LBUTTONDBLCLK = 0x0203;

        public const UInt32 LVN_FIRST = unchecked(0u - 100u);
        public const UInt32 LVN_GETEMPTYMARKUP = LVN_FIRST - 87;

        public const Int32 L_MAX_URL_LENGTH = 2084;

        [StructLayout(LayoutKind.Sequential)]
        public struct NMHDR
        {
            public IntPtr hwndFrom;
            public IntPtr idFrom;
            public UInt32 code;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct NMLVEMPTYMARKUP
        {
            public NMHDR hdr;
            public UInt32 dwFlags;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = L_MAX_URL_LENGTH)]
            public String szMarkup;
        }

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 SetWindowTheme(IntPtr hWnd, String pszSubAppName, String pszSubIdList);
    }
}
