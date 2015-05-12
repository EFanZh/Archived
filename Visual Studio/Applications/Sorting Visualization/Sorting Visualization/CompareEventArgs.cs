using System;

namespace SortingVisualization
{
    internal class CompareEventArgs<T> : EventArgs where T : IComparable<T>
    {
        public CompareEventArgs(SceneItem<T> lhs, SceneItem<T> rhs)
        {
            LeftHandSide = lhs;
            RightHandSide = rhs;
        }

        public SceneItem<T> LeftHandSide
        {
            get;
            private set;
        }

        public SceneItem<T> RightHandSide
        {
            get;
            private set;
        }
    }
}
