using System;
using System.Windows.Forms;

namespace Test
{
    public class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}