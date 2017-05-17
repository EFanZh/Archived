using System;
using System.Windows.Forms;

namespace TheOldMatrixTextRain
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.Run(new MainForm());
        }
    }
}
