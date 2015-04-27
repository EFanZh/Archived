using System.Collections.Generic;
using System.Drawing;

namespace TheMatrix
{
    internal class Strip
    {
        private int last_mutation_frame = 0;

        public Strip(int length, int column, double position, double speed, Color special_color, Color color_from, Color color_to, double mutation_probability, int mutation_frame)
        {
            Characters = new List<char>();
            for (int i = 0; i < length; i++)
            {
                Characters.Add(Utilities.GetRandomCharacter());
            }
            Column = column;
            Position = position;
            Speed = speed;
            SpecialColor = special_color;
            ColorFrom = color_from;
            ColorTo = color_to;
            MutationProbability = mutation_probability;
            MutationFrame = mutation_frame;
        }

        public List<char> Characters
        {
            get;
            private set;
        }

        public int Column
        {
            get;
            set;
        }

        public double Position
        {
            set;
            get;
        }

        public int IntPosition
        {
            get
            {
                return PositionToInt(Position);
            }
        }

        public double Speed
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

        private int MutationFrame
        {
            get;
            set;
        }

        public Color GetColorAt(int i)
        {
            switch (i)
            {
                case 0:
                    return SpecialColor;
                case 1:
                    return ColorFrom;
                default:
                    return MixColor(ColorFrom, ColorTo, (i - 1.0) / (Characters.Count - 2.0));
            }
        }

        public void NextFrame()
        {
            var new_position = Position + Speed;
            if (PositionToInt(new_position) > IntPosition)
            {
                Characters.RemoveAt(Characters.Count - 1);
                Characters.Insert(0, Utilities.GetRandomCharacter());
            }
            Position = new_position;
            if (last_mutation_frame == MutationFrame)
            {
                for (int i = 0; i < Characters.Count; i++)
                {
                    if (Utilities.Probability(MutationProbability))
                    {
                        Characters[i] = Utilities.GetRandomCharacter();
                    }
                }
                last_mutation_frame = 0;
            }
            else
            {
                last_mutation_frame++;
            }
        }

        private static int PositionToInt(double position)
        {
            return (int)position;
        }

        private static Color MixColor(Color from, Color to, double percent)
        {
            return Color.FromArgb(Utilities.RoundToInt(from.A + percent * (to.A - from.A)), Utilities.RoundToInt(from.R + percent * (to.R - from.R)), Utilities.RoundToInt(from.G + percent * (to.G - from.G)), Utilities.RoundToInt(from.B + percent * (to.B - from.B)));
        }
    }
}
