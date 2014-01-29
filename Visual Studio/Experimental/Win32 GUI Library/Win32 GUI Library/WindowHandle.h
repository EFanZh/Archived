#ifndef WINDOWHANDLE_H
#define WINDOWHANDLE_H

#include "Win32GUILibraryBase.h"

namespace Win32GUILibrary
{
  class WindowHandle
  {
    HWND hWnd;

  protected:
    WindowHandle()
    {
    }

    void SetHWnd(HWND value)
    {
      hWnd = value;
    }

  public:

    WindowHandle(HWND hWnd) : hWnd(hWnd)
    {
    }

    operator HWND()
    {
      return hWnd;
    }

    operator HWND() const
    {
      return hWnd;
    }

    HDC BeginPaint(LPPAINTSTRUCT lpPaint)
    {
      return ::BeginPaint(hWnd, lpPaint);
    }

    LRESULT DefaultWindowProc(UINT uMsg, WPARAM wParam, LPARAM lParam)
    {
      return ::DefWindowProc(*this, uMsg, wParam, lParam);
    }

    BOOL Destroy()
    {
      return ::DestroyWindow(hWnd);
    }

    BOOL EndPaint(const PAINTSTRUCT *lpPaint)
    {
      return ::EndPaint(hWnd, lpPaint);
    }

    HBRUSH GetBackgroundBrush()
    {
      return reinterpret_cast<HBRUSH>(::GetClassLongPtr(hWnd, GCLP_HBRBACKGROUND));
    }

    BOOL GetClientRect(RECT *lpRect)
    {
      return ::GetClientRect(hWnd, lpRect);
    }

    HWND GetDialogItem(int nIDDlgItem)
    {
      return ::GetDlgItem(hWnd, nIDDlgItem);
    }

    HFONT GetFont()
    {
      return GetWindowFont(hWnd);
    }

    BOOL GetRect(RECT *lpRect)
    {
      return ::GetWindowRect(hWnd, lpRect);
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

    BOOL Move(int X, int Y, int nWidth, int nHeight, BOOL bRepaint)
    {
      return ::MoveWindow(hWnd, X, Y, nWidth, nHeight, bRepaint);
    }

    BOOL RemoveSubclass(SUBCLASSPROC pfnSubclass, UINT_PTR uIdSubclass)
    {
      return ::RemoveWindowSubclass(hWnd, pfnSubclass, uIdSubclass);
    }

#pragma push_macro("SendMessage")
#undef SendMessage
    LRESULT SendMessage(UINT uMsg, WPARAM wParam, LPARAM lParam)
#pragma pop_macro("SendMessage")
    {
      return ::SendMessage(hWnd, uMsg, wParam, lParam);
    }

    LRESULT SendMessage(UINT uMsg, WPARAM wParam, LPARAM lParam)
    {
      return ::SendMessage(hWnd, uMsg, wParam, lParam);
    }

    HWND SetFocus()
    {
      return ::SetFocus(hWnd);
    }

    LONG_PTR SetLongPtr(int nIndex, LONG_PTR dwNewLong)
    {
      return ::SetWindowLongPtr(hWnd, nIndex, dwNewLong);
    }

    BOOL SetSubclass(SUBCLASSPROC pfnSubclass, UINT_PTR uIdSubclass, DWORD_PTR dwRefData)
    {
      return ::SetWindowSubclass(hWnd, pfnSubclass, uIdSubclass, dwRefData);
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
