using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchColors
{
    internal class Program
    {
        private static bool TestColor(string file, Searcher searcher)
        {
            bool result = false;

            try
            {
                using (Bitmap bitmap = new Bitmap(file))
                {
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            if (searcher.Test(file, bitmap.GetPixel(x, y)))
                            {
                                result = true;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message);
            }

            return result;
        }

        private static void Main(string[] args)
        {
            Searcher searcher = new Searcher(Color.FromArgb(0, 0, 255));

            Parallel.ForEach(Directory.GetFiles(@"E:\Colors", "*", SearchOption.AllDirectories), f =>
            {
                if (TestColor(f, searcher))
                {
                    Console.WriteLine(f);
                    Console.WriteLine("Color: {0}", searcher.FoundColor);
                }
            });
        }
    }
}
