using System;
using System.Threading.Tasks;

namespace AutoWakeUp
{
    internal static class SystemActions
    {
        public static void WaitThenDoStuff(DateTime time, Action stuff)
        {
            Task.Run(() =>
            {
                IntPtr timer = NativeMethods.CreateWaitableTimer(IntPtr.Zero, 1, null);
                long abs_utc_time = time.ToFileTimeUtc();
                NativeMethods.SetWaitableTimer(timer, ref abs_utc_time, 0, IntPtr.Zero, IntPtr.Zero, 1);
                NativeMethods.WaitForSingleObject(timer, NativeMethods.INFINITE);

                stuff();
            });
        }

        public static void SystemSleep()
        {
            NativeMethods.SetSuspendState(NativeMethods.FALSE, NativeMethods.FALSE, NativeMethods.FALSE);
        }

        public static bool SystemShutdown()
        {
            IntPtr hToken;
            var tkp = new NativeMethods.TOKEN_PRIVILEGES();
            tkp.Privileges = new NativeMethods.LUID_AND_ATTRIBUTES[NativeMethods.ANYSIZE_ARRAY];

            if (NativeMethods.OpenProcessToken(NativeMethods.GetCurrentProcess(), NativeMethods.TOKEN_ADJUST_PRIVILEGES | NativeMethods.TOKEN_QUERY, out hToken) == 0)
            {
                return false;
            }

            NativeMethods.LookupPrivilegeValue(null, NativeMethods.SE_SHUTDOWN_NAME, out tkp.Privileges[0].Luid);

            tkp.PrivilegeCount = 1;
            tkp.Privileges[0].Attributes = NativeMethods.SE_PRIVILEGE_ENABLED;

            NativeMethods.AdjustTokenPrivileges(hToken, NativeMethods.FALSE, ref tkp, 0, NativeMethods.NULL, NativeMethods.NULL);

            if (NativeMethods.GetLastError() != NativeMethods.ERROR_SUCCESS)
            {
                return false;
            }

            if (NativeMethods.ExitWindowsEx(NativeMethods.EWX_SHUTDOWN | NativeMethods.EWX_FORCE | NativeMethods.EWX_HYBRID_SHUTDOWN, NativeMethods.SHTDN_REASON_MAJOR_OTHER | NativeMethods.SHTDN_REASON_MINOR_MAINTENANCE | NativeMethods.SHTDN_REASON_FLAG_PLANNED) == 0)
            {
                return false;
            }

            return true;
        }
    }
}
