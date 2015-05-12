using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace SortingVisualization
{
    internal class Scene<T> : DispatcherObject where T : IComparable<T>
    {
        public event EventHandler<AssignEventArgs<T>> Assign;

        public event EventHandler<CompareEventArgs<T>> Compare;

        public event EventHandler<SwapEventArgs<T>> Swap;

        public event EventHandler<CreateItemEventArgs<T>> CreateItem;

        public event EventHandler Reset;

        public Scene()
        {
        }

        public SceneItem<T>[] Data
        {
            get;
            private set;
        }

        public SceneItem<T> DoCreateItem()
        {
            var item = new SceneItem<T>();

            if (CreateItem != null)
            {
                this.Dispatcher.Invoke(CreateItem, new CreateItemEventArgs<T>(item));
            }

            return item;
        }

        public void DoAssign(SceneItem<T> lhs, SceneItem<T> rhs)
        {
            lhs.Value = rhs.Value;

            if (Assign != null)
            {
                this.Dispatcher.Invoke(Assign, new AssignEventArgs<T>(lhs, rhs));
            }
        }

        public int DoCompare(SceneItem<T> lhs, SceneItem<T> rhs)
        {
            if (Compare != null)
            {
                this.Dispatcher.Invoke(Compare, new CompareEventArgs<T>(lhs, rhs));
            }

            return lhs.Value.CompareTo(rhs.Value);
        }

        public void DoReset(IEnumerable<SceneItem<T>> data)
        {
            Data = data.ToArray();

            if (Reset != null)
            {
                this.Dispatcher.Invoke(Reset, EventArgs.Empty);
            }
        }
    }
}
