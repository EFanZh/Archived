#ifndef TESTWINDOW_H
#define TESTWINDOW_H

#include "Window.h"

class TestWindow : public Window
{
    static LPCTSTR window_class_name;

protected:

public:
    TestWindow() : Window(window_class_name, TEXT("Test Window"), WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, HWND_DESKTOP, NULL)
    {
    }

    static void Initialize()
    {
        HICON icon, small_icon;
        LoadIconMetric(NULL, IDI_APPLICATION, LIM_LARGE, &icon);
        LoadIconMetric(NULL, IDI_APPLICATION, LIM_SMALL, &small_icon);
        Window::RegisterClass(0, 0, 0, icon, static_cast<HCURSOR>(LoadImage(NULL, IDC_ARROW, IMAGE_CURSOR, 0, 0, LR_SHARED)), NULL, NULL, window_class_name, small_icon);
    }
};

__declspec(selectany) LPCTSTR TestWindow::window_class_name = TEXT("TestWindow");

#endif // TESTWINDOW_H
