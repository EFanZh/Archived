using System.Collections.Generic;
using System.Linq;

namespace Core
{
    internal class TheMatrixRainDrop
    {
        public TheMatrixRainDrop(double position, double speed, IEnumerable<char> characters)
        {
            Position = position;
            Speed = speed;
            Characters = characters.ToArray();
        }

        public double Position
        {
            get;
            set;
        }

        public double Speed
        {
            get;
        }

        public char[] Characters
        {
            get;
        }

        public int Size => Characters.Length;
    }
}
