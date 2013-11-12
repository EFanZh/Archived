#include "MainWindow.h"

MainWindow::MainWindow()
{
  this->Create(0, TEXT("OpenSceneGraph Test"), WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, 640, 360, HWND_DESKTOP, NULL, NULL);
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
  HICON icon = static_cast<HICON>(::LoadImage(NULL, IDI_APPLICATION, IMAGE_ICON, 0, 0, LR_SHARED));
  HCURSOR cursor = static_cast<HCURSOR>(::LoadImage(NULL, IDI_APPLICATION, IMAGE_CURSOR, 0, 0, LR_SHARED));

  return Win32GUILibrary::WindowClass(0, 0, 0, icon, cursor, NULL, NULL, TEXT("MainWindow"), icon);
}
