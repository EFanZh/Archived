using System.Runtime.InteropServices;

namespace ParserCombinators
{
    internal interface IStream
    {
        long Position
        {
            get;
        }
    }

    internal interface ISeekableStream : IStream
    {
        void Seek(long position);
    }

    internal interface IReadableStream<out T> : IStream
    {
        T Read();
    }

    internal interface IWriteableOnlyStream<in T> : IStream
    {
        void Write(T value);
    }

    internal interface IInputStream<out T> : IReadableStream<T>, ISeekableStream
    {
    }
}
