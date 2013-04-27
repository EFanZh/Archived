#ifndef USERWINDOW_H
#define USERWINDOW_H

#include "Window.h"
#include "UserProcWindow.h"
#include "WindowClass.h"

namespace Win32GUILibrary
{
  template<class T>
  class UserWindow : public Window<UserWindow<T>>, public UserProcWindow
  {
    static LRESULT CALLBACK StaticWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
    {
      return reinterpret_cast<T *>(hWnd)->WindowProc(uMsg, wParam, lParam);
    }

  public:
    HWND Create(CREATE_FUNC_PARAM_LIST)
    {
      static ATOM class_atom = T::GetWindowClass().Register(UserProcWindow::StartWindowProc);

      UserProcWindow::AddCreateWindowInfo(this, StaticWindowProc, this);
      return ::CreateWindowEx(dwExStyle, reinterpret_cast<LPCTSTR>(class_atom), lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, hMenu, HINST_THISCOMPONENT, lpParam);
    }
  };
}

#endif // USERWINDOW_H
