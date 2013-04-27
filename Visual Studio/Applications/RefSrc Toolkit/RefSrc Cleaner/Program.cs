using System;
using System.IO;
using System.Linq;

namespace RefSrcCleaner
{
    internal class Program
    {
        private static void CleanFile(string path)
        {
            var data = File.ReadAllBytes(path);
            int length = data.Length / 2;
            if (length * 2 == data.Length && data.Take(length).SequenceEqual(data.Skip(length)))
            {
                File.WriteAllBytes(path, data.Take(length).ToArray());
            }
            else
            {
                Console.WriteLine("!!!: {0}", path);
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
            Clean(@"E:\Source");
            Console.WriteLine("End");
        }
    }
}
