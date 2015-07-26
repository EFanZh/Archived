using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckFileList
{
    internal class Path : IEquatable<Path>, IComparable<Path>
    {
        private readonly List<Segment> segments;
        private bool normalized;

        private static readonly char[] Separators = { '/', '\\' };

        public Path()
        {
            segments = new List<Segment>();
            normalized = false;
        }

        public Path(Path other)
        {
            segments = new List<Segment>(other.segments);
            normalized = other.normalized;
        }

        public IReadOnlyList<Segment> Segments
        {
            get;
            set;
        }

        public void Normalize()
        {
            if (normalized)
            {
                return;
            }

            for (int i = 0; i < segments.Count;)
            {
                Segment current = segments[i];

                if (current is NamedSegment)
                {
                    ++i;
                }
                else
                {
                    segments.RemoveAt(i);

                    if (i > 0 && current is ParentSegment)
                    {
                        segments.RemoveAt(i - 1);
                        --i;
                    }
                }
            }

            normalized = true;
        }

        public void MakeRelativeTo(Path basePath)
        {
            if (!basePath.normalized)
            {
                basePath = new Path(basePath);
                basePath.Normalize();
            }

            this.Normalize();

            int length = Math.Min(this.segments.Count, basePath.segments.Count);
            int commonPrefixLength = 0;

            while (commonPrefixLength < length && this.segments[commonPrefixLength].Equals(basePath.segments[commonPrefixLength]))
            {
                commonPrefixLength++;
            }

            int parentCount = basePath.segments.Count - commonPrefixLength;
            int finalLength = parentCount + (this.segments.Count - commonPrefixLength);
            int i;

            if (this.segments.Count < finalLength)
            {
                i = finalLength - this.segments.Count;
                this.segments.InsertRange(0, Enumerable.Repeat(Segment.Parent, i));
            }
            else
            {
                i = 0;
                this.segments.RemoveRange(0, this.segments.Count - finalLength);
            }

            for (int j = i; j < parentCount; j++)
            {
                this.segments[j] = Segment.Parent;
            }
        }

        public override int GetHashCode()
        {
            return segments.Aggregate(0, (i, segment) => i ^ segment.GetHashCode());
        }

        public override string ToString()
        {
            return string.Join("/", segments);
        }

        public bool Equals(Path other)
        {
            return segments.SequenceEqual(other.segments);
        }

        public int CompareTo(Path other)
        {
            int length = Math.Min(segments.Count, other.segments.Count);

            for (int i = 0; i < length; i++)
            {
                int result = segments[i].CompareTo(other.segments[i]);

                if (result != 0)
                {
                    return result;
                }
            }

            return segments.Count - other.segments.Count;
        }

        public static Path Parse(string s)
        {
            Path path = new Path();

            path.segments.AddRange(s.Split(Separators).Select(Segment.Parse));

            return path;
        }

        public static Path ParseNormalized(string s)
        {
            Path path = Parse(s);

            path.Normalize();

            return path;
        }
    }
}
