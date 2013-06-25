#ifndef USERWINDOW_H
#define USERWINDOW_H

#include "UserProcWindow.h"
#include "UserProcWindowTraits.h"
#include "Win32GUILibraryUtilities.h"
#include "WindowClass.h"

namespace Win32GUILibrary
{
  template<class T>
  class UserWindow : public UserProcWindow<UserProcWindowTraitsUserWindow>
  {
    static LRESULT CALLBACK StaticWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
    {
      return reinterpret_cast<T *>(hWnd)->WindowProc(uMsg, wParam, lParam);
    }

  protected:
    LRESULT DefaultWindowProc(UINT uMsg, WPARAM wParam, LPARAM lParam)
    {
      return ::DefWindowProc(*this, uMsg, wParam, lParam);
    }

  public:
    HWND Create(DWORD dwExStyle, LPCTSTR lpWindowName, DWORD dwStyle, int x, int y, int nWidth, int nHeight, HWND hWndParent, HMENUOrUINT hMenuOrnId, LPVOID lpParam)
    {
      UserProcWindow::AddCreateWindowInfo(this, StaticWindowProc);

      return ::CreateWindowEx(dwExStyle, reinterpret_cast<LPCTSTR>(InitWindowClass()), lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, hMenuOrnId, HINST_THISCOMPONENT, lpParam);
    }

    static ATOM InitWindowClass()
    {
      static ATOM class_atom = T::GetWindowClass().Register(UserProcWindow::StartWindowProc);

      return class_atom;
    }
  };
}

#define DECLARE_USER_WINDOW_CLASS(style, cbClsExtra, cbWndExtra, hIcon, hCursor, hbrBackground, lpszMenuName, lpszClassName, hIconSm) static Win32GUILibrary::WindowClass GetWindowClass() { return Win32GUILibrary::WindowClass(style, cbClsExtra, cbWndExtra, hIcon, hCursor, hbrBackground, lpszMenuName, lpszClassName, hIconSm); }

#endif // USERWINDOW_H
