#ifndef WINDOWHANDLE
#define WINDOWHANDLE

namespace Win32GUILibrary
{
  class WindowHandle
  {
    HWND hWnd;

  protected:
    void SetHWnd(HWND value)
    {
      hWnd = value;
    }

  public:
    WindowHandle(HWND hWnd = NULL) : hWnd(hWnd)
    {
    }

    operator HWND()
    {
      return hWnd;
    }
  };
}

#endif // WINDOWHANDLE
