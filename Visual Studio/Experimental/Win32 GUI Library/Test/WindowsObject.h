#pragma once

#include "Ref.h"

template <class T, class THandle>
class WindowsObject
{
    THandle handle;

protected:
    void SetHandle(THandle handle)
    {
        this->handle = handle;
    }

public:
    typedef THandle HandleType;

    WindowsObject() = default;

    WindowsObject(THandle handle) : handle(handle)
    {
    }

    Ref<T> GetRef() const
    {
        return handle;
    }
};
