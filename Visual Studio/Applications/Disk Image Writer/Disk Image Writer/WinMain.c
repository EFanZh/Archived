#include "MainWindow.h"
#include "Layout.h"

int WINAPI wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPTSTR lpCmdLine, int nCmdShow)
{
  HWND hWnd;
  BOOL ret;
  MSG msg;

  RegisterMainWindowClass();
  hWnd = CreateMainWindow();

  ShowWindow(hWnd, nCmdShow);
  UpdateWindow(hWnd);

  while ((ret = GetMessage(&msg, NULL, 0, 0)) != 0)
  {
    if (ret == -1)
    {
      return 1;
    }
    if (IsDialogMessage(hWnd, &msg))
    {
      continue;
    }
    TranslateMessage(&msg);
    DispatchMessage(&msg);
  }
  return msg.wParam;
}
