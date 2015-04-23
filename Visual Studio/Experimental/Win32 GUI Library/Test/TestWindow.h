#pragma once

#include "Window.h"
#include "WindowMessage.h"
#include "WindowClass.h"

class TestWindow : public Window
{
    static WindowClass<TestWindow> windowClass;

public:
    TestWindow() : Window(ExtendedWindowStyle::None, windowClass.GetClassId(), TEXT(""), WindowStyle::OverlappedWindow, 0, 0, CW_USEDEFAULT, CW_USEDEFAULT, nullptr, nullptr, nullptr)
    {
    }

    IntPtr WindowProc(WindowMessage message, UIntPtr wParam, IntPtr lParam)
    {
        switch (message)
        {
            case WindowMessage::Destroy:
                ::PostQuitMessage(0);
                break;

            default:
                return Window::WindowProc(message, wParam, lParam);
        }
    }
};

__declspec(selectany) WindowClass<TestWindow> TestWindow::windowClass(WindowClassStyle::None,
                                                                      0,
                                                                      0,
                                                                      Icon::LoadMetric(StandardIconId::Application, IconMetric::Large),
                                                                      Cursor::Load(StandardCursorId::Arrow),
                                                                      nullptr,
                                                                      nullptr,
                                                                      TEXT("Test Window"),
                                                                      Icon::LoadMetric(StandardIconId::Application, IconMetric::Small));
