#ifndef WINDOWCLASS_H
#define WINDOWCLASS_H

namespace Win32GUILibrary
{
  class WindowClass
  {
    WNDCLASSEX *p_wcex;

  public:
    WindowClass(UINT style, int cbClsExtra, int cbWndExtra, HICON hIcon, HCURSOR hCursor, HBRUSH hbrBackground, LPCWSTR lpszMenuName, LPCWSTR lpszClassName, HICON hIconSm) : p_wcex(new WNDCLASSEX())
    {
      p_wcex->cbSize = sizeof(*p_wcex);
      p_wcex->style = style;
      p_wcex->cbClsExtra = cbClsExtra;
      p_wcex->cbWndExtra = cbWndExtra;
      p_wcex->hInstance = HINST_THISCOMPONENT;
      p_wcex->hIcon = hIcon;
      p_wcex->hCursor = hCursor;
      p_wcex->hbrBackground = hbrBackground;
      p_wcex->lpszMenuName = lpszMenuName;
      p_wcex->lpszClassName = lpszClassName;
      p_wcex->hIconSm = hIconSm;
    }

    WindowClass(const WindowClass &rhs) : p_wcex(new WNDCLASSEX())
    {
      *p_wcex = *rhs.p_wcex;
    }

    WindowClass(WindowClass &&rhs) : p_wcex(rhs.p_wcex)
    {
      rhs.p_wcex = nullptr;
    }

    ~WindowClass()
    {
      delete p_wcex;
    }

    WindowClass &operator =(WindowClass rhs)
    {
      std::swap(p_wcex, rhs.p_wcex);

      return *this;
    }

    ATOM Register(WNDPROC WndProc)
    {
      p_wcex->lpfnWndProc = WndProc;

      return ::RegisterClassEx(p_wcex);
    }
  };
}

#endif // WINDOWCLASS_H
