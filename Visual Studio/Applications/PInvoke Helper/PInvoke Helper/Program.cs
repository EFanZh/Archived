
namespace PInvokeHelper
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string data = @"BOOL CALLBACK EnumWindowsProc(
  _In_ HWND   hwnd,
  _In_ LPARAM lParam
);";

            Parser.Parser.Parse(data);
        }
    }
}
