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

    HBRUSH GetBackgroundBrush()
    {
      return reinterpret_cast<HBRUSH>(::GetClassLongPtr(hWnd, GCLP_HBRBACKGROUND));
    }

    HWND GetDialogItem(int nIDDlgItem)
    {
      return ::GetDlgItem(hWnd, nIDDlgItem);
    }

    HFONT GetFont()
    {
      return GetWindowFont(hWnd);
    }

    int GetText(LPTSTR lpString, int nMaxCount)
    {
      return ::GetWindowText(hWnd, lpString, nMaxCount);
    }

    int SetText(LPTSTR lpString)
    {
      return ::SetWindowText(hWnd, lpString);
    }

    int GetTextLength()
    {
      return ::GetWindowTextLength(hWnd);
    }

    BOOL InvalidateRect(const RECT *lpRect, BOOL bErase)
    {
      return ::InvalidateRect(hWnd, lpRect, bErase);
    }
  };
}

#endif // WINDOWHANDLE
