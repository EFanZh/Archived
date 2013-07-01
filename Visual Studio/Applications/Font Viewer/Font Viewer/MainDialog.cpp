#include "resource.h"
#include "MainDialog.h"

INT_PTR MainDialog::DialogProc(UINT uMsg, WPARAM wParam, LPARAM lParam)
{
  WindowHandle edit(this->GetDialogItem(IDC_EDIT1));

  LOGFONT lf;
  GetObject(edit.GetFont(), sizeof(lf), &lf);

  switch (uMsg)
  {
  case WM_CLOSE:
    OnClose();
    break;

  case WM_INITDIALOG:
    return OnInitDialog(reinterpret_cast<HWND>(wParam), lParam);

  case WM_COMMAND:
    OnCommand(LOWORD(wParam), reinterpret_cast<HWND>(lParam), HIWORD(wParam));
    break;

  case WM_SETFONT:
    return TRUE;

  default:
    return FALSE;
  }

  return TRUE;
}

void MainDialog::OnClose()
{
  EndDialog(*this, 0);
}

BOOL MainDialog::OnInitDialog(HWND hWndFocus, LPARAM lParam)
{
  view.Create(0, TEXT("Preview"), WS_CHILD | WS_VISIBLE, 11, 11, 100, 100, *this, 7000, NULL);

  return TRUE;
}

void MainDialog::OnCommand(int id, HWND hWndCtl, UINT codeNotify)
{
  switch (id)
  {
  case IDC_EDITFONT:
    EditFont_OnCommand(codeNotify);
    break;

  default:
    break;
  }
}

void MainDialog::EditFont_OnCommand(UINT codeNotify)
{
  switch (codeNotify)
  {
  case EN_CHANGE:
    EditFont_OnChange();
    break;

  default:
    break;
  }
}

void MainDialog::EditFont_OnChange()
{
  WindowHandle edit_font(this->GetDialogItem(IDC_EDITFONT));
  int font_name_length = edit_font.GetTextLength() + 1;
  std::unique_ptr<TCHAR []> font_name(new TCHAR[font_name_length]);

  edit_font.GetText(font_name.get(), font_name_length);
  view.SetText(font_name.get());
}