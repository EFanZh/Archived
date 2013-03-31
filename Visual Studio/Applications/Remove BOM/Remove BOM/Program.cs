using System;
using System.IO;

namespace RemoveBOM
{
    internal class Program
    {
        private const int bom_size = 3;

        private static void RemoveBomFromFile(string path)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);

                var buffer = new byte[bom_size];

                fs.Read(buffer, 0, buffer.Length);
                if (buffer[0] == 0xef && buffer[1] == 0xbb & buffer[2] == 0xbf)
                {
                    var data = new byte[(int)fs.Length - bom_size];
                    fs.Read(data, 0, data.Length);
                    fs.Dispose();
                    fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                    fs.Write(data, 0, data.Length);
                    Console.WriteLine(path);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose();
                }
            }
        }

        private static void RemoveBomFromDirectory(string path)
        {
            foreach (var d in Directory.GetDirectories(path))
            {
                RemoveBomFromDirectory(d);
            }
            foreach (var f in Directory.GetFiles(path))
            {
                RemoveBomFromFile(f);
            }
        }

        private static void Main(string[] args)
        {
            RemoveBomFromDirectory(@"D:\EFanZh\Development\Git");
        }
    }
}
