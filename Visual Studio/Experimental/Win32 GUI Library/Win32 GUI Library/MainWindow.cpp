#include "MainWindow.h"

using namespace Win32GUILibrary;

WindowClass MainWindow::GetWindowClass()
{
  HICON icon = static_cast<HICON>(LoadImage(NULL, IDI_APPLICATION, IMAGE_ICON, 0, 0, LR_SHARED));
  return std::move(WindowClass(0, 0, 0, icon, static_cast<HCURSOR>(LoadImage(NULL, IDI_APPLICATION, IMAGE_CURSOR, 0, 0, LR_SHARED)), reinterpret_cast<HBRUSH>(COLOR_WINDOW + 1), NULL, TEXT("MainWindow"), icon));
}

LRESULT MainWindow::WindowProc(UINT uMsg, WPARAM wParam, LPARAM lParam)
{
  switch (uMsg)
  {
  case WM_CREATE:
    button.Create(0, TEXT("Test Button"), WS_CHILD | WS_VISIBLE, 11, 11, 120, 23, *this, 100, NULL);
    return 0;
  case WM_DESTROY:
    ::PostQuitMessage(0);
  default:
    return UserWindow::DefaultProc(uMsg, wParam, lParam);
  }
}
