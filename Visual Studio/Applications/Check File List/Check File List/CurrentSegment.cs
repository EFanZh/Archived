namespace CheckFileList
{
    internal class CurrentSegment : Segment
    {
        public override string ToString()
        {
            return ".";
        }

        public override bool Equals(Segment other)
        {
            return other is CurrentSegment;
        }

        public override int CompareTo(Segment other)
        {
            if (other is EmptySegment)
            {
                return 1;
            }
            else if (other is CurrentSegment)
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
