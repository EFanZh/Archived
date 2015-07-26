namespace CheckFileList
{
    internal class ParentSegment : Segment
    {
        public override string ToString()
        {
            return "..";
        }

        public override bool Equals(Segment other)
        {
            return other is ParentSegment;
        }

        public override int CompareTo(Segment other)
        {
            if (other is NamedSegment)
            {
                return -1;
            }
            else if (other is ParentSegment)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
