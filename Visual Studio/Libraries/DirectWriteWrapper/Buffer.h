#pragma once

namespace DirectWriteWrapper
{
    template <class T>
    class Buffer
    {
        T *data = nullptr;
        size_t size = 0;

    public:
        Buffer(size_t count)
        {
            data = new T[count];
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

        size_t GetSize()
        {
            return size;
        }
    };
}
