#include "Window.h"

std::map<DWORD, Window *> Window::tid_to_window_map;

LRESULT CALLBACK Window::StaticWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    LONG_PTR user_data = GetWindowLongPtr(hWnd, GWLP_USERDATA);
    if (user_data == 0)
    {
        Window *window = tid_to_window_map[GetCurrentThreadId()];
        window->hWnd = hWnd;
        SetWindowLongPtr(hWnd, GWLP_USERDATA, reinterpret_cast<LONG_PTR>(window));
        return window->WindowProc(uMsg, wParam, lParam);
    }
    return reinterpret_cast<Window *>(user_data)->WindowProc(uMsg, wParam, lParam);
}

HWND Window::Create(LPCTSTR lpWindowName, DWORD dwStyle, int x, int y, int nWidth, int nHeight, HWND hWndParent, HMENU hMenu, LPVOID lpParam)
{
    return CreateEx(0, lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, hMenu, lpParam);
}

HWND Window::CreateEx(DWORD dwExStyle, LPCTSTR lpWindowName, DWORD dwStyle, int x, int y, int nWidth, int nHeight, HWND hWndParent, HMENU hMenu, LPVOID lpParam)
{
    HINSTANCE hInstance = GetModuleHandle(NULL);
    LPCTSTR window_class_name = GetWindowClassName();
    WNDCLASSEX wcex = { 0 };
    if (!GetClassInfoEx(hInstance, window_class_name, &wcex))
    {
        wcex.cbSize = sizeof(wcex);
        wcex.lpfnWndProc = StaticWindowProc;
        wcex.hInstance = hInstance;
        LoadIconMetric(NULL, IDI_APPLICATION, LIM_LARGE, &wcex.hIcon);
        wcex.hCursor = (HCURSOR)LoadImage(NULL, IDC_ARROW, IMAGE_CURSOR, 0, 0, LR_SHARED);
        wcex.lpszClassName = window_class_name;
        LoadIconMetric(NULL, IDI_APPLICATION, LIM_SMALL, &wcex.hIconSm);
        if (!RegisterClassEx(&wcex))
        {
            return NULL;
        }
    }
    tid_to_window_map[GetCurrentThreadId()] = this;
    return CreateWindowEx(dwExStyle, window_class_name, lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, hMenu, NULL, lpParam);
}
