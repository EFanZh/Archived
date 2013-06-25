#ifndef MAINDIALOG_H
#define MAINDIALOG_H

#include "PreviewView.h"
#include "resource.h"

class MainDialog : public Win32GUILibrary::UserDialogBox<MainDialog>
{
  PreviewView view;

public:
  INT_PTR DialogProc(UINT uMsg, WPARAM wParam, LPARAM lParam);
  void OnClose();
  BOOL OnInitDialog(HWND hWndFocus, LPARAM lParam);
  void OnCommand(int id, HWND hWndCtl, UINT codeNotify);
  void EditFont_OnCommand(UINT codeNotify);
  void EditFont_OnChange();

  DECLARE_DIALOGBOX_TEMPLATE_EX(IDD_MAINDIALOG, TEXT("MainDialog"))
};

#endif // MAINDIALOG_H
