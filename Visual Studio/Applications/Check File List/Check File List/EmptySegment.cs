namespace CheckFileList
{
    internal class EmptySegment : Segment
    {
        public override string ToString()
        {
            return string.Empty;
        }

        public override bool Equals(Segment other)
        {
            return other is EmptySegment;
        }

        public override int CompareTo(Segment other)
        {
            if (other is EmptySegment)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
