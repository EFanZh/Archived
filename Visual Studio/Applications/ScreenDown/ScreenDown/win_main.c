#include "screen_down.h"
#include "main_window.h"

int APIENTRY _tWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPTSTR lpCmdLine, int nCmdShow)
{
  MSG msg;
  BOOL ret;

  UNREFERENCED_PARAMETER(hInstance);
  UNREFERENCED_PARAMETER(hPrevInstance);
  UNREFERENCED_PARAMETER(lpCmdLine);
  UNREFERENCED_PARAMETER(nCmdShow);

  // ScreenDown(48, 24, 0.01, RGB(0, 0, 0));
  register_main_window_class();
  ShowWindow(create_main_window(), nCmdShow);

  while ((ret = GetMessage(&msg, NULL, 0, 0)) != 0)
  {
    if (ret == -1)
    {
      return EXIT_FAILURE;
    }
    TranslateMessage(&msg);
    DispatchMessage(&msg);
  }

  return msg.wParam;
}
