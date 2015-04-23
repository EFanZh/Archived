#pragma once

EXTERN_C IMAGE_DOS_HEADER __ImageBase;

class Application
{
    static Application current;

public:
    HINSTANCE GetInstanceHandle()
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

__declspec(selectany) Application Application::current;
