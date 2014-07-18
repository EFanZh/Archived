#ifndef WINDOW_H
#define WINDOW_H

class Window
{
    static std::map<DWORD, Window *> tid_to_window_map;

    static LRESULT CALLBACK StaticWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

protected:
    HWND hWnd;

public:
    HWND Create(LPCTSTR lpWindowName, DWORD dwStyle = WS_OVERLAPPEDWINDOW, int x = CW_USEDEFAULT, int y = CW_USEDEFAULT, int nWidth = CW_USEDEFAULT, int nHeight = CW_USEDEFAULT, HWND hWndParent = HWND_DESKTOP, HMENU hMenu = NULL, LPVOID lpParam = NULL);
    HWND CreateEx(DWORD dwExStyle, LPCTSTR lpWindowName, DWORD dwStyle = WS_OVERLAPPEDWINDOW, int x = CW_USEDEFAULT, int y = CW_USEDEFAULT, int nWidth = CW_USEDEFAULT, int nHeight = CW_USEDEFAULT, HWND hWndParent = HWND_DESKTOP, HMENU hMenu = NULL, LPVOID lpParam = NULL);

    virtual LPCTSTR GetWindowClassName() const = 0;
    virtual LRESULT WindowProc(UINT uMsg, WPARAM wParam, LPARAM lParam) = 0;
};

#endif // WINDOW_H
