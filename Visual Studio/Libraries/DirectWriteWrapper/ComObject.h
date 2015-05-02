#pragma once

namespace DirectWriteWrapper
{
    template <class T>
    public ref class ComObject
    {
        T *comObject;

    protected:
        ComObject()
        {
        }

        ComObject(::IUnknown *comObject) : comObject(static_cast<T *>(comObject))
        {
        }

        ~ComObject()
        {
            comObject->Release();
        }

        T *GetComObject()
        {
            return comObject;
        }

        void SetComObject(::IUnknown *value)
        {
            comObject = static_cast<T *>(value);
        }
    };
}
