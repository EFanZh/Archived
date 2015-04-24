#pragma once

#include "WindowsObject.h"
#include "WindowClass.h"
#include "WindowsString.h"
#include "WindowStyle.h"
#include "ExtendedWindowStyle.h"
#include "Menu.h"
#include "WindowMessage.h"
#include "Application.h"
#include "ShowWindowCommand.h"

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
    Window(unsigned int classId,
           Ref<WindowsString> windowName = nullptr,
           WindowStyle style = WindowStyle::OverlappedWindow,
           ExtendedWindowStyle extendedStyle = ExtendedWindowStyle::None,
           int x = CW_USEDEFAULT,
           int y = CW_USEDEFAULT,
           int width = CW_USEDEFAULT,
           int height = CW_USEDEFAULT,
           Ref<Window> parent = GetDesktop(),
           Ref<Menu> menu = nullptr,
           void *param = nullptr)
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
                         Application::GetCurrent().GetInstance(),
                         param);
    }

    void Show(ShowWindowCommand command)
    {
        ::ShowWindow(this->GetRef(), static_cast<int>(command));
    }

    static Ref<Window> GetDesktop()
    {
        return NULL;
    }
};
