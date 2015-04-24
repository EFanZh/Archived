#pragma once

#include "WindowClassBase.h"
#include "WindowClassStyle.h"
#include "Icon.h"
#include "Cursor.h"
#include "Brush.h"
#include "WindowsString.h"
#include "Application.h"

template <class T>
class WindowClass : public WindowClassBase
{
    unsigned short id;

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
    WindowClass(Ref<WindowsString> className,
                Ref<Brush> backgroundBrush = Brush::GetSysColorBrush(SystemColorIndex::Window),
                Ref<Icon> icon = Icon::LoadMetric(StandardIconId::Application, IconMetric::Large),
                Ref<Icon> smallIcon = Icon::LoadMetric(StandardIconId::Application, IconMetric::Small),
                Ref<Cursor> cursor = Cursor::Load(StandardCursorId::Arrow),
                WindowClassStyle style = WindowClassStyle::None,
                Ref<WindowsString> menuName = nullptr,
                int classExtra = 0,
                int windowExtra = 0)
    {
        WNDCLASSEX windowClass =
        {
            sizeof(windowClass),
            static_cast<UINT>(style),
            InitialWindowProc,
            classExtra,
            windowExtra,
            Application::GetCurrent().GetInstance(),
            icon,
            cursor,
            backgroundBrush,
            menuName,
            className,
            smallIcon
        };

        id = ::RegisterClassEx(&windowClass);
    }

    unsigned short GetId() const
    {
        return id;
    }
};
