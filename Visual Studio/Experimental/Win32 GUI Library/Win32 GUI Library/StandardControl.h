#ifndef STANDARDCONTROL_H
#define STANDARDCONTROL_H

#include "Window.h"
#include "WindowHandle.h"

namespace Win32GUILibrary
{
  template<class T>
  class StandardControl : public Window<StandardControl<T>>, public WindowHandle
  {
  public:
    HWND Create(CREATE_FUNC_PARAM_LIST)
    {
      static LPCTSTR control_class_name = T::GetControlClassName();

      this->SetHWnd(::CreateWindowEx(dwExStyle, control_class_name, lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, hMenu, HINST_THISCOMPONENT, lpParam));
      return *this;
    }

    HWND Create(DWORD dwExStyle, LPCTSTR lpWindowName, DWORD dwStyle, int x, int y, int nWidth, int nHeight, HWND hWndParent, int nID, LPVOID lpParam)
    {
      return Create(dwExStyle, lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, reinterpret_cast<HMENU>(nID), lpParam);
    }
  };
}

#endif // STANDARDCONTROL_H
