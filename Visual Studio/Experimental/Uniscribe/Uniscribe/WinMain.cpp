#include "MainWindow.h"

int WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
  MainWindow main_window;

  main_window.Create(HWND_DESKTOP, NULL, TEXT("Uniscribe"), WS_OVERLAPPEDWINDOW);
  main_window.ShowWindow(nCmdShow);

  return CMessageLoop().Run();
}
