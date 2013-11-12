#include "MainWindow.h"
#include "TestScene.h"

int WINAPI wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPWSTR lpCmdLine, int nCmdShow)
{
  MainWindow main_window;

  main_window.Show(nCmdShow);

  return Win32GUILibrary::BasicMessageLoop().Run();
}
