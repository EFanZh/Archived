using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace EmfToWmf
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                using (Metafile inFile = new Metafile(args[0]))
                {
                    using (Metafile outFile = new Metafile(args[1], Graphics.FromHwnd(IntPtr.Zero).GetHdc(), EmfType.EmfOnly))
                    {
                        using (Graphics graphics = Graphics.FromImage(outFile))
                        {
                            graphics.DrawImage(inFile, Point.Empty);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine("Error: {0}", exception.Message);
            }
        }
    }
}
