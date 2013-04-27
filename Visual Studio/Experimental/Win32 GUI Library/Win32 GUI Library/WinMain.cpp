#include "MainWindow.h"
#include "BasicMessageLoop.h"

using namespace Win32GUILibrary;

int WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
  MainWindow main_window;
  main_window.Create(0, TEXT("Win32 GUI Library Test"), WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, HWND_DESKTOP, NULL, NULL);
  ::ShowWindow(main_window, nCmdShow);
  return BasicMessageLoop().Run();
}
