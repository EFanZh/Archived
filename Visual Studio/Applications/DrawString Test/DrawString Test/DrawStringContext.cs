using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawStringTest
{
    internal class DrawStringContext
    {
        private RectangleF layoutRectangle;

        public DrawStringContext()
        {
            StringFormat = new StringFormat();
        }

        public string Text
        {
            get;
            set;
        }

        public Font Font
        {
            get;
            set;
        }

        public Color Brush
        {
            get;
            set;
        }

        public float X
        {
            get
            {
                return LayoutRectangle.X;
            }
            set
            {
                layoutRectangle.X = value;
            }
        }

        public float Y
        {
            get
            {
                return LayoutRectangle.Y;
            }
            set
            {
                layoutRectangle.Y = value;
            }
        }

        public float Width
        {
            get
            {
                return LayoutRectangle.Width;
            }
            set
            {
                layoutRectangle.Width = value;
            }
        }

        public float Height
        {
            get
            {
                return LayoutRectangle.Height;
            }
            set
            {
                layoutRectangle.Height = value;
            }
        }

        public RectangleF LayoutRectangle
        {
            get
            {
                return layoutRectangle;
            }
        }

        public StringAlignment Alignment
        {
            get
            {
                return StringFormat.Alignment;
            }
            set
            {
                StringFormat.Alignment = value;
            }
        }

        public StringFormatFlags FormatFlags
        {
            get
            {
                return StringFormat.FormatFlags;
            }
            set
            {
                StringFormat.FormatFlags = value;
            }
        }

        public HotkeyPrefix HotkeyPrefix
        {
            get
            {
                return StringFormat.HotkeyPrefix;
            }
            set
            {
                StringFormat.HotkeyPrefix = value;
            }
        }

        public StringAlignment LineAlignment
        {
            get
            {
                return StringFormat.LineAlignment;
            }
            set
            {
                StringFormat.LineAlignment = value;
            }
        }

        public StringTrimming Trimming
        {
            get
            {
                return StringFormat.Trimming;
            }
            set
            {
                StringFormat.Trimming = value;
            }
        }

        public StringFormat StringFormat
        {
            get;
            private set;
        }
    }
}
