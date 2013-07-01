#include "MainWindow.h"

static LPCTSTR main_window_class_name = TEXT("MainWindow");

LRESULT CALLBACK MainWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
void MainWindow_OnDestroy(HWND hWnd);

ATOM RegisterMainWindowClass()
{
  WNDCLASSEX wcex = { sizeof(wcex) };

  wcex.lpfnWndProc = MainWindowProc;
  wcex.hInstance = HINST_THISCOMPONENT;
  wcex.hIcon = static_cast<HICON>(LoadImage(NULL, IDI_APPLICATION, IMAGE_ICON, 0, 0, LR_SHARED));
  wcex.hCursor = static_cast<HCURSOR>(LoadImage(NULL, IDC_ARROW, IMAGE_CURSOR, 0, 0, LR_SHARED));
  wcex.lpszClassName = main_window_class_name;
  wcex.hIconSm = wcex.hIcon;

  return RegisterClassEx(&wcex);
}

HWND CreateMainWindow()
{
  return CreateWindowEx(0, main_window_class_name, TEXT("Win32 Application"), WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, HWND_DESKTOP, NULL, HINST_THISCOMPONENT, NULL);
}

LRESULT CALLBACK MainWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
  switch (uMsg)
  {
    HANDLE_MSG(hWnd, WM_DESTROY, MainWindow_OnDestroy);

  default:
    return DefWindowProc(hWnd, uMsg, wParam, lParam);
  }
}

void MainWindow_OnDestroy(HWND hWnd)
{
  UNREFERENCED_PARAMETER(hWnd);

  PostQuitMessage(0);
}