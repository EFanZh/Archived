#ifndef WINDOWHANDLE_H
#define WINDOWHANDLE_H

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

    HDC BeginPaint(LPPAINTSTRUCT lpPaint)
    {
      return ::BeginPaint(hWnd, lpPaint);
    }

    BOOL EndPaint(const PAINTSTRUCT *lpPaint)
    {
      return ::EndPaint(hWnd, lpPaint);
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

    int GetTextLength()
    {
      return ::GetWindowTextLength(hWnd);
    }

    BOOL InvalidateRect(const RECT *lpRect, BOOL bErase)
    {
      return ::InvalidateRect(hWnd, lpRect, bErase);
    }

    LRESULT SendMessage(UINT uMsg, WPARAM wParam, LPARAM lParam)
    {
      return ::SendMessage(hWnd, uMsg, wParam, lParam);
    }

    int SetText(LPTSTR lpString)
    {
      return ::SetWindowText(hWnd, lpString);
    }

    BOOL Show(int nCmdShow)
    {
      return ::ShowWindow(hWnd, nCmdShow);
    }
  };
}

#endif // WINDOWHANDLE_H
