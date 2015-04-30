namespace SmallLibrary
{
    internal interface IEnumerator<out T>
    {
        T Current
        {
            get;
        }

        bool MoveNext();

        void Reset();
    }

    internal interface IEnumerable<out T>
    {
        IEnumerator<T> GetEnumerator();
    }

    internal interface ICollectionBase<out T> : IEnumerable<T>
    {
        int Count
        {
            get;
        }
    }

    internal interface IReadableCollection<T> : ICollectionBase<T>
    {
        bool Contains(T item);
    }

    internal interface IWritableCollection<T> : ICollectionBase<T>
    {
        void Add(T item);

        void Remove(T item);

        void Clear();
    }

    internal interface ICollection<T> : IReadableCollection<T>, IWritableCollection<T>
    {
    }

    internal interface IListBase<out T> : ICollectionBase<T>
    {
    }

    internal interface IReadableList<T> : IReadableCollection<T>
    {
        T this[int index]
        {
            get;
        }
    }

    internal interface IWritableList<T> : IWritableCollection<T>
    {
        T this[int index]
        {
            set;
        }
    }

    internal interface IList<T> : IReadableList<T>, IWritableList<T>
    {
    }

    public class MainClass
    {
    }
}
