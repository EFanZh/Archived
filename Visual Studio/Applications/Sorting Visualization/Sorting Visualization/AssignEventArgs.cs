using System;

namespace SortingVisualization
{
    internal class AssignEventArgs<T> : EventArgs where T : IComparable<T>
    {
        public AssignEventArgs(SceneItem<T> lhs, SceneItem<T> rhs)
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
