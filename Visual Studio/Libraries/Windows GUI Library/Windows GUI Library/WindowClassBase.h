#pragma once

#include "SpinLock.h"

class Window;

class WindowClassBase
{
    static std::map<std::thread::id, Window *> tidToWindowMap;
    static SpinLock tidToWindowMapLock;

    friend class Window;

    static void AddCreateInfo(Window *window)
    {
        tidToWindowMapLock.Lock();

        tidToWindowMap.emplace(std::this_thread::get_id(), window);

        tidToWindowMapLock.Unlock();
    }

protected:
    static Window *ExtractCreateInfo()
    {
        tidToWindowMapLock.Lock();

        auto it = tidToWindowMap.find(std::this_thread::get_id());
        Window *window = it->second;

        tidToWindowMap.erase(it);

        tidToWindowMapLock.Unlock();

        return window;
    }
};

HEADER_STATIC_MEMBER std::map<std::thread::id, Window *> WindowClassBase::tidToWindowMap;
HEADER_STATIC_MEMBER SpinLock WindowClassBase::tidToWindowMapLock;
