using System;
using System.Runtime.InteropServices;

namespace AutoWakeUp
{
    internal static class NativeMethods
    {
        public const Int32 TRUE = 1;
        public const Int32 FALSE = 0;
        public const Int32 ANYSIZE_ARRAY = 1;
        public const Int32 ERROR_SUCCESS = 0;
        public const UInt32 EWX_FORCE = 0x00000004;
        public const UInt32 EWX_HYBRID_SHUTDOWN = 0x00400000;
        public const UInt32 EWX_SHUTDOWN = 0x00000001;
        public const UInt32 INFINITE = 0xFFFFFFFF;
        public const UInt32 SE_PRIVILEGE_ENABLED = 0x00000002;
        public const UInt32 SHTDN_REASON_FLAG_PLANNED = 0x80000000;
        public const UInt32 SHTDN_REASON_MAJOR_OTHER = 0x00000000;
        public const UInt32 SHTDN_REASON_MINOR_MAINTENANCE = 0x00000001;
        public const UInt32 TOKEN_ADJUST_PRIVILEGES = 0x0020;
        public const UInt32 TOKEN_QUERY = 0x0008;

        public const String SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

        public static readonly IntPtr NULL = IntPtr.Zero;

        public struct LUID
        {
            public UInt32 LowPart;
            public Int32 HighPart;
        }

        public struct LUID_AND_ATTRIBUTES
        {
            public LUID Luid;
            public UInt32 Attributes;
        }

        public struct TOKEN_PRIVILEGES
        {
            public UInt32 PrivilegeCount;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = ANYSIZE_ARRAY)]
            public LUID_AND_ATTRIBUTES[] Privileges;
        }

        #region advapi32.dll

        [DllImport("advapi32.dll")]
        public static extern Int32 AdjustTokenPrivileges([In] IntPtr TokenHandle, [In] Int32 DisableAllPrivileges, [In, Optional] ref TOKEN_PRIVILEGES NewState, [In] UInt32 BufferLength, [Out, Optional] IntPtr PreviousState, [Out, Optional] IntPtr ReturnLength);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 LookupPrivilegeValue([In, Optional] String lpSystemName, [In] String lpName, [Out] out LUID lpLuid);

        [DllImport("advapi32.dll")]
        public static extern Int32 OpenProcessToken([In] IntPtr ProcessHandle, [In] UInt32 DesiredAccess, [Out] out IntPtr TokenHandle);

        #endregion advapi32.dll

        #region kernel32.dll

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateWaitableTimer([In, Optional] IntPtr lpTimerAttributes, [In] Int32 bManualReset, [In, Optional] String lpTimerName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll")]
        public static extern UInt32 GetLastError();

        [DllImport("kernel32.dll")]
        public static extern Int32 SetWaitableTimer([In] IntPtr hTimer, [In] ref Int64 pDueTime, [In] Int32 lPeriod, [In, Optional] IntPtr pfnCompletionRoutine, [In, Optional] IntPtr lpArgToCompletionRoutine, [In] Int32 fResume);

        [DllImport("kernel32.dll")]
        public static extern UInt32 WaitForSingleObject([In] IntPtr hHandle, [In] UInt32 dwMilliseconds);

        #endregion kernel32.dll

        #region powrprof.dll

        [DllImport("powrprof.dll")]
        public static extern Byte SetSuspendState([In] Byte Hibernate, [In] Byte ForceCritical, [In] Byte DisableWakeEvent);

        #endregion powrprof.dll

        #region user32.dll

        [DllImport("user32.dll")]
        public static extern Int32 ExitWindowsEx([In] UInt32 uFlags, [In] UInt32 dwReason);

        #endregion user32.dll
    }
}
