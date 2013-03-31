using System;
using System.Drawing;

namespace Draw
{
    internal abstract class Geometry : IComparable<Geometry>
    {
        public Pen Stroke
        {
            get;
            set;
        }

        public int ZOrder
        {
            get;
            set;
        }

        public bool IsReal
        {
            get;
            set;
        }

        public abstract void Draw(Graphics graphics);

        public abstract float Distance(Point point);

        #region IComparable<Shape> Members

        public int CompareTo(Geometry other)
        {
            return ZOrder - other.ZOrder;
        }

        #endregion IComparable<Shape> Members
    }
}
