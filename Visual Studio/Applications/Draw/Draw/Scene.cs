using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw
{
    internal class Scene
    {
        private SortedSet<Geometry> shapes = new SortedSet<Geometry>();

        public SortedSet<Geometry> Shapes
        {
            get
            {
                return this.shapes;
            }
        }

        public Scene()
        {
        }

        public Size Size
        {
            get;
            set;
        }

        public void Render(Graphics graphics)
        {
            foreach (var shape in shapes)
            {
                shape.Draw(graphics);
            }
        }
    }
}
