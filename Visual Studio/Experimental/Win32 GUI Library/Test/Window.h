#pragma once

#include "WindowsObject.h"
#include "ExtendedWindowStyle.h"
#include "WindowStyle.h"
#include "Menu.h"
#include "String.h"
#include "WindowClass.h"
#include "Application.h"
#include "WindowMessage.h"

class Window : public WindowsObject < Window, HWND >
{
    ATL::CStdCallThunk thunk;

    template <class T>
    friend class WindowClass;

protected:
    IntPtr WindowProc(WindowMessage message, UIntPtr wParam, IntPtr lParam)
    {
        return ::DefWindowProc(this->GetRef(), static_cast<UINT>(message), wParam, lParam);
    }

public:
    Window(ExtendedWindowStyle extendedStyle,
           unsigned int classId,
           Ref<String> windowName,
           WindowStyle style,
           int x,
           int y,
           int width,
           int height,
           Ref<Window> parent,
           Ref<Menu> menu,
           void *param)
    {
        WindowClassBase::AddCreateInfo(this);

        ::CreateWindowEx(static_cast<DWORD>(extendedStyle),
                         reinterpret_cast<LPCTSTR>(classId),
                         windowName,
                         static_cast<DWORD>(style),
                         x,
                         y,
                         width,
                         height,
                         parent,
                         menu,
                         Application::GetCurrent().GetInstanceHandle(),
                         param);
    }

    void Show()
    {
        ::ShowWindow(this->GetRef(), SW_NORMAL);
    }
};
