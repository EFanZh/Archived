#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include "UserWindow.h"
#include "CommonControls.h"

class MainWindow : public Win32GUILibrary::UserWindow<MainWindow>
{
  friend UserWindow;

  Win32GUILibrary::Button button;

  static ATOM class_atom;

  LRESULT WindowProc(UINT uMsg, WPARAM wParam, LPARAM lParam);

public:
  using UserWindow::Create;

  static void Init()
  {
    WNDCLASSEX wcex = { 0 };

    LoadIconMetric(NULL, IDI_APPLICATION, LIM_LARGE, &wcex.hIcon);
    wcex.hCursor = static_cast<HCURSOR>(LoadImage(NULL, IDC_ARROW, IMAGE_CURSOR, 0, 0, LR_SHARED));
    wcex.lpszClassName = TEXT("MainWindow");
    LoadIconMetric(NULL, IDI_APPLICATION, LIM_SMALL, &wcex.hIconSm);

    UserWindow::RegisterWindowClass(wcex);
  }
};

#endif // MAINWINDOW_H
