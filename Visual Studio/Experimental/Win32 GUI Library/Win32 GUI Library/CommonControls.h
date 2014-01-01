#ifndef COMMONCONTROLS_H
#define COMMONCONTROLS_H

#include "WindowHandle.h"

#define DECLARE_COMMON_CONTROL(x) static LPCTSTR GetControlClassName() { return x; }
#define DEFINE_COMMON_CONTROL(class_name, window_class_name) class class_name : public CommonControl<class_name> { public: DECLARE_COMMON_CONTROL(window_class_name) }

namespace Win32GUILibrary
{
  template<class T>
  class CommonControl : public WindowHandle
  {
  public:
    HWND Create(DWORD dwExStyle, LPCTSTR lpWindowName, DWORD dwStyle, int x, int y, int nWidth, int nHeight, HWND hWndParent, int nID, LPVOID lpParam)
    {
      this->SetHWnd(::CreateWindowEx(dwExStyle, T::GetControlClassName(), lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, reinterpret_cast<HMENU>(nID), HINST_THISCOMPONENT, lpParam));
      return *this;
    }
  };

  DEFINE_COMMON_CONTROL(Animation, ANIMATE_CLASS);
  DEFINE_COMMON_CONTROL(Button, WC_BUTTON);
  DEFINE_COMMON_CONTROL(ComboBox, WC_COMBOBOX);
  DEFINE_COMMON_CONTROL(ComboBoxEx, WC_COMBOBOXEX);
  DEFINE_COMMON_CONTROL(DateTimePicker, DATETIMEPICK_CLASS);
  DEFINE_COMMON_CONTROL(Dialog, WC_DIALOG);
  DEFINE_COMMON_CONTROL(Edit, WC_EDIT);
  DEFINE_COMMON_CONTROL(Header, WC_HEADER);
  DEFINE_COMMON_CONTROL(HotKey, HOTKEY_CLASS);
  DEFINE_COMMON_CONTROL(IPAddress, WC_IPADDRESS);
  DEFINE_COMMON_CONTROL(ListBox, WC_LISTBOX);
  DEFINE_COMMON_CONTROL(ListView, WC_LISTVIEW);
  DEFINE_COMMON_CONTROL(MonthCalendar, MONTHCAL_CLASS);
  DEFINE_COMMON_CONTROL(NativeFont, WC_NATIVEFONTCTL);
  DEFINE_COMMON_CONTROL(Pager, WC_PAGESCROLLER);
  DEFINE_COMMON_CONTROL(ProgressBar, PROGRESS_CLASS);
  DEFINE_COMMON_CONTROL(Rebar, REBARCLASSNAME);
  DEFINE_COMMON_CONTROL(ScrollBar, WC_SCROLLBAR);
  DEFINE_COMMON_CONTROL(Static, WC_STATIC);
  DEFINE_COMMON_CONTROL(StatusBar, STATUSCLASSNAME);
  DEFINE_COMMON_CONTROL(SysLink, WC_LINK);
  DEFINE_COMMON_CONTROL(Tab, WC_TABCONTROL);
  DEFINE_COMMON_CONTROL(Toolbar, TOOLBARCLASSNAME);
  DEFINE_COMMON_CONTROL(Tooltip, TOOLTIPS_CLASS);
  DEFINE_COMMON_CONTROL(Trackbar, TRACKBAR_CLASS);
  DEFINE_COMMON_CONTROL(TreeView, WC_TREEVIEW);
  DEFINE_COMMON_CONTROL(UpDown, UPDOWN_CLASS);
}

#endif // COMMONCONTROLS_H
