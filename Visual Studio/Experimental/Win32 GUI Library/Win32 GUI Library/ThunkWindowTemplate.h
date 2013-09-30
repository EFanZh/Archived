#ifndef THUNKWINDOWTEMPLATE_H
#define THUNKWINDOWTEMPLATE_H

#include "ThunkWindow.h"

namespace Win32GUILibrary
{
  template<class T>
  class ThunkWindowTemplate : public ThunkWindow
  {
  protected:
    static typename T::ReturnType CALLBACK StartWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
    {
      return static_cast<T::ProcType>(ThunkWindow::InitThunkProc(hWnd, T::PROC_TYPE))(hWnd, uMsg, wParam, lParam);
    }

  public:
    void CreateFromHWnd(HWND hWnd)
    {
      this->CreateFromHWndInternal(hWnd, StartWindowProc, T::PROC_TYPE);
    }
  };
}

#endif // THUNKWINDOWTEMPLATE_H
