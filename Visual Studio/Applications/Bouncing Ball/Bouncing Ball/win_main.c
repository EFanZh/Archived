#include "main_window.h"
#include "message_loop.h"

int APIENTRY _tWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPTSTR lpCmdLine, int nCmdShow)
{
  HWND hWnd;

  UNREFERENCED_PARAMETER(hPrevInstance);
  UNREFERENCED_PARAMETER(lpCmdLine);

  RegisterMainWindowClass(hInstance);
  hWnd = CreateMainWindow(hInstance);
  ShowWindow(hWnd, nCmdShow);
  UpdateWindow(hWnd);

  return MessageLoop().wParam;
}
