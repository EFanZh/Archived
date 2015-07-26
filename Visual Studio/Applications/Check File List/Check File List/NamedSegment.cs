using System;

namespace CheckFileList
{
    internal class NamedSegment : Segment
    {
        public NamedSegment(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
        }

        public static StringComparison NameComparison
        {
            get;
            set;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(Segment other)
        {
            NamedSegment otherNamedSegment = other as NamedSegment;

            return otherNamedSegment != null && string.Equals(Name, otherNamedSegment.Name, NameComparison);
        }

        public override int CompareTo(Segment other)
        {
            NamedSegment otherNamedSegment = other as NamedSegment;

            if (otherNamedSegment == null)
            {
                return 1;
            }
            else
            {
                return string.Compare(this.Name, otherNamedSegment.Name, NameComparison);
            }
        }
    }
}
