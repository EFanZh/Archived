using System;

namespace SortingVisualization
{
    internal class CreateItemEventArgs<T> : EventArgs where T : IComparable<T>
    {
        public CreateItemEventArgs(SceneItem<T> item)
        {
            Item = item;
        }

        public SceneItem<T> Item
        {
            get;
            private set;
        }
    }
}
