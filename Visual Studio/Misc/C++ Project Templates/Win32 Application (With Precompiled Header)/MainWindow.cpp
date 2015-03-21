#include "MainWindow.h"

static LPCTSTR main_window_class_name = TEXT("MainWindow");

LRESULT CALLBACK MainWindow_WindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
void MainWindow_OnDestroy(HWND hWnd);

ATOM RegisterMainWindowClass()
{
    WNDCLASSEX wcex = { sizeof(wcex) };

    wcex.lpfnWndProc = MainWindow_WindowProc;
    wcex.hInstance = HINST_THISCOMPONENT;
    ::LoadIconMetric(NULL, IDI_APPLICATION, LIM_LARGE, &wcex.hIcon);
    wcex.hCursor = static_cast<HCURSOR>(LoadImage(NULL, IDC_ARROW, IMAGE_CURSOR, 0, 0, LR_SHARED));
    wcex.lpszClassName = TEXT("MainWindow");
    ::LoadIconMetric(NULL, IDI_APPLICATION, LIM_SMALL, &wcex.hIconSm);

    return RegisterClassEx(&wcex);
}

HWND CreateMainWindow()
{
    return ::CreateWindowEx(0, main_window_class_name, TEXT("Win32 Application"), WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, HWND_DESKTOP, NULL, HINST_THISCOMPONENT, NULL);
}

LRESULT CALLBACK MainWindow_WindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    switch (uMsg)
    {
        HANDLE_MSG(hWnd, WM_DESTROY, MainWindow_OnDestroy);

        default:
            return ::DefWindowProc(hWnd, uMsg, wParam, lParam);
    }
}

void MainWindow_OnDestroy(HWND hWnd)
{
    UNREFERENCED_PARAMETER(hWnd);

    ::PostQuitMessage(0);
}
