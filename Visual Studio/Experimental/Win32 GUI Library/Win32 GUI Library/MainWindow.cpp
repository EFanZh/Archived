#include "MainWindow.h"

using namespace Win32GUILibrary;

ATOM MainWindow::class_atom;

LRESULT MainWindow::WindowProc(UINT uMsg, WPARAM wParam, LPARAM lParam)
{
  switch (uMsg)
  {
  case WM_CREATE:
    button.Create(0, TEXT("Test Button"), WS_CHILD | WS_VISIBLE, 11, 11, 120, 23, *this, 100, NULL);
    return 0;
  case WM_DESTROY:
    ::PostQuitMessage(0);
    return 0;
  default:
    return UserWindow::DefaultWindowProc(uMsg, wParam, lParam);
  }
}
