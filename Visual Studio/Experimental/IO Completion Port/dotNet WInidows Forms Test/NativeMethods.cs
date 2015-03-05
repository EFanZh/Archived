using System;
using System.Runtime.InteropServices;
using BOOL = System.Boolean;
using DWORD = System.UInt32;
using HANDLE = System.IntPtr;

using ULONG_PTR = System.IntPtr;

namespace dotNetWInidowsFormsTest
{
    internal static class NativeMethods
    {
        // ReSharper disable All

        public const UInt32 INFINITE = 0xFFFFFFFF;
        public static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        [DllImport("Kernel32.dll")]
        public static extern HANDLE CreateIoCompletionPort([In] HANDLE FileHandle, [In, Optional] HANDLE ExistingCompletionPort, [In] ULONG_PTR CompletionKey, [In] DWORD NumberOfConcurrentThreads);

        [DllImport("Kernel32.dll")]
        public static extern BOOL PostQueuedCompletionStatus([In] HANDLE CompletionPort, [In] DWORD dwNumberOfBytesTransferred, [In] ULONG_PTR dwCompletionKey, [In, Optional] IntPtr lpOverlapped);

        [DllImport("Kernel32.dll")]
        public static extern BOOL GetQueuedCompletionStatus([In] HANDLE CompletionPort, [Out] out DWORD lpNumberOfBytes, [Out] out ULONG_PTR lpCompletionKey, [Out] out IntPtr lpOverlapped, [In] DWORD dwMilliseconds);

        // ReSharper restore All
    }
}
