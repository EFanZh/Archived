#include "MainWindow.h"

using namespace Win32GUILibrary;

int WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
  UNREFERENCED_PARAMETER(hInstance);
  UNREFERENCED_PARAMETER(hPrevInstance);
  UNREFERENCED_PARAMETER(lpCmdLine);

  MainWindow::Initialize();

  MainWindow main_window;
  main_window.Show(nCmdShow);

  return BasicMessageLoop::Run();
}
