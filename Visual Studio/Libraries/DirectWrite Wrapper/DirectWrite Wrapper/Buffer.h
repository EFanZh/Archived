#pragma once

namespace DirectWriteWrapper
{
    template <class T, class TSize = size_t>
    class Buffer
    {
        T *data;
        TSize size;

    public:
        Buffer(TSize count) : data(new T[count]), size(count)
        {
        }

        ~Buffer()
        {
            delete[] data;
        }

        T *GetBuffer()
        {
            return data;
        }

        TSize GetSize()
        {
            return size;
        }
    };
}
