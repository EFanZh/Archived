using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace TheMatrix
{
    internal class Scene
    {
        private Size size, logical_size;

        private List<Strip> matrix = new List<Strip>();
        private float[] line_position_cache, column_position_cache;
        private StringFormat string_format = new StringFormat();
        private int update_time = 2000, frame_count = 0;
        private Stopwatch stopwatch = new Stopwatch();

        public Scene(Size logical_size, Size size, Font font, Color background_color, int generate_count, double generate_probability, int min_length, int max_lenghth, double min_speed, double max_speed, Color special_color, Color color_from, Color color_to, double mutation_probability, int mutation_frames)
        {
            LogicalSize = logical_size;
            Size = size;
            Font = font;
            BackgroundColor = background_color;
            GenerateCount = generate_count;
            GenerateProbability = generate_probability;
            MinLength = min_length;
            MaxLength = max_lenghth;
            MinSpeed = min_speed;
            MaxSpeed = max_speed;
            SpecialColor = special_color;
            ColorFrom = color_from;
            ColorTo = color_to;
            MutationProbability = mutation_probability;
            MutationFrames = mutation_frames;

            DisplayInfo = false;
            Rendering = true;

            string_format.Alignment = StringAlignment.Center;
            string_format.LineAlignment = StringAlignment.Center;

            stopwatch.Start();
        }

        public bool DisplayInfo
        {
            get;
            set;
        }

        public bool Rendering
        {
            get;
            set;
        }

        public Size LogicalSize
        {
            get
            {
                return logical_size;
            }
            set
            {
                logical_size = value;
                UpdateSizeCache();
            }
        }

        public Size Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                UpdateSizeCache();
            }
        }

        public Font Font
        {
            get;
            set;
        }

        public Color BackgroundColor
        {
            get;
            set;
        }

        public int GenerateCount
        {
            get;
            set;
        }

        public double GenerateProbability
        {
            get;
            set;
        }

        public int MinLength
        {
            get;
            set;
        }

        public int MaxLength
        {
            get;
            set;
        }

        public double MinSpeed
        {
            get;
            set;
        }

        public double MaxSpeed
        {
            get;
            set;
        }

        public Color SpecialColor
        {
            get;
            set;
        }

        public Color ColorFrom
        {
            get;
            set;
        }

        public Color ColorTo
        {
            get;
            set;
        }

        public double MutationProbability
        {
            get;
            set;
        }

        public int MutationFrames
        {
            get;
            set;
        }

        public void Render(Graphics graphics)
        {
            graphics.Clear(BackgroundColor);
            if (Rendering)
            {
                foreach (var strip in matrix)
                {
                    DrawStrip(graphics, strip);
                }
            }
            if (DisplayInfo)
            {
                graphics.DrawString(string.Format("Cols:\t{0}\r\nLines:\t{1}\r\nFPS:\t{2:0.00}\r\nCount:\t{3}", LogicalSize.Width, LogicalSize.Height, frame_count * 1000.0 / stopwatch.ElapsedMilliseconds, matrix.Count), SystemFonts.MessageBoxFont, SystemBrushes.HighlightText, 11.0f, 11.0f);
            }
            if (stopwatch.ElapsedMilliseconds > update_time)
            {
                frame_count = 0;
                stopwatch.Restart();
            }
            frame_count++;
        }

        public void NextFrame()
        {
            foreach (var strip in matrix)
            {
                strip.NextFrame();
            }
            if (matrix.RemoveAll(strip => { return strip.IntPosition - strip.Characters.Count >= LogicalSize.Height; }) > 0)
            {
                GC.Collect();
            }
            for (int i = 0; i < GenerateCount; i++)
            {
                if (Utilities.Probability(GenerateProbability))
                {
                    matrix.Add(new Strip(Utilities.Random.Next(MinLength, MaxLength), Utilities.Random.Next(0, LogicalSize.Width), 0.0, Utilities.GetRandomDouble(MinSpeed, MaxSpeed), SpecialColor, ColorFrom, ColorTo, MutationProbability, MutationFrames));
                }
            }
        }

        private void DrawStrip(Graphics graphics, Strip strip)
        {
            if (strip.Column < LogicalSize.Width)
            {
                for (int i = 0; i < strip.Characters.Count; i++)
                {
                    int p = strip.IntPosition - i;
                    if (p >= 0 && p < LogicalSize.Height)
                    {
                        graphics.DrawString(strip.Characters[i].ToString(), Font, new SolidBrush(strip.GetColorAt(i)), column_position_cache[strip.Column], line_position_cache[p], string_format);
                    }
                }
            }
        }

        private void UpdateSizeCache()
        {
            column_position_cache = new float[logical_size.Width];
            for (int i = 0; i < logical_size.Width; i++)
            {
                column_position_cache[i] = size.Width * (i + 0.5f) / logical_size.Width;
            }
            line_position_cache = new float[logical_size.Height];
            for (int i = 0; i < logical_size.Height; i++)
            {
                line_position_cache[i] = size.Height * (i + 0.5f) / logical_size.Height;
            }
        }
    }
}
