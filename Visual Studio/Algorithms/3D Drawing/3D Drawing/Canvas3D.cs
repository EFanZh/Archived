using System.Drawing;
using System.Windows.Forms;

namespace ThreeDDrawing
{
    internal class Canvas3D : Control
    {
        public Canvas3D()
        {
            this.BackColor = Color.White;
            this.DoubleBuffered = true;
        }
    }
}
