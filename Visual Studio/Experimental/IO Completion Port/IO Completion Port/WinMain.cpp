#include "MainWindow.h"

CAppModule _Module;

int WINAPI _tWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPTSTR lpCmdLine, _In_ int nCmdShow)
{
  UNREFERENCED_PARAMETER(hPrevInstance);
  UNREFERENCED_PARAMETER(lpCmdLine);

  _CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);

  // WTL initialize.
  _Module.Init(NULL, hInstance);

  // WSA initialize.
  WSADATA wsa_data;
  WSAStartup(MAKEWORD(2, 2), &wsa_data);

  // Main window creation.
  MainWindow main_window;

  main_window.Create();
  main_window.ShowWindow(nCmdShow);

  // Main message loop.
  MSG msg;
  while (GetMessage(&msg, NULL, 0, 0))
  {
    TranslateMessage(&msg);
    DispatchMessage(&msg);
  }

  // WSA termination.
  WSACleanup();

  // WTL termination.
  _Module.Term();

  return msg.wParam;
}
