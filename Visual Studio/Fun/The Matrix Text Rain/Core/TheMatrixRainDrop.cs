using System.Collections.Generic;
using System.Linq;

namespace Core
{
    internal class TheMatrixRaindrop
    {
        public TheMatrixRaindrop(double position, double speed, IEnumerable<char> characters)
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
            private set;
        }

        public char[] Characters
        {
            get;
        }

        public int Size => Characters.Length;

        public void Reset(double position, double speed, IEnumerable<char> characters)
        {
            Position = position;
            Speed = speed;

            using (var enumerator = characters.GetEnumerator())
            {
                for (var i = 0; i < Size; i++)
                {
                    enumerator.MoveNext();

                    Characters[i] = enumerator.Current;
                }
            }
        }
    }
}
