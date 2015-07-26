using System;

namespace CheckFileList
{
    internal abstract class Segment : IEquatable<Segment>, IComparable<Segment>
    {
        static Segment()
        {
            Current = new CurrentSegment();
            Parent = new ParentSegment();
        }

        public abstract bool Equals(Segment other);

        public abstract int CompareTo(Segment other);

        public static Segment Parse(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return new EmptySegment();
            }

            switch (s)
            {
                case ".":
                    return new CurrentSegment();

                case "..":
                    return new ParentSegment();

                default:
                    return new NamedSegment(s);
            }
        }

        public static CurrentSegment Current
        {
            get;
            private set;
        }

        public static ParentSegment Parent
        {
            get;
            private set;
        }
    }
}
