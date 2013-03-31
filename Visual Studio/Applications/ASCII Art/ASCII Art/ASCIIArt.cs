using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ASCIIArt
{
    internal static class ASCIIArt
    {
        private static SolidBrush black_brush = new SolidBrush(Color.Black);

        static ASCIIArt()
        {
        }

        public static string Generate(Graphics graphics, Bitmap bitmap, Font font, HashSet<char> char_set)
        {
            Size char_size = GetCharSize(font);
            var dict_char = new Dictionary<char, int>();
            int min_char = int.MaxValue, max_char = int.MinValue;
            using (Bitmap char_bitmap = new Bitmap(char_size.Width, char_size.Height))
            {
                using (Graphics char_graphics = Graphics.FromImage(char_bitmap))
                {
                    foreach (var ch in char_set)
                    {
                        char_graphics.Clear(Color.White);
                        char_graphics.DrawString(ch.ToString(), font, black_brush, 0.0f, 0.0f);
                        int sum = 0;
                        for (int i = 0; i < char_bitmap.Width; i++)
                        {
                            for (int j = 0; j < char_bitmap.Height; j++)
                            {
                                Color color = char_bitmap.GetPixel(i, j);
                                sum += color.R + color.G + color.B;
                            }
                        }
                        dict_char[ch] = sum;
                        if (sum < min_char)
                        {
                            min_char = sum;
                        }
                        if (sum > max_char)
                        {
                            max_char = sum;
                        }
                    }
                }
            }
            foreach (var key in char_set)
            {
                dict_char[key] -= min_char;
            }
            max_char -= min_char;
            var dict_char_ordered = dict_char.OrderBy(kvp => kvp.Value);

            var bitmap_matrix = new int[bitmap.Width, bitmap.Height];
            int min_bitmap = int.MaxValue, max_bitmap = int.MinValue;
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color color = bitmap.GetPixel(i, j);
                    int sum = color.A * (color.R + color.G + color.B) + (255 - color.A) * 765;
                    bitmap_matrix[i, j] = sum;
                    if (sum < min_bitmap)
                    {
                        min_bitmap = sum;
                    }
                    if (sum > max_bitmap)
                    {
                        max_bitmap = sum;
                    }
                }
            }
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    bitmap_matrix[i, j] -= min_bitmap;
                }
            }
            max_bitmap -= min_bitmap;

            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < bitmap.Height; j++)
            {
                for (int i = 0; i < bitmap.Width; i++)
                {
                    double position = (double)((long)bitmap_matrix[i, j] * max_char) / max_bitmap;
                    var kvp1 = dict_char_ordered.Last(kvp => kvp.Value <= position);
                    var kvp2 = dict_char_ordered.First(kvp => kvp.Value >= position);
                    if (kvp1.Value == kvp2.Value || random.NextDouble() >= (position - kvp1.Value) / (kvp2.Value - kvp1.Value))
                    {
                        sb.Append(kvp1.Key);
                    }
                    else
                    {
                        sb.Append(kvp2.Key);
                    }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private static Size GetCharSize(Font font)
        {
            const string measure_string = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            IntPtr hdc = NativeMethods.CreateCompatibleDC(IntPtr.Zero);
            NativeMethods.SelectObject(hdc, font.ToHfont());
            NativeMethods.TEXTMETRIC tm;
            NativeMethods.GetTextMetrics(hdc, out tm);
            NativeMethods.SIZE size;
            NativeMethods.GetTextExtentPoint32(hdc, measure_string, measure_string.Length, out size);
            NativeMethods.DeleteDC(hdc);
            return new Size((int)Math.Round((double)size.cx / (double)measure_string.Length), tm.tmHeight);
        }
    }
}
