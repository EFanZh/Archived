#pragma once

template<class T>
class Ref
{
    typename T::HandleType handle;

public:
    Ref(typename T::HandleType handle) : handle(handle)
    {
    }

    operator typename T::HandleType() const
    {
        return handle;
    }
};
