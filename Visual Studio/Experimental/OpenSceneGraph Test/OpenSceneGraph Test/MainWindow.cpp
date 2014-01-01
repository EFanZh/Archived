#include "MainWindow.h"

MainWindow::MainWindow()
{
  this->Create(0, TEXT("OpenSceneGraph Test"), WS_OVERLAPPEDWINDOW | WS_CLIPCHILDREN, CW_USEDEFAULT, CW_USEDEFAULT, 640, 360, HWND_DESKTOP, NULL, NULL);
}

LRESULT MainWindow::WindowProc(UINT uMsg, WPARAM wParam, LPARAM lParam)
{
  switch (uMsg)
  {
    WGL_HANDLE_WM_CREATE(OnCreate);
    WGL_HANDLE_WM_DESTROY(OnDestroy);
  default:
    if (uMsg != WM_SETCURSOR && uMsg != WM_NCHITTEST)
    {
      return scene.HandleNativeMessage(*this, uMsg, wParam, lParam);
    }
    else
    {
      return this->DefaultWindowProc(uMsg, wParam, lParam);
    }
  }
}

BOOL MainWindow::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
  UNREFERENCED_PARAMETER(lpCreateStruct);

  scene.BeginRender(*this);

  return TRUE;
}

void MainWindow::OnDestroy()
{
  scene.EndRender();
  PostQuitMessage(EXIT_SUCCESS);
}

void MainWindow::OnPaint()
{
  PAINTSTRUCT ps;

  this->BeginPaint(&ps);
  this->EndPaint(&ps);
}

Win32GUILibrary::WindowClass MainWindow::GetWindowClass()
{
  HICON h1, h2;
  HCURSOR cursor = static_cast<HCURSOR>(::LoadImage(NULL, IDC_ARROW, IMAGE_CURSOR, 0, 0, LR_SHARED));

  LoadIconMetric(NULL, IDI_APPLICATION, LIM_LARGE, &h1);
  LoadIconMetric(NULL, IDI_APPLICATION, LIM_SMALL, &h2);

  return Win32GUILibrary::WindowClass(0, 0, 0, h1, cursor, NULL, NULL, TEXT("MainWindow"), h2);
}
