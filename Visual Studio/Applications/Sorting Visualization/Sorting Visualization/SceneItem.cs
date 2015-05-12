using System;

namespace SortingVisualization
{
    internal class SceneItem<T> where T : IComparable<T>
    {
        public T Value
        {
            get;
            set;
        }
    }
}
