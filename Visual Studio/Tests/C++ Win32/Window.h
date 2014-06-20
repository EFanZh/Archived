#ifndef WINDOW_H
#define WINDOW_H

#include <Windows.h>
#include <CommCtrl.h>

#pragma comment(lib, "ComCtl32.Lib")
#pragma comment(linker, "\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")

EXTERN_C IMAGE_DOS_HEADER __ImageBase;
#define HINST_THISMODULE reinterpret_cast<HINSTANCE>(&__ImageBase)

class Window
{
    struct RegisterWindowClass
    {
        RegisterWindowClass()
        {
        }
    };

    HWND hWnd;

    static ATOM class_atom;

    static LRESULT CALLBACK StartStaticProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
    {
        if (uMsg == WM_NCCREATE)
        {
            SetClassLongPtr(hWnd, GCLP_WNDPROC, reinterpret_cast<LONG_PTR>(StaticProc));

            Window *p_window = reinterpret_cast<Window *>(reinterpret_cast<CREATESTRUCT *>(lParam)->lpCreateParams);

            SetWindowLongPtr(hWnd, GWLP_USERDATA, reinterpret_cast<LONG_PTR>(p_window));
            return p_window->Proc(uMsg, wParam, lParam);
        }
        else
        {
            return DefWindowProc(hWnd, uMsg, wParam, lParam);
        }
    }

    static LRESULT CALLBACK StaticProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
    {
        return reinterpret_cast<Window *>(GetWindowLongPtr(hWnd, GWLP_USERDATA))->Proc(uMsg, wParam, lParam);
    }

protected:
    virtual LRESULT Proc(UINT uMsg, WPARAM wParam, LPARAM lParam)
    {
        switch (uMsg)
        {
        default:
            return DefWindowProc(hWnd, uMsg, wParam, lParam);
        }
    }

    static ATOM RegisterClass(UINT style, int cbClsExtra, int cbWndExtra, HICON hIcon, HCURSOR hCursor, HBRUSH hbrBackground, LPCTSTR lpszMenuName, LPCTSTR lpszClassName, HICON hIconSm)
    {
        WNDCLASSEX wcex = { sizeof(wcex), style, StartStaticProc, cbClsExtra, cbWndExtra, HINST_THISMODULE, hIcon, hCursor, hbrBackground, lpszMenuName, lpszClassName, hIconSm };

        return RegisterClassEx(&wcex);
    }

public:
    Window(LPCTSTR lpClassName, LPCTSTR lpWindowName, DWORD dwStyle, int x, int y, int nWidth, int nHeight, HWND hWndParent, HMENU hMenu) :
        Window(0, lpClassName, lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, hMenu)
    {
    }

    Window(DWORD dwExStyle, LPCTSTR lpClassName, LPCTSTR lpWindowName, DWORD dwStyle, int x, int y, int nWidth, int nHeight, HWND hWndParent, HMENU hMenu)
    {
        CreateWindowEx(dwExStyle, lpClassName, lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, hMenu, HINST_THISMODULE, this);
    }
};

#endif // WINDOW_H
