#ifndef USERDIALOGBOX_H
#define USERDIALOGBOX_H

#include "ThunkWindowTemplate.h"

namespace Win32GUILibrary
{
  template<class T>
  class UserDialogBox : public ThunkWindowTemplate<ThunkWindowTemplateTraitUserDialogBox>
  {
    static INT_PTR CALLBACK RedirectDialogProc(T *p_this, UINT uMsg, WPARAM wParam, LPARAM lParam)
    {
      return p_this->DialogProc(uMsg, wParam, lParam);
    }

  protected:
    INT_PTR DoModal(HWND hWndParent, LPARAM dwInitParam = NULL)
    {
      ThunkWindow::AddCreateWindowInfo(this, RedirectDialogProc);

      return ::DialogBoxParam(HINST_THISCOMPONENT, MAKEINTRESOURCE(T::IDD), hWndParent, ThunkWindowTemplate::StartWindowProc, dwInitParam);
    }
  };
}

#define DECLARE_DIALOGBOX_TEMPLATE(id) enum { IDD = id };

#define DECLARE_DIALOGBOX_TEMPLATE_EX(id, name) DECLARE_DIALOGBOX_TEMPLATE(id) static ATOM InitWindowClass() { WNDCLASSEX wcex = { sizeof(wcex) }; GetClassInfoEx(HINST_THISCOMPONENT, WC_DIALOG, &wcex); wcex.lpszClassName = name;  return RegisterClassEx(&wcex); }

#endif // USERDIALOGBOX_H
