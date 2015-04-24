#pragma once

#include "Instance.h"

class Application
{
    static Application current;

public:
    Ref<Instance> GetInstance()
    {
        return reinterpret_cast<HINSTANCE>(&__ImageBase);
    }

    int Run()
    {
        MSG msg;

        while (::GetMessage(&msg, NULL, 0, 0))
        {
            ::TranslateMessage(&msg);
            ::DispatchMessage(&msg);
        }

        return msg.wParam;
    }

    static Application &GetCurrent()
    {
        return current;
    }
};

HEADER_STATIC_MEMBER Application Application::current;
