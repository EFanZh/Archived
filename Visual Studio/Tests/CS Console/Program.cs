using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSConsole
{
    internal static class Program
    {
        private static async Task G()
        {
            await Task.Run(() => Thread.Sleep(1000));
        }

        private async static void F()
        {
            await G();
            Console.WriteLine("[{0}] F", Thread.CurrentThread.ManagedThreadId);
        }

        public static void Main()
        {
            F();
            Console.WriteLine("[{0}] Main", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
        }
    }
}
