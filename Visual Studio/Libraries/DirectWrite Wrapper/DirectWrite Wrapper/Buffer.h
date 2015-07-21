#pragma once

namespace DirectWriteWrapper
{
    template <class T, class TSize = size_t>
    class Buffer
    {
        T *data = nullptr;
        TSize size = 0;

    public:
        Buffer(TSize count)
        {
            data = new T[count];
            memset(data, sizeof(T) * count, 0);
            this->size = size;
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
