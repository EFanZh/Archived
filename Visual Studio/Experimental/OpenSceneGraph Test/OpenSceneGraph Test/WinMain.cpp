#include "MainWindow.h"
#include "TestScene.h"

int WINAPI wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPWSTR lpCmdLine, int nCmdShow)
{
  UNREFERENCED_PARAMETER(hInstance);
  UNREFERENCED_PARAMETER(hPrevInstance);
  UNREFERENCED_PARAMETER(lpCmdLine);

  MainWindow main_window;

  main_window.Show(nCmdShow);

  return Win32GUILibrary::BasicMessageLoop().Run();
}
