using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualization
{
    internal class Array
    {
        private SceneItem[] data;
        private Scene scene;

        private void Reset(IReadOnlyList<int> data)
        {
            this.data = data.Select(d => new SceneItem(scene, d)).ToArray();
        }

        public SceneItem this[int index]
        {
            get
            {
                return data[index];
            }
            set
            {
                data[index].Value = value.Value;
                scene.ReportAssign(data[index], value);
            }
        }

        public int Length
        {
            get
            {
                return data.Length;
            }
        }
    }
}
