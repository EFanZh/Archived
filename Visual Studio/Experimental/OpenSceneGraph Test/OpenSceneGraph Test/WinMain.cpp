#include "MainWindow.h"

int WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
  UNREFERENCED_PARAMETER(hInstance);
  UNREFERENCED_PARAMETER(hPrevInstance);
  UNREFERENCED_PARAMETER(lpCmdLine);

  ATOM atom = RegisterMainWindowClass();
  if (atom == NULL)
  {
    return 1;
  }

  HWND hWnd = CreateMainWindow();
  if (hWnd == NULL)
  {
    return 1;
  }

  ShowWindow(hWnd, nCmdShow);
  UpdateWindow(hWnd);

  BOOL ret;
  MSG msg;
  while ((ret = GetMessage(&msg, NULL, 0, 0)) != 0)
  {
    if (ret == -1)
    {
      return 1;
    }
    TranslateMessage(&msg);
    DispatchMessage(&msg);
  }

  return msg.wParam;
}
