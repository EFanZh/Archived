using System;
using System.IO;
using System.Linq;

namespace RefSrcCleaner
{
    internal class Program
    {
        private static int Verify(byte[] data, int length)
        {
            for (int i = 0, j = length; i < length; i++, j++)
            {
                if (data[i] != data[j])
                {
                    return i;
                }
            }
            return length;
        }

        private static void CleanFile(string path)
        {
            var data = File.ReadAllBytes(path);
            int length = data.Length / 2;
            int value = Verify(data, length);
            if (value == length)
            {
                FileStream fs = File.Open(path, FileMode.Create);
                fs.Write(data, 0, length);
                fs.Close();
            }
            else
            {
                if (path.EndsWith(".pdb") && value > 24)
                {
                    FileStream fs = File.Open(path, FileMode.Create);
                    fs.Write(data, 0, length);
                    fs.Close();
                    Console.WriteLine("PDB: {0}", path);
                }
                else
                {
                    Console.WriteLine("!!!: {0}", path);
                }
            }
        }

        private static void Clean(string path)
        {
            foreach (var file in Directory.GetFiles(path))
            {
                CleanFile(file);
            }
            foreach (var dir in Directory.GetDirectories(path))
            {
                Clean(dir);
            }
        }

        private static void Main(string[] args)
        {
            Clean(@"D:\EFanZh\Development\Source\RefSrc");
            Console.WriteLine("End");
        }
    }
}
