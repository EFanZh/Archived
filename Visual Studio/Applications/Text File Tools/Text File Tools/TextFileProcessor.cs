using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TextFileTools
{
    internal class TextFileProcessor
    {
        private static byte[] utf8_bom = { 0xef, 0xbb, 0xbf };
        private static byte[] utf8_blank_line = { (byte)'\r', (byte)'\n' };
        private static int utf8_bom_size = utf8_bom.Length;
        private static int utf8_blank_line_size = utf8_blank_line.Length;

        private static byte[] utf_16_le_bom = { 0xff, 0xfe };
        private static byte[] utf_16_le_blank_line = { (byte)'\r', 0, (byte)'\n', 0 };
        private static int utf_16_le_bom_size = utf_16_le_bom.Length;
        private static int utf_16_le_blank_line_size = utf_16_le_blank_line.Length;

        private static Dictionary<TextFileProcessOptions, Func<byte[], byte[]>> process_option_dict = new Dictionary<TextFileProcessOptions, Func<byte[], byte[]>>()
        {
            { TextFileProcessOptions.RemoveUTF8BOM, RemoveBOM },
            { TextFileProcessOptions.AddBlankLineAtFileBottom, AddBlankLineAtFileBottom },
        };

        public async Task ProcessFileAsync(string[] files, IProgress<int> progress, IEnumerable<TextFileProcessOptions> options)
        {
            int file_count = files.Length;
            var process_list = from option in options select process_option_dict[option];

            for (int i = 0; i < file_count; i++)
            {
                progress.Report(i);

                string file = files[i];

                await Task.Run(() =>
                {
                    try
                    {
                        var data = File.ReadAllBytes(file);
                        bool changed = false;
                        foreach (var process in process_list)
                        {
                            var new_data = process(data);
                            if (new_data != null)
                            {
                                changed = true;
                                data = new_data;
                            }
                        }
                        if (changed)
                        {
                            File.WriteAllBytes(file, data);
                        }
                    }
                    catch (Exception)
                    {
                    }
                });
            }
            progress.Report(file_count);
        }

        private static byte[] RemoveBOM(byte[] file_data)
        {
            if (file_data.Length >= utf8_bom_size && file_data.Take(utf8_bom_size).SequenceEqual(utf8_bom))
            {
                return file_data.Skip(utf8_bom_size).ToArray();
            }
            return null;
        }

        private static byte[] AddBlankLineAtFileBottom(byte[] file_data)
        {
            if (file_data.Length >= utf_16_le_bom_size && file_data.Take(utf_16_le_bom_size).SequenceEqual(utf_16_le_bom))
            {
                if (!file_data.Skip(file_data.Length - utf_16_le_blank_line_size).SequenceEqual(utf_16_le_blank_line))
                {
                    return file_data.Concat(utf_16_le_blank_line).ToArray();
                }
            }
            else if (!file_data.Skip(file_data.Length - utf8_blank_line_size).SequenceEqual(utf8_blank_line))
            {
                return file_data.Concat(utf8_blank_line).ToArray();
            }
            return null;
        }
    }
}
