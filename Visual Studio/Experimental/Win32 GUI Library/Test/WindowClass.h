#pragma once

#include "Application.h"
#include "WindowClassStyle.h"
#include "Icon.h"
#include "Cursor.h"
#include "Brush.h"
#include "String.h"
#include "WindowClassBase.h"

template <class T>
class WindowClass : public WindowClassBase
{
    unsigned short classId;

    static LRESULT CALLBACK InitialWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
    {
        Window *window = ExtractCreateInfo();

        window->SetHandle(hWnd);
        window->thunk.Init(reinterpret_cast<DWORD_PTR>(StaticWindowProc), window);

        WNDPROC thunkProc = static_cast<WNDPROC>(window->thunk.GetCodeAddress());

        ::SetWindowLongPtr(hWnd, GWLP_WNDPROC, reinterpret_cast<LONG>(thunkProc));

        return thunkProc(hWnd, uMsg, wParam, lParam);
    }

    static LRESULT CALLBACK StaticWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
    {
        T *window = static_cast<T *>(reinterpret_cast<Window *>(hWnd));

        return window->WindowProc(static_cast<WindowMessage>(uMsg), wParam, lParam);
    }

public:
    WindowClass(WindowClassStyle style,
                int classExtra,
                int windowExtra,
                Ref<Icon> icon,
                Ref<Cursor> cursor,
                Ref<Brush> backgroundBrush,
                Ref<String> menuName,
                Ref<String> className,
                Ref<Icon> smallIcon)
    {
        WNDCLASSEX windowClass =
        {
            sizeof(windowClass),
            static_cast<UINT>(style),
            InitialWindowProc,
            classExtra,
            windowExtra,
            Application::GetCurrent().GetInstanceHandle(),
            icon,
            cursor,
            backgroundBrush,
            menuName,
            className,
            smallIcon
        };

        classId = ::RegisterClassEx(&windowClass);
    }

    unsigned short GetClassId() const
    {
        return classId;
    }
};
