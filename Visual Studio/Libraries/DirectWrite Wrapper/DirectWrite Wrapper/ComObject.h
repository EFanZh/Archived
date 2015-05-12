#pragma once

namespace DirectWriteWrapper
{
    template <class T>
    public ref class ComObject
    {
        T *comObject;
        bool isDisposed = false;

    protected:
        ComObject()
        {
        }

        ComObject(::IUnknown *comObject) : comObject(static_cast<T *>(comObject))
        {
        }

        ~ComObject()
        {
            if (isDisposed)
            {
                return;
            }

            // Since there is no managed data.
            // Call finalizer.
            this->!ComObject();

            isDisposed = true;

            // GC::SuppressFinalize() is automatically inserted.
        }

        !ComObject()
        {
            // Dispose unmanaged data.
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
