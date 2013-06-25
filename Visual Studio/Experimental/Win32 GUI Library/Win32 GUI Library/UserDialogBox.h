#ifndef USERDIALOGBOX_H
#define USERDIALOGBOX_H

#include "UserProcWindow.h"
#include "UserProcWindowTraits.h"

namespace Win32GUILibrary
{
  template<class T>
  class UserDialogBox : public UserProcWindow<UserProcWindowTraitsUserDialogBox>
  {
    static INT_PTR CALLBACK StaticDialogProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
    {
      return reinterpret_cast<T *>(hWnd)->DialogProc(uMsg, wParam, lParam);
    }

  public:
    INT_PTR DoModal(HWND hWndParent, LPARAM dwInitParam = NULL)
    {
      UserProcWindow::AddCreateWindowInfo(this, StaticDialogProc);

      return ::DialogBoxParam(HINST_THISCOMPONENT, MAKEINTRESOURCE(T::IDD), hWndParent, UserProcWindow::StartWindowProc, dwInitParam);
    }
  };
}

#define DECLARE_DIALOGBOX_TEMPLATE(id) enum { IDD = id };

#define DECLARE_DIALOGBOX_TEMPLATE_EX(id, name) DECLARE_DIALOGBOX_TEMPLATE(id) static ATOM InitWindowClass() { WNDCLASSEX wcex = { sizeof(wcex) }; GetClassInfoEx(HINST_THISCOMPONENT, WC_DIALOG, &wcex); wcex.lpszClassName = name;  return RegisterClassEx(&wcex); }

#endif // USERDIALOGBOX_H
