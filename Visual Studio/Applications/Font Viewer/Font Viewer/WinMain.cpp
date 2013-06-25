#include "MainDialog.h"

int WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
  UNREFERENCED_PARAMETER(hInstance);
  UNREFERENCED_PARAMETER(hPrevInstance);
  UNREFERENCED_PARAMETER(lpCmdLine);

  MainDialog::InitWindowClass();
  PreviewView::InitWindowClass();
  BufferedPaintInit();

  MainDialog().DoModal(HWND_DESKTOP);

  BufferedPaintUnInit();
}
