#ifndef STANDARDCONTROL_H
#define STANDARDCONTROL_H

#include "WindowHandle.h"

namespace Win32GUILibrary
{
  template<class T>
  class StandardControl : public WindowHandle
  {
  public:
    HWND Create(DWORD dwExStyle, LPCTSTR lpWindowName, DWORD dwStyle, int x, int y, int nWidth, int nHeight, HWND hWndParent, int nID, LPVOID lpParam)
    {
      return ::CreateWindowEx(dwExStyle, T::GetControlClassName(), lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, reinterpret_cast<HMENU>(nID), HINST_THISCOMPONENT, lpParam);
    }
  };
}

#define DECLARE_STANDARD_CONTROL(x) static LPCTSTR GetControlClassName() { return WC_##x; }
#define DEFINE_STANDARD_CONTROL(name, class_name) class name : public StandardControl<name> { public: DECLARE_STANDARD_CONTROL(class_name) };

#endif // STANDARDCONTROL_H
