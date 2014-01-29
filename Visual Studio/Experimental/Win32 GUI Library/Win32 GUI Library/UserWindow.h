#ifndef USERWINDOW_H
#define USERWINDOW_H

#include "ThunkWindowTemplate.h"

namespace Win32GUILibrary
{
  class HMENUOrInt
  {
    HMENU hMenu;

  public:
    HMENUOrInt(HMENU hMenu) : hMenu(hMenu)
    {
    }

    HMENUOrInt(int nID) : hMenu(reinterpret_cast<HMENU>(nID))
    {
    }

    operator HMENU()
    {
      return hMenu;
    }
  };

  template<class T>
  class UserWindow : public ThunkWindowTemplate<ThunkWindowTemplateTraitUserWindow>
  {
    static LRESULT CALLBACK RedirectWindowProc(T *p_this, UINT uMsg, WPARAM wParam, LPARAM lParam)
    {
      return p_this->WindowProc(uMsg, wParam, lParam);
    }

  protected:
    HWND Create(DWORD dwExStyle, LPCTSTR lpWindowName, DWORD dwStyle, int x, int y, int nWidth, int nHeight, HWND hWndParent, HMENUOrInt hMenuOrnId, LPVOID lpParam)
    {
      ThunkWindow::AddCreateWindowInfo(this, RedirectWindowProc);

      return ::CreateWindowEx(dwExStyle, reinterpret_cast<LPCTSTR>(T::class_atom), lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, hMenuOrnId, HINST_THISCOMPONENT, lpParam);
    }

    static ATOM RegisterWindowClass(WNDCLASSEX &wcex)
    {
      // Set necessary fields.
      ThunkWindowTemplate::ProcessWindowClass(wcex);

      T::class_atom = ::RegisterClassEx(&wcex);

      return T::class_atom;
    }
  };
}

#endif // USERWINDOW_H
