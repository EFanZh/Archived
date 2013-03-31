using System;
using System.Drawing;

namespace ClockDotNet
{
    internal abstract class Scene : IDisposable
    {
        private Size size;

        protected event EventHandler SizeChanged;

        public Size Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                if (SizeChanged != null)
                {
                    SizeChanged(this, new EventArgs());
                }
            }
        }

        protected abstract void DoDrawBackground(Graphics graphics);

        protected abstract void DoDrawForeground(Graphics graphics);

        public void DrawBackground(Bitmap bitmap)
        {
            DoDrawBackground(Graphics.FromImage(bitmap));
        }

        public void DrawForeground(Bitmap bitmap)
        {
            DoDrawForeground(Graphics.FromImage(bitmap));
        }

        #region IDisposable Members

        public abstract void Dispose();

        #endregion IDisposable Members
    }
}
