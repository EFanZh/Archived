#include "MainWindow.h"

ATOM MainWindow::class_atom;

MainWindow::MainWindow()
{
  this->Create(0, TEXT("Win32 GUI Library Test"), WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, HWND_DESKTOP, NULL, NULL);
}

LRESULT MainWindow::WindowProc(UINT uMsg, WPARAM wParam, LPARAM lParam)
{
  switch (uMsg)
  {
    WGL_HANDLE_WM_DESTROY(OnDestroy);
  default:
    return this->DefaultWindowProc(uMsg, wParam, lParam);
  }
}

void MainWindow::OnDestroy()
{
  PostQuitMessage(0);
}

void MainWindow::Initialize()
{
  WNDCLASSEX wcex = { 0 };

  LoadIconMetric(NULL, IDI_APPLICATION, LIM_LARGE, &wcex.hIcon);
  wcex.hCursor = static_cast<HCURSOR>(LoadImage(NULL, IDC_ARROW, IMAGE_CURSOR, 0, 0, LR_SHARED));
  wcex.lpszClassName = TEXT("MainWindow");
  LoadIconMetric(NULL, IDI_APPLICATION, LIM_SMALL, &wcex.hIconSm);

  UserWindow::RegisterWindowClass(wcex);
}
