using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ImgProc.MyControls
{
    internal class MySplitter : Control
    {
        const int defaultMinSize = 100;
        const int defaultMinExtra = 100;
        const bool defaultTabStop = false;
        int minSize;
        int minExtra;
        Point pt0;
        Size sz0, szExtra;

        public MySplitter()
        {
            this.Dock = DockStyle.Left;
            this.Size = new Size(4, 4);
            MinSize = defaultMinSize;
            MinExtra = defaultMinExtra;
            TabStop = defaultTabStop;
        }

        private Control FindDockedToControl()
        {
            var ctrl = this.Parent.Controls.Cast<Control>().GetEnumerator();

            while (ctrl.MoveNext())
            {
                if (ctrl.Current == this)
                {
                    while (ctrl.MoveNext())
                    {
                        if (ctrl.Current.Dock == this.Dock)
                        {
                            return ctrl.Current;
                        }
                    }
                    break;
                }
            }
            return null;
        }

        private Size CalcExtraSize()
        {
            Size s = this.Parent.ClientSize;

            var ctrl = this.Parent.Controls.Cast<Control>().GetEnumerator();

            while (ctrl.MoveNext())
            {
                switch (ctrl.Current.Dock)
                {
                    case DockStyle.Left:
                    case DockStyle.Right:
                        s.Width -= ctrl.Current.Width;
                        break;
                    case DockStyle.Top:
                    case DockStyle.Bottom:
                        s.Height -= ctrl.Current.Height;
                        break;
                }
            }
            return s;
        }

        protected override void OnDockChanged(EventArgs e)
        {
            switch (this.Dock)
            {
                case DockStyle.Left:
                case DockStyle.Right:
                    this.Cursor = Cursors.VSplit;
                    break;
                case DockStyle.Top:
                case DockStyle.Bottom:
                    this.Cursor = Cursors.HSplit;
                    break;
                default:
                    return;
            }

            base.OnDockChanged(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Control dockOn = FindDockedToControl();
            if (dockOn != null)
            {
                pt0 = this.PointToScreen(e.Location);
                sz0 = FindDockedToControl().Size;
            }
            szExtra = CalcExtraSize();
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Control dockOn = FindDockedToControl();
                if (dockOn != null)
                {
                    Point pt1 = this.PointToScreen(e.Location);
                    int xOffset = pt1.X - pt0.X;
                    int yOffset = pt1.Y - pt0.Y;
                    int targetSize;
                    switch (this.Dock)
                    {
                        case DockStyle.Left:
                            targetSize = sz0.Width + xOffset;
                            if (szExtra.Width - xOffset < MinExtra)
                            {
                                targetSize = sz0.Width + szExtra.Width - MinExtra;
                            }
                            if (targetSize < MinSize)
                            {
                                targetSize = MinSize;
                            }
                            dockOn.Width = targetSize;
                            break;
                        case DockStyle.Right:
                            targetSize = sz0.Width - xOffset;
                            if (szExtra.Width + xOffset < MinExtra)
                            {
                                targetSize = sz0.Width + szExtra.Width - MinExtra;
                            }
                            if (targetSize < MinSize)
                            {
                                targetSize = MinSize;
                            }
                            dockOn.Width = targetSize;
                            break;
                        case DockStyle.Top:
                            targetSize = sz0.Height + yOffset;
                            if (szExtra.Height - yOffset < MinExtra)
                            {
                                targetSize = sz0.Height + szExtra.Height - MinExtra;
                            }
                            if (targetSize < MinSize)
                            {
                                targetSize = MinSize;
                            }
                            dockOn.Height = targetSize;
                            break;
                        case DockStyle.Bottom:
                            targetSize = sz0.Height - yOffset;
                            if (szExtra.Height + yOffset < MinExtra)
                            {
                                targetSize = sz0.Height + szExtra.Height - MinExtra;
                            }
                            if (targetSize < MinSize)
                            {
                                targetSize = MinSize;
                            }
                            dockOn.Height = targetSize;
                            break;
                    }
                }
            }

            base.OnMouseMove(e);
        }

        [DefaultValue(defaultMinSize)]
        public int MinSize
        {
            get
            {
                return minSize;
            }
            set
            {
                if (value < 0)
                {
                    minSize = 0;
                }
                else
                {
                    minSize = value;
                }
            }
        }

        [DefaultValue(defaultMinExtra)]
        public int MinExtra
        {
            get
            {
                return minExtra;
            }
            set
            {
                if (value < 0)
                {
                    minExtra = 0;
                }
                else
                {
                    minExtra = value;
                }
            }
        }

        #region TabStop property

        [DefaultValue(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        private new bool TabStop
        {
            get
            {
                return base.TabStop;
            }
            set
            {
                base.TabStop = value;
            }
        }

        #endregion TabStop property
    }
}
